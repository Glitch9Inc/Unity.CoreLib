using System;
using UnityEngine;

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
        private static class ConfirmationMessages
        {
            internal const string REVERT_CHANGES = "Do you wish to revert changes?";
            internal const string SAVE_ITEM = "Do you wish to save changes?";
            internal const string DELETE_ITEM = "Do you really wish to delete this item?";
        }

        internal static class ExtendedTreeViewGUI
        {
            internal static void Button(GUIContent label, TTreeViewItem item, TSelf parentWindow, Action<TTreeViewItem, Action<bool>> itemAction, Action<bool> deleted)
            {
                if (item == null || itemAction == null || parentWindow == null) return;

                if (GUILayout.Button(label))
                {
                    ExtendedTreeViewUtils.PerformItemAction(item, parentWindow, itemAction, deleted, false);
                }
            }

            internal static void ButtonWithWarning(string warningText, GUIContent label, TTreeViewItem item, TSelf parentWindow, Action<TTreeViewItem, Action<bool>> itemAction, Action<bool> deleted)
            {
                if (item == null || itemAction == null || parentWindow == null) return;

                if (GUILayout.Button(label))
                {
                    ExtendedTreeViewUtils.PerformItemActionWithWarning(warningText, item, parentWindow, itemAction, deleted, false);
                }
            }
        }


        internal static class ExtendedTreeViewUtils
        {
            internal static void PerformItemAction(TTreeViewItem item, TSelf parentWindow, Action<TTreeViewItem, Action<bool>> itemAction, Action<bool> onSuccess, bool force = false)
            {
                if (item == null || itemAction == null || parentWindow == null) return;

                itemAction(item, (success) =>
                {
                    onSuccess?.Invoke(success);

                    if (force)
                    {
                        parentWindow.RemoveItem(item);
                    }
                    else if (success)
                    {
                        parentWindow.RemoveItem(item);
                    }
                });
            }

            internal static void PerformItemActionWithWarning(string warningText, TTreeViewItem item, TSelf parentWindow, Action<TTreeViewItem, Action<bool>> itemAction, Action<bool> onSuccess, bool force = false)
            {
                if (item == null || itemAction == null || parentWindow == null) return;

                if (EGUI.Confirmation(warningText))
                {
                    itemAction(item, (success) =>
                    {
                        onSuccess?.Invoke(success);

                        if (force)
                        {
                            parentWindow.RemoveItem(item);
                        }
                        else if (success)
                        {
                            parentWindow.RemoveItem(item);
                        }
                    });
                }
            }
        }
    }
}