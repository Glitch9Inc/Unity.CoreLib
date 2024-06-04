using System;
using System.Collections.Generic;
using System.Linq;
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
        where TData : class, ITreeViewData<TData>
        where TFilter : class, ITreeViewFilter<TFilter, TData>
        where TEventHandler : TreeViewEventHandler<TTreeViewItem, TData, TFilter>
    {
        internal static class Strings
        {
            // Styles
            internal const string STYLE_TREEVIEW_ITEM = "treeviewitem";
            internal const string STYLE_TREEVIEW_GROUP = "treeviewgroup";
        }

        public abstract class ExtendedTreeView : TreeView
        {
            public List<TData> SourceData { get; private set; }
            public bool RequiresRefresh { get; set; }
            public int ShowingCount => _rows.Count;
            public int TotalCount => SourceData.Count;


            private TFilter _filter;
            private readonly EPrefs<TFilter> _filterSave;
            private readonly TTreeViewWindow _treeViewWindow;
            private readonly TEventHandler _eventHandler;

            private List<TreeViewItem> _cachedItems;
            private List<TreeViewItem> _rows;



            protected ExtendedTreeView(TTreeViewWindow treeViewWindow, TEventHandler eventHandler, TreeViewState treeViewState, MultiColumnHeader multiColumnHeader) : base(treeViewState, multiColumnHeader)
            {
                try
                {
                    _treeViewWindow = treeViewWindow;

                    string filterPrefsKey = $"{GetType().Name}.Filter";
                    _filterSave = new EPrefs<TFilter>(filterPrefsKey, Activator.CreateInstance<TFilter>());
                    _filter = _filterSave.Value;

                    // ReSharper disable once VirtualMemberCallInConstructor
                    SourceData = GetAllDataFromSource().ToList();

                    _eventHandler = PrepareEventHandler(eventHandler);

                    UpdateTreeView(true);

                    multiColumnHeader.sortingChanged += OnSortingChanged;
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            public void OnSortingChanged(MultiColumnHeader multiColumnHeaderParam)
            {
                UpdateTreeView();
            }

            public void OnSearchStringChanged()
            {
                UpdateTreeView();
            }
            
            public void ResetFilter()
            {
                _filter = null;
            }

            /// <summary>
            /// Override this method to get data from a source.
            /// </summary>
            /// <returns></returns>
            protected abstract IEnumerable<TData> GetAllDataFromSource();

            public void OnDestroy()
            {
                if (_filterSave != null)
                {
                    _filterSave.Value = _filter;
                }
            }

            private TEventHandler PrepareEventHandler(TEventHandler eventHandler)
            {
                if (eventHandler == null) return Activator.CreateInstance<TEventHandler>();

                eventHandler.DeleteItem?.AddListener((item, deleted) =>
                {
                    if (deleted) RemoveItem(item);
                });

                return eventHandler;
            }

            protected override TreeViewItem BuildRoot()
            {
                TreeViewItem root = new() { id = 0, depth = -1, displayName = "Root" };

                if (_cachedItems != null)
                {
                    SetupParentsAndChildrenFromDepths(root, _cachedItems);
                    return root;
                }

                UpdateTreeView();
                if (_cachedItems == null) return root;
                SetupParentsAndChildrenFromDepths(root, _cachedItems);
                return root;
            }

            protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
            {
                if (root.children == null) return new List<TreeViewItem>();
                if (_rows != null && !RequiresRefresh) return _rows;

                //Debug.Log("Rebuilding tree view rows...");
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

                if (multiColumnHeader.sortedColumnIndex == -1) return _rows;
                int columnIndex = multiColumnHeader.sortedColumnIndex;
                bool ascending = multiColumnHeader.IsSortedAscending(columnIndex);

                _rows.ToList().Sort(Comparison);
                RequiresRefresh = false;

                return _rows;

                int Comparison(TreeViewItem x, TreeViewItem y)
                {
                    if (x is not TTreeViewItem itemX || y is not TTreeViewItem itemY) return 0;
                    return itemX.CompareTo(itemY, columnIndex, ascending);
                }
            }

            protected override void RowGUI(RowGUIArgs args)
            {
                TreeViewItem tableItem = args.item;

                if (Event.current.type == EventType.Repaint)
                {
                    bool isNotGroupRow = tableItem is TTreeViewItem;
                    GUIStyle backgroundStyle = isNotGroupRow ? EGUI.skin.GetStyle(Strings.STYLE_TREEVIEW_ITEM) : EGUI.skin.GetStyle(Strings.STYLE_TREEVIEW_GROUP);
                    backgroundStyle.Draw(args.rowRect, false, false, false, false);
                }

                for (int i = 0; i < args.GetNumVisibleColumns(); ++i)
                {
                    CellGUI(args.GetCellRect(i), tableItem, i, ref args);
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
                    UpdateTreeView();
                }
            }

            public void ShowEditWindow(TTreeViewItem item)
            {
                if (item == null || item.Data == null) return;
                string windowTitle = item.Data.Id == null ? "Details" : $"Details: {item.Data.Id}";

                try
                {
                    TTreeViewEditWindow window = GetWindow<TTreeViewEditWindow>(false, windowTitle, true);
                    window.minSize = new Vector2(400, 400);
                    window.maxSize = new Vector2(800, 800);
                    window.SetData(item, this as TTreeView, _eventHandler);
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
            
            public void UpdateTreeView(bool filterUpdated = false)
            {
                //Debug.Log("Reloading TreeView...");

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
                        Debug.LogError($"Error updating cached items: {e.Message}");
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
                        if (Activator.CreateInstance(typeof(TTreeViewItem), id, 0, null, data) is not TTreeViewItem newItem)
                        {
                            throw new Exception($"Failed to create new {typeof(TTreeViewItem).Name} instance.");
                        }

                        if (_filter != null)
                        {
                            bool filtered = newItem.IsFiltered(_filter);
                            if (filtered)
                            {
                                Debug.Log($"Item {newItem.Data.Id} is not visible.");
                                continue;
                            }
                        }

                        items.Add(newItem);
                    }

                    return items;
                }
            }


            protected override void ContextClickedItem(int id)
            {
                base.ContextClickedItem(id);

                TreeViewItem item = FindItem(id, rootItem);
                if (item is TTreeViewItem treeViewItem)
                {
                    OnRightClickedItem(treeViewItem);
                }
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
                UpdateTreeView();
            }

            public void UpdateData(TData data)
            {
                if (data == null) return;
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

                UpdateTreeView();
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

                UpdateTreeView();

                //Debug.LogWarning($"Item {item.displayName} not found in cached items.");
            }

        }
    }
}
