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

        // Actions
        public TreeViewEvent CopyItem
        {
            get
            {
                _copyItem ??= new TreeViewEvent(MenuNames.COPY_ITEM, ConfirmationMessages.COPY_ITEM);
                return _copyItem;
            }
        }

        public TreeViewEvent PasteItem
        {
            get
            {
                _pasteItem ??= new TreeViewEvent(MenuNames.PASTE_ITEM, ConfirmationMessages.PASTE_ITEM);
                return _pasteItem;
            }
        }

        public TreeViewEvent SaveItem
        {
            get
            {
                _saveItem ??= new TreeViewEvent(MenuNames.SAVE_ITEM, ConfirmationMessages.SAVE_ITEM);
                return _saveItem;
            }
        }

        public TreeViewEvent DeleteItem
        {
            get
            {
                _deleteItem ??= new TreeViewEvent(MenuNames.DELETE_ITEM, ConfirmationMessages.DELETE_ITEM);
                return _deleteItem;
            }
        }
        
        private TreeViewEvent _copyItem;
        private TreeViewEvent _pasteItem;
        private TreeViewEvent _saveItem;
        private TreeViewEvent _deleteItem;


        public void ShowRightClickMenu(TTreeViewItem item, Action<bool> refreshTreeView)
        {
            GenericMenu menu = new();

            if (CopyItem != null && CopyItem.ShowInRightClickMenu)
            {
                menu.AddItem(new GUIContent(CopyItem.Name), false, () => CopyItem.Execute(item, refreshTreeView));
            }

            if (PasteItem != null && PasteItem.ShowInRightClickMenu)
            {
                menu.AddItem(new GUIContent(PasteItem.Name), false, () => PasteItem.Execute(item, refreshTreeView));
            }

            if (SaveItem != null && SaveItem.ShowInRightClickMenu)
            {
                menu.AddItem(new GUIContent(SaveItem.Name), false, () => SaveItem.Execute(item, refreshTreeView));
            }

            if (DeleteItem != null && DeleteItem.ShowInRightClickMenu)
            {
                menu.AddItem(new GUIContent(DeleteItem.Name), false, () => DeleteItem.Execute(item, refreshTreeView));
            }

            menu.ShowAsContext();
        }

        public void ShowEditWindowMenu(TTreeViewItem item, Action<IResult> refreshWindow, bool isDirty, Action revertChange = null)
        {
            GenericMenu menu = new();

            if (CopyItem != null && CopyItem.ShowInEditWindowMenu)
            {
                menu.AddItem(new GUIContent(CopyItem.Name), false, () => CopyItem.Execute(item, (success) =>
                {
                    refreshWindow(Result.Success());
                }));
            }

            if (PasteItem != null && PasteItem.ShowInEditWindowMenu)
            {
                menu.AddItem(new GUIContent(PasteItem.Name), false, () => PasteItem.Execute(item, (success) =>
                {
                    refreshWindow(Result.Success());
                }));
            }

            if (revertChange != null)
            {
                menu.AddItem(new GUIContent(MenuNames.REVERT_CHANGES), isDirty, () => revertChange());
            }

            if (SaveItem != null && SaveItem.ShowInEditWindowMenu)
            {
                menu.AddItem(new GUIContent(SaveItem.Name), isDirty, () => SaveItem.Execute(item, (success) =>
                {
                    refreshWindow(Result.Saved());
                }));
            }

            if (DeleteItem != null && DeleteItem.ShowInEditWindowMenu)
            {
                menu.AddItem(new GUIContent(DeleteItem.Name), false, () => DeleteItem.Execute(item, (success) =>
                {
                    refreshWindow(Result.Success());
                }));
            }

            menu.ShowAsContext();
        }

        public class TreeViewEvent
        {
            public bool ShowInRightClickMenu { get; set; } = true;
            public bool ShowInEditWindowMenu { get; set; } = true;
            public bool ShowConfirmationMessage { get; set; } = false;

            public readonly string Name;
            private readonly string _confirmationMessage;

            private Action<TTreeViewItem, Action<bool>> _onAction;
            private Action<TTreeViewItem, bool> _onSuccess;

            internal TreeViewEvent(string name, string confirmationMessage)
            {
                Name = name;
                _confirmationMessage = confirmationMessage;
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