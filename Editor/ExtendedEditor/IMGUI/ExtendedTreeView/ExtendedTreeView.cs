using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using TreeView = UnityEditor.IMGUI.Controls.TreeView;


namespace Glitch9.ExtendedEditor.IMGUI
{
    public abstract partial class ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>
        where TTreeViewWindow : ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>
        where TTreeView : ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>.ExtendedTreeView
        where TTreeViewItem : ExtendedTreeViewItem<TTreeViewItem, TData, TFilter>
        where TTreeViewEditWindow : ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>.ExtendedTreeViewEditWindow
        where TData : class, IData<TData>
        where TFilter : class, IFilter<TFilter, TData>
        where TEventHandler : TreeViewEventHandler<TTreeViewItem, TData, TFilter>
    {
        public abstract class ExtendedTreeView : TreeView
        {
            public TFilter Filter { get; private set; }
            public List<TData> SourceData { get; private set; }
            public bool RequiresRefresh { get; set; }
            public int ShowingCount => _rows?.Count ?? 0;
            public int TotalCount => SourceData?.Count ?? 0;

            private readonly EPrefs<TFilter> _filterSave;
            private readonly TTreeViewWindow _treeViewWindow;
            private readonly TEventHandler _eventHandler;

            private List<TreeViewItem> _cachedItems;
            private List<TreeViewItem> _rows;

            private TTreeViewEditWindow _editWindowInstance;



            protected ExtendedTreeView(TTreeViewWindow treeViewWindow, TEventHandler eventHandler, TreeViewState treeViewState, MultiColumnHeader multiColumnHeader) : base(treeViewState, multiColumnHeader)
            {
                try
                {
                    _treeViewWindow = treeViewWindow;

                    string filterPrefsKey = $"{GetType().Name}.Filter";
                    _filterSave = new EPrefs<TFilter>(filterPrefsKey, Activator.CreateInstance<TFilter>());
                    Filter = _filterSave.Value;

                    _eventHandler = PrepareEventHandler(eventHandler);
                    
                    // ReSharper disable once VirtualMemberCallInConstructor
                    SourceData = GetAllDataFromSource().ToList();
                    if (SourceData.IsNullOrEmpty()) Debug.LogWarning("No data found for tree view.");

                    RefreshTreeView(true);
                    multiColumnHeader.sortingChanged += OnSortingChanged;
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            public void OnSortingChanged(MultiColumnHeader multiColumnHeaderParam)
            {
                Debug.Log("Sorting changed: " + multiColumnHeaderParam.sortedColumnIndex);
                RefreshTreeView();
            }

            public void OnSearchStringChanged()
            {
                RefreshTreeView();
            }

            public void ResetFilter()
            {
                Filter = null;
            }

            /// <summary>
            /// Override this method to get data from a source.
            /// </summary>
            /// <returns></returns>
            protected abstract IEnumerable<TData> GetAllDataFromSource();

            protected virtual void RemoveDataFromSource(TData data)
            {
                Debug.LogWarning("RemoveDataFromSource not implemented.");
            }

            public void OnDestroy()
            {
                if (_filterSave != null)
                {
                    _filterSave.Value = Filter;
                }
            }

            private TEventHandler PrepareEventHandler(TEventHandler eventHandler)
            {
                if (eventHandler == null) return Activator.CreateInstance<TEventHandler>();

                eventHandler.AddOnItemAdded((item, added) =>
                {
                    if (added) AddItem(item);
                });

                eventHandler.AddOnItemUpdated((item, updated) =>
                {
                    if (updated) UpdateItem(item);
                });

                eventHandler.AddOnItemRemoved((item, deleted) =>
                {
                    if (deleted) RemoveItem(item);
                });

                eventHandler.CreateEvent(TreeViewItemEvent.Revert)
                    .WithAction(RevertChanges)
                    .WithCondition(RevertCondition)
                    .Add();

                return eventHandler;
            }

            private void RevertChanges(TTreeViewItem item, Action<bool> onResult)
            {
                if (_editWindowInstance == null) return;
                _editWindowInstance.RevertChanges();
                onResult?.Invoke(true);
            }

            private bool RevertCondition(TTreeViewItem item)
            {
                if (_editWindowInstance == null) return false;
                return _editWindowInstance.IsDirty;
            }

            protected override TreeViewItem BuildRoot()
            {
                TreeViewItem root = new() { id = 0, depth = -1, displayName = "Root" };

                if (_cachedItems != null)
                {
                    SetupParentsAndChildrenFromDepths(root, _cachedItems);
                    return root;
                }

                RefreshTreeView();
                if (_cachedItems == null) return root;
                SetupParentsAndChildrenFromDepths(root, _cachedItems);
                return root;
            }

            protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
            {
                if (root.children == null) return new List<TreeViewItem>();
                if (_rows != null && !RequiresRefresh) return _rows;

                Debug.Log("Rebuilding tree view rows...");
                _rows = new();

                foreach (TreeViewItem treeViewItem in _cachedItems)
                {
                    if (_treeViewWindow.Menu != null && _treeViewWindow.Menu.HasSearchField)
                    {
                        //Debug.Log("Search string: " + _parentWindow.Toolbar.SearchString);
                        if (treeViewItem is TTreeViewItem genericItem)
                        {
                            if (genericItem.Data == null) continue;
                            if (!genericItem.Search(_treeViewWindow.Menu.SearchString)) continue;
                        }
                    }

                    _rows.Add(treeViewItem);
                }

                if (multiColumnHeader.sortedColumnIndex == -1)
                {
                    RequiresRefresh = false;
                    OnTreeViewUpdated();
                    return _rows;
                }

                Debug.Log("Resorting tree view rows...");
                int columnIndex = multiColumnHeader.sortedColumnIndex;
                bool ascending = multiColumnHeader.IsSortedAscending(columnIndex);

                _rows.Sort(Comparison);

                RequiresRefresh = false;
                OnTreeViewUpdated();
                return _rows;

                int Comparison(TreeViewItem x, TreeViewItem y)
                {
                    if (x is not TTreeViewItem itemX || y is not TTreeViewItem itemY)
                    {
                        Debug.LogWarning("Comparison failed: " + x + " - " + y);
                        return 0;
                    }
                    return itemX.CompareTo(itemY, columnIndex, ascending);
                }
            }

            protected override void RowGUI(RowGUIArgs args)
            {
                TreeViewItem treeViewItem = args.item;

                if (Event.current.type == EventType.Repaint)
                {
                    bool isNotGroupRow = treeViewItem is TTreeViewItem;
                    GUIStyle backgroundStyle = isNotGroupRow ? TreeViewStyles.TreeViewItem : TreeViewStyles.TreeViewGroup;
                    backgroundStyle.Draw(args.rowRect, false, false, false, false);
                }

                for (int i = 0; i < args.GetNumVisibleColumns(); ++i)
                {
                    CellGUI(args.GetCellRect(i), treeViewItem, i, ref args);
                }
            }

            /// <summary>
            /// Override this method to draw the cell GUI for each column in the tree view.
            /// </summary>
            /// <param name="cellRect"></param>
            /// <param name="item"></param>
            /// <param name="columnIndex"></param>
            /// <param name="args"></param>
            protected abstract void CellGUI(Rect cellRect, TreeViewItem item, int columnIndex, ref RowGUIArgs args);

            /// <summary>
            /// Override this method to handle right-clicking on an item in the tree view.
            /// </summary>
            /// <param name="item"></param>
            protected virtual void OnRightClickedItem(TTreeViewItem item)
            {
                if (_eventHandler == null)
                {
                    Debug.LogError("Event handler is null.");
                    return;
                }
                _eventHandler.ShowRightClickMenu(item, OnDataUpdated);
            }

            private void OnDataUpdated(bool success)
            {
                if (success)
                {
                    RefreshTreeView();
                }
            }

            public void ShowEditWindow(TTreeViewItem item)
            {
                if (item == null || item.Data == null) return;
                string windowTitle = item.Data.Id == null ? "Details" : $"Details: {item.Data.Id}";

                if (_editWindowInstance != null) _editWindowInstance.Close();

                try
                {
                    _editWindowInstance = GetWindow<TTreeViewEditWindow>(false, windowTitle, true);
                    _editWindowInstance.minSize = new Vector2(400, 400);
                    _editWindowInstance.maxSize = new Vector2(800, 800);
                    _editWindowInstance.SetData(item, this as TTreeView, _eventHandler);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error creating window: {e.Message}");
                }
            }

            /// <summary>
            /// Override this method to handle double clicking on a tree view item.
            /// </summary>
            /// <param name="item"></param>
            protected virtual void OnDoubleClickedItem(TTreeViewItem item)
            {
                ShowEditWindow(item);
            }

            public void RefreshTreeView(bool filterUpdated = false, bool reloadSourceData = false)
            {
                //Debug.Log("Reloading TreeView...");
                if (reloadSourceData) SourceData = GetAllDataFromSource().ToList();
                
                if (filterUpdated)
                {
                    if (UpdateFilter())
                    {
                        RequiresRefresh = true;
                        Reload();
                    }

                    return;
                }

                RequiresRefresh = true;
                Reload();

                return;

                bool UpdateFilter()
                {
                    if (SourceData == null) return false;

                    try
                    {
                        _cachedItems = BuildCachedItems();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error updating cached items: {e.Message}: {e.StackTrace}");
                        return false;
                    }
                }

                List<TreeViewItem> BuildCachedItems()
                {
                    Debug.Log("Building tree view cached items...");

                    List<TreeViewItem> items = new();

                    for (int i = 0; i < SourceData.Count; i++)
                    {
                        int id = i + 1000;
                        TData data = SourceData[i];
                        if (data == null) continue;
                        string name = data.Name ?? $"NoName ({id})";

                        if (Activator.CreateInstance(typeof(TTreeViewItem), id, 0, name, data) is not TTreeViewItem newItem)
                        {
                            throw new Exception($"Failed to create new {typeof(TTreeViewItem).Name} instance.");
                        }

                        if (Filter != null)
                        {
                            bool filtered = newItem.IsFiltered(Filter);
                            if (filtered)
                            {
                                //Debug.Log($"Item {newItem.Data.Id} is not visible.");
                                continue;
                            }
                        }

                        items.Add(newItem);
                    }

                    return items;
                }
            }

            protected virtual void OnTreeViewUpdated()
            {
                // Do nothing
            }


            protected override void ContextClickedItem(int id)
            {
                IList<int> selection = GetSelection();

                if (selection.Count > 1)
                {
                    List<TTreeViewItem> bulkItems = new();
                    foreach (int selectedId in selection)
                    {
                        TreeViewItem item = FindItem(selectedId, rootItem);
                        if (item is TTreeViewItem treeViewItem)
                        {
                            bulkItems.Add(treeViewItem);
                        }
                    }

                    OnRightClickedBulkItems(bulkItems);
                }
                else
                {
                    TreeViewItem item = FindItem(id, rootItem);
                    if (item is TTreeViewItem treeViewItem)
                    {
                        OnRightClickedItem(treeViewItem);
                    }
                }
            }


            protected virtual void OnRightClickedBulkItems(IList<TTreeViewItem> bulkItems)
            {
                // Do nothing
            }

            protected override void DoubleClickedItem(int id)
            {
                base.DoubleClickedItem(id);

                TreeViewItem item = FindItem(id, rootItem);
                if (item is TTreeViewItem treeViewItem)
                {
                    OnDoubleClickedItem(treeViewItem);
                }
            }

            protected void SetData(List<TData> data)
            {
                if (data == null) return;
                Debug.Log($"Setting data set of {data.Count} to TreeView.");
                SourceData = data;
                RefreshTreeView();
            }

            public void UpdateData(TData data)
            {
                if (data == null) return;
                if (string.IsNullOrEmpty(data.Id)) return;
                
                Debug.Log($"Updating data {data.Id} in TreeView.");
                TData existingData = SourceData.FirstOrDefault(d => d.Id == data.Id);
                if (existingData == null)
                {
                    Debug.LogError($"Data with id {data.Id} not found in TreeView.");
                    return;
                }

                // replace the existing data with the new data
                int index = SourceData.IndexOf(existingData);
                SourceData[index] = data;

                RefreshTreeView();
            }

            public void AddItem(TTreeViewItem item)
            {
                if (item == null) return;

                // add the new item to the cached items
                _cachedItems.Add(item);

                if (item.Data != null)
                {
                    SourceData.Add(item.Data);
                }

                RefreshTreeView();
            }

            public void UpdateItem(TTreeViewItem item)
            {
                if (item == null) return;

                // find the same item by comparing id
                TreeViewItem foundItem = _cachedItems.Find(x => x.id == item.id);

                if (foundItem != null)
                {
                    _cachedItems.Remove(foundItem);
                    _cachedItems.Add(item);
                }

                if (item.Data != null && !string.IsNullOrEmpty(item.Data.Id))
                {
                    TData existingData = SourceData.FirstOrDefault(d => d.Id == item.Data.Id);
                    if (existingData == null)
                    {
                        Debug.LogError($"Data with id {item.Data.Id} not found in TreeView.");
                        return;
                    }

                    // replace the existing data with the new data
                    int index = SourceData.IndexOf(existingData);
                    SourceData[index] = item.Data;
                }

                RefreshTreeView();
            }

            public void RemoveItem(TTreeViewItem item)
            {
                if (item == null) return;

                // find the same item by comparing id
                TreeViewItem foundItem = _cachedItems.Find(x => x.id == item.id);

                if (foundItem != null)
                {
                    _cachedItems.Remove(foundItem);
                }

                if (item.Data != null)
                {
                    SourceData.Remove(item.Data);
                }

                RemoveDataFromSource(item.Data);
                RefreshTreeView();

                //Debug.LogWarning($"Item {item.displayName} not found in cached items.");
            }

            public void RemoveItems(IList<TTreeViewItem> selectedItems)
            {
                if (selectedItems == null || selectedItems.Count == 0) return;

                foreach (TTreeViewItem item in selectedItems)
                {
                    RemoveItem(item);
                }
            }
        }
    }
}
