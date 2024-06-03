using System;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public class EditWindowMenuActionResult
    {
        internal bool IsSuccess { get; set; }
        internal bool IsFailure => !IsSuccess;
        internal bool IsSaved { get; set; }

        public EditWindowMenuActionResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public abstract class TreeViewEventHandler<TTreeViewItem, TData, TFilter>
        where TTreeViewItem : ExtendedTreeViewItem<TTreeViewItem, TData, TFilter>
        where TData : class, ITreeViewData<TData, TFilter>
        where TFilter : class, ITreeViewFilter<TFilter, TData>
    {

        internal const string MENU_SAVE = "Save";
        internal const string MENU_REVERT_CHANGES = "Revert Changes";
        internal const string MENU_DELETE = "Delete";
        internal const string MENU_COPY = "Copy";
        internal const string MENU_PASTE = "Paste";

        // Actions
        public abstract TreeViewEvent CopyItem { get; set; }
        public abstract TreeViewEvent PasteItem { get; set; }
        public abstract TreeViewEvent SaveItem { get; set; }
        public abstract TreeViewEvent DeleteItem { get; set; }


        public void ShowRightClickMenu(TTreeViewItem item, Action<bool> refreshTreeView)
        {
            GenericMenu menu = new();

            if (CopyItem != null && CopyItem.ShowInRightClickMenu)
            {
                menu.AddItem(new GUIContent(MENU_COPY), false, () => CopyItem.Execute(item, refreshTreeView));
            }

            if (PasteItem != null && PasteItem.ShowInRightClickMenu)
            {
                menu.AddItem(new GUIContent(MENU_PASTE), false, () => PasteItem.Execute(item, refreshTreeView));
            }

            if (SaveItem != null && SaveItem.ShowInRightClickMenu)
            {
                menu.AddItem(new GUIContent(MENU_SAVE), false, () => SaveItem.Execute(item, refreshTreeView));
            }

            if (DeleteItem != null && DeleteItem.ShowInRightClickMenu)
            {
                menu.AddItem(new GUIContent(MENU_DELETE), false, () => DeleteItem.Execute(item, refreshTreeView));
            }

            menu.ShowAsContext();
        }

        public void ShowEditWindowMenu(TTreeViewItem item, Action<EditWindowMenuActionResult> refreshWindow, bool isDirty, Action revertChange = null)
        {
            GenericMenu menu = new();

            if (CopyItem != null && CopyItem.ShowInEditWindowMenu)
            {
                menu.AddItem(new GUIContent(MENU_COPY), false, () => CopyItem.Execute(item, (success) =>
                {
                    EditWindowMenuActionResult result = new(success);
                    refreshWindow(result);
                }));
            }

            if (PasteItem != null && PasteItem.ShowInEditWindowMenu)
            {
                menu.AddItem(new GUIContent(MENU_PASTE), false, () => PasteItem.Execute(item, (success) =>
                {
                    EditWindowMenuActionResult result = new(success);
                    refreshWindow(result);
                }));
            }

            if (revertChange != null)
            {
                menu.AddItem(new GUIContent(MENU_REVERT_CHANGES), isDirty, () => revertChange());
            }

            if (SaveItem != null && SaveItem.ShowInEditWindowMenu)
            {
                menu.AddItem(new GUIContent(MENU_SAVE), isDirty, () => SaveItem.Execute(item, (success) =>
                {
                    EditWindowMenuActionResult result = new(success);
                    if (success) result.IsSaved = true;
                    refreshWindow(result);
                }));
            }

            if (DeleteItem != null && DeleteItem.ShowInEditWindowMenu)
            {
                menu.AddItem(new GUIContent(MENU_DELETE), false, () => DeleteItem.Execute(item, (success) =>
                {
                    EditWindowMenuActionResult result = new(success);
                    refreshWindow(result);
                }));
            }

            menu.ShowAsContext();
        }


        public class TreeViewEvent
        {
            //public string NameOverride { get; set; }
            //public string ConfirmationMessageOverride { get; set; }

            public bool ShowInRightClickMenu { get; set; } = true;
            public bool ShowInEditWindowMenu { get; set; } = true;


            private Action<TTreeViewItem, Action<bool>> _onAction;
            private Action<TTreeViewItem, bool> _onSuccess;

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
                _onAction?.Invoke(item, (success) =>
                {
                    _onSuccess?.Invoke(item, success);
                    onSuccess(success);
                });
            }
        }
    }
}