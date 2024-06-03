using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using TreeView = UnityEditor.IMGUI.Controls.TreeView;


namespace Glitch9.ExtendedEditor.IMGUI
{
    public abstract partial class ExtendedTreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>
        where TSelf : ExtendedTreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>
        where TTreeView : ExtendedTreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>.ExtendedTreeView
        where TTreeViewItem : ExtendedTreeViewItem<TTreeViewItem, TData, TFilter>
        where TTreeViewEditWindow : ExtendedTreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>.ExtendedTreeViewEditWindow
        where TData : class, ITreeViewData<TData, TFilter>
        where TFilter : class, ITreeViewFilter<TFilter, TData>
        where TEventHandler : TreeViewEventHandler<TTreeViewItem, TData, TFilter>
    {
        internal static class Strings
        {
            internal const string UNKNOWN_TIME = "Unknown";

            // Styles
            internal const string STYLE_TREEVIEW_ITEM = "treeviewitem";
            internal const string STYLE_TREEVIEW_GROUP = "treeviewgroup";
        }

        public abstract class ExtendedTreeView : TreeView
        {
            public bool RequiresRefresh { get; set; }

            private readonly TSelf _parentWindow;
            private readonly TEventHandler _eventHandler;

            private List<TreeViewItem> _cachedTreeViewItems;
            private List<TreeViewItem> _rows;

            protected ExtendedTreeView(TSelf parentWindow, TEventHandler eventHandler, TreeViewState treeViewState, MultiColumnHeader multiColumnHeader) : base(treeViewState, multiColumnHeader)
            {
                _parentWindow = parentWindow;
                _eventHandler = PrepareEventHandler(eventHandler);
                multiColumnHeader.sortingChanged += OnSortingChanged;
                RefreshData();
            }

            private TEventHandler PrepareEventHandler(TEventHandler eventHandler)
            {
                if (eventHandler == null) return Activator.CreateInstance<TEventHandler>();

                eventHandler.DeleteItem?.AddListener((item, deleted) =>
                {
                    if (deleted) _parentWindow.RemoveItem(item);
                });

                return eventHandler;
            }

            protected override TreeViewItem BuildRoot()
            {
                TreeViewItem root = new() { id = 0, depth = -1, displayName = "Root" };

                if (_cachedTreeViewItems != null)
                {
                    SetupParentsAndChildrenFromDepths(root, _cachedTreeViewItems);
                    return root;
                }

                UpdateCachedItems();
                if (_cachedTreeViewItems == null) return root;
                SetupParentsAndChildrenFromDepths(root, _cachedTreeViewItems);
                return root;
            }

            protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
            {
                if (root.children == null) return new List<TreeViewItem>();
                if (_rows != null && !RequiresRefresh) return _rows;

                //Debug.Log("Rebuilding tree view rows...");
                _rows = new();

                foreach (TreeViewItem treeViewItem in _cachedTreeViewItems)
                {
                    if (_parentWindow.Toolbar.HasSearchField)
                    {
                        //Debug.Log("Search string: " + _parentWindow.Toolbar.SearchString);
                        if (treeViewItem is TTreeViewItem genericItem)
                        {
                            if (genericItem.Data == null) continue;
                            if (!genericItem.Data.Search(_parentWindow.Toolbar.SearchString)) continue;
                        }
                    }

                    _rows.Add(treeViewItem);
                }

                if (multiColumnHeader.sortedColumnIndex == -1) return _rows;
                int columnIndex = multiColumnHeader.sortedColumnIndex;
                bool ascending = multiColumnHeader.IsSortedAscending(columnIndex);

                int Comparison(TreeViewItem x, TreeViewItem y)
                {
                    TTreeViewItem itemX = x as TTreeViewItem;
                    TTreeViewItem itemY = y as TTreeViewItem;
                    if (itemX == null || itemY == null) return 0;

                    return itemX.CompareTo(itemY, columnIndex, ascending);
                }

                _rows.ToList().Sort(Comparison);
                RequiresRefresh = false;

                return _rows;
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
                    try
                    {
                        CellGUI(args.GetCellRect(i), tableItem, i, ref args);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
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
                    RefreshData();
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
                    window.SetData(item, _parentWindow, _eventHandler);
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

            protected void DrawString(Rect cellRect, string text, GUIStyle style = null)
            {
                if (text == null) return;
                if (style == null) GUI.Label(cellRect, text);
                else GUI.Label(cellRect, text, style);
            }

            protected void DrawUnixTime(Rect cellRect, UnixTime? unixTime, GUIStyle style = null)
            {
                string timeString = unixTime == null ? Strings.UNKNOWN_TIME : unixTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                DrawString(cellRect, timeString, style);
            }

            public void OnSortingChanged(MultiColumnHeader multiColumnHeaderParam)
            {
                if (UpdateCachedItems()) CustomReload();
            }

            public void RefreshData()
            {
                if (UpdateCachedItems()) CustomReload();
            }

            public void CustomReload()
            {
                //Debug.Log("Reloading TreeView...");
                RequiresRefresh = true;
                Reload();
            }

            private bool UpdateCachedItems()
            {
                if (_parentWindow.FilteredData == null)
                {
                    GNLog.Error("Filtered Data is null");
                    return false;
                }

                _cachedTreeViewItems = new();

                for (int i = 0; i < _parentWindow.FilteredData.Count; i++)
                {
                    int id = i + 1000;
                    TData data = _parentWindow.FilteredData[i];
                    TTreeViewItem newItem = Activator.CreateInstance(typeof(TTreeViewItem), id, 0, null, data) as TTreeViewItem;
                    if (newItem == null)
                    {
                        GNLog.Error($"Failed to create new {typeof(TTreeViewItem).Name} instance.");
                        return false;
                    }
                    _cachedTreeViewItems.Add(newItem);
                }

                return true;
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

            public void RemoveItem(TTreeViewItem item)
            {
                if (item == null) return;

                // find the same item by comparing id
                TreeViewItem foundItem = _cachedTreeViewItems.Find(x => x.id == item.id);

                if (foundItem != null)
                {
                    _cachedTreeViewItems.Remove(foundItem);
                    RefreshData();
                }

                //Debug.LogWarning($"Item {item.displayName} not found in cached items.");
            }
        }
    }
}
