using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using TreeView = UnityEditor.IMGUI.Controls.TreeView;


namespace Glitch9.ExtendedEditor.IMGUI
{
    public abstract partial class TreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewChildWindow, TData, TFilter, TEventHandler>
        where TSelf : TreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewChildWindow, TData, TFilter, TEventHandler>
        where TTreeView : TreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewChildWindow, TData, TFilter, TEventHandler>.ExtendedTreeView
        where TTreeViewItem : ExtendedTreeViewItem<TData, TFilter>
        where TTreeViewChildWindow : TreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewChildWindow, TData, TFilter, TEventHandler>.TreeViewChildWindow
        where TData : ITreeViewData<TData, TFilter>
        where TFilter : ITreeViewFilter<TFilter, TData>
        where TEventHandler : ITreeViewEventHandler<TTreeViewItem, TData, TFilter>
    {
        internal static class Strings
        {
            internal const string UNKNOWN_TIME = "Unknown";
        }

        public abstract class ExtendedTreeView : TreeView
        {
            private const string STYLE_TREEVIEW_ITEM = "treeviewitem";
            private const string STYLE_TREEVIEW_GROUP = "treeviewgroup";

            private readonly TSelf _parentWindow;
            private readonly TEventHandler _eventHandler;

            private List<TreeViewItem> _cachedTreeViewItems;

            protected ExtendedTreeView(TSelf parentWindow, TEventHandler eventHandler, TreeViewState treeViewState, MultiColumnHeader multiColumnHeader) : base(treeViewState, multiColumnHeader)
            {
                _parentWindow = parentWindow;
                _eventHandler = eventHandler;
                multiColumnHeader.sortingChanged += OnSortingChanged;
                RefreshData();
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

            protected override void RowGUI(RowGUIArgs args)
            {
                TreeViewItem tableItem = args.item;

                if (Event.current.type == EventType.Repaint)
                {
                    bool isNotGroupRow = tableItem is TTreeViewItem;
                    GUIStyle backgroundStyle = isNotGroupRow ? EGUI.skin.GetStyle(STYLE_TREEVIEW_ITEM) : EGUI.skin.GetStyle(STYLE_TREEVIEW_GROUP);
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
                //Debug.Log($"Right clicked item: {item.Data}");
                if (_eventHandler == null || _eventHandler.EmptyActions) return;

                // Draw context menu

                GenericMenu menu = new();

                if (_eventHandler.CopyItem != null)
                {
                    menu.AddItem(new GUIContent("Copy"), false, () => _eventHandler.CopyItem(item));
                }

                if (_eventHandler.PasteItem != null)
                {
                    menu.AddItem(new GUIContent("Paste"), false, () => _eventHandler.PasteItem(item, (success) => { if (success) RefreshData(); }));
                }

                if (_eventHandler.DeleteItem != null)
                {
                    menu.AddItem(new GUIContent("Delete"), false, () => _eventHandler.DeleteItem(item, (success) => { if (success) RefreshData(); }));
                }

                menu.ShowAsContext();
            }

            private void ShowChildWindow(TTreeViewItem item)
            {
                if (item == null || item.Data == null) return;
                string windowTitle = item.Data.Id == null ? "Details" : $"Details: {item.Data.Id}";

                try
                {
                    TTreeViewChildWindow window = GetWindow<TTreeViewChildWindow>(true, windowTitle, true);
                    window.minSize = new Vector2(400, 400);
                    window.maxSize = new Vector2(800, 800);
                    window.Data = item.Data;
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
                ShowChildWindow(item);
            }

            protected void DrawString(Rect cellRect, string text, GUIStyle style = null)
            {
                if (text == null) return;
                if (style == null) GUI.Label(cellRect, text);
                else GUI.Label(cellRect, text, style);
            }

            protected void DrawUnixTime(Rect cellRect, UnixTime? unixTime, GUIStyle style = null)
            {
                string timeString = unixTime == null ? Strings.UNKNOWN_TIME : unixTime.Value.ToString();
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

            private void CustomReload()
            {
                Debug.Log("Reloading TreeView...");
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
        }
    }
}
