using System;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{

    public abstract class TreeViewEventHandler<TTreeViewItem, TData, TFilter>
        where TTreeViewItem : ExtendedTreeViewItem<TTreeViewItem, TData, TFilter>
        where TData : class, ITreeViewData<TData>
        where TFilter : class, ITreeViewFilter<TFilter, TData>
    {
        private static class ConfirmationMessages
        {
            internal const string REVERT_CHANGES = "Do you wish to revert changes?";
            internal const string SAVE_ITEM = "Do you wish to save changes?";
            internal const string DELETE_ITEM = "Do you really wish to delete this item?";
            internal const string COPY_ITEM = "Do you wish to copy this item?";
            internal const string PASTE_ITEM = "Do you wish to paste this item?";
        }

        private static class MenuNames
        {
            internal const string SAVE_ITEM = "Save";
            internal const string REVERT_CHANGES = "Revert Changes";
            internal const string DELETE_ITEM = "Delete";
            internal const string COPY_ITEM = "Copy";
            internal const string PASTE_ITEM = "Paste";
        }

        public TreeViewEvent CopyItem => _copyItem ??= new TreeViewEvent(MenuNames.COPY_ITEM, ConfirmationMessages.COPY_ITEM);
        public TreeViewEvent PasteItem => _pasteItem ??= new TreeViewEvent(MenuNames.PASTE_ITEM, ConfirmationMessages.PASTE_ITEM);
        public TreeViewEvent SaveItem => _saveItem ??= new TreeViewEvent(MenuNames.SAVE_ITEM, ConfirmationMessages.SAVE_ITEM);
        public TreeViewEvent DeleteItem => _deleteItem ??= new TreeViewEvent(MenuNames.DELETE_ITEM, ConfirmationMessages.DELETE_ITEM);

        private TreeViewEvent _copyItem;
        private TreeViewEvent _pasteItem;
        private TreeViewEvent _saveItem;
        private TreeViewEvent _deleteItem;


        public void ShowRightClickMenu(TTreeViewItem item, Action<bool> refreshTreeView)
        {
            GenericMenu menu = new();

            ShowRightClickMenuInternal(ref menu, item, refreshTreeView, CopyItem);
            ShowRightClickMenuInternal(ref menu, item, refreshTreeView, PasteItem);
            ShowRightClickMenuInternal(ref menu, item, refreshTreeView, SaveItem);
            ShowRightClickMenuInternal(ref menu, item, refreshTreeView, DeleteItem);

            menu.ShowAsContext();
        }

        public void ShowEditWindowMenu(TTreeViewItem item, Action<IResult> refreshWindow, bool isDirty, Action revertChange = null)
        {
            GenericMenu menu = new();

            ShowEditWindowMenuInternal(ref menu, item, refreshWindow, CopyItem);
            ShowEditWindowMenuInternal(ref menu, item, refreshWindow, PasteItem);

            if (revertChange != null) menu.AddItem(new GUIContent(MenuNames.REVERT_CHANGES), isDirty, () => revertChange());

            ShowEditWindowMenuInternal(ref menu, item, refreshWindow, SaveItem, Result.Saved());
            ShowEditWindowMenuInternal(ref menu, item, refreshWindow, DeleteItem);

            menu.ShowAsContext();
        }

        private void ShowRightClickMenuInternal(ref GenericMenu menu, TTreeViewItem item, Action<bool> refreshWindow, TreeViewEvent treeViewEvent)
        {
            if (!treeViewEvent.IsEmpty && treeViewEvent.ShowInRightClickMenu)
            {
                bool isVisible = treeViewEvent.IsVisible(item);

                if (isVisible)
                {
                    menu.AddItem(new GUIContent(treeViewEvent.Name), false, () => treeViewEvent.Execute(item, refreshWindow));
                }
                else
                {
                    menu.AddDisabledItem(new GUIContent(treeViewEvent.Name));
                }
            }
        }

        private void ShowEditWindowMenuInternal(ref GenericMenu menu, TTreeViewItem item, Action<IResult> refreshWindow, TreeViewEvent treeViewEvent, IResult customResult = null)
        {
            if (!treeViewEvent.IsEmpty && treeViewEvent.ShowInEditWindowMenu)
            {
                bool isVisible = treeViewEvent.IsVisible(item);

                if (isVisible)
                {
                    menu.AddItem(new GUIContent(treeViewEvent.Name), false, () => treeViewEvent.Execute(item, (success) =>
                    {
                        refreshWindow(customResult ?? Result.Success());
                    }));
                }
                else
                {
                    menu.AddDisabledItem(new GUIContent(treeViewEvent.Name));
                }
            }
        }

        public class TreeViewEvent
        {
            public bool IsEmpty => _onAction == null;
            public bool ShowInRightClickMenu { get; set; } = true;
            public bool ShowInEditWindowMenu { get; set; } = true;
            public bool ShowConfirmationMessage { get; set; } = false;

            public readonly string Name;
            private readonly string _confirmationMessage;

            private Action<TTreeViewItem, Action<bool>> _onAction;
            private Action<TTreeViewItem, bool> _onSuccess;
            private Func<TTreeViewItem, bool> _isVisible;

            internal TreeViewEvent(string name, string confirmationMessage)
            {
                Name = name;
                _confirmationMessage = confirmationMessage;
            }

            public bool IsVisible(TTreeViewItem item)
            {
                bool isVisible = _isVisible == null || _isVisible(item);
                //Debug.Log($"{Name} is visible: {isVisible}");
                return isVisible;
            }

            public void AddVisibilityCheck(Func<TTreeViewItem, bool> isVisible)
            {
                _isVisible += isVisible;
            }

            public void AddListener(Action<TTreeViewItem, Action<bool>> action)
            {
                _onAction += action;
            }

            public void AddListener(Action<TTreeViewItem, bool> onSuccess)
            {
                _onSuccess += onSuccess;
            }

            public void RemoveListener(Action<TTreeViewItem, Action<bool>> action)
            {
                _onAction -= action;
            }

            public void RemoveListener(Action<TTreeViewItem, bool> onSuccess)
            {
                _onSuccess -= onSuccess;
            }

            public void Execute(TTreeViewItem item, Action<bool> onSuccess)
            {
                if (ShowConfirmationMessage)
                {
                    if (!EGUI.Confirmation(_confirmationMessage))
                    {
                        return;
                    }
                }

                _onAction?.Invoke(item, (success) =>
                {
                    _onSuccess?.Invoke(item, success);
                    onSuccess(success);
                });
            }
        }

    }
}