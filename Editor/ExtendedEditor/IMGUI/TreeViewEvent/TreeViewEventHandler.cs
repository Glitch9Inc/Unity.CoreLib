using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public abstract class TreeViewEventHandler<TTreeViewItem, TData, TFilter>
        where TTreeViewItem : ExtendedTreeViewItem<TTreeViewItem, TData, TFilter>
        where TData : class, IData<TData>
        where TFilter : class, IFilter<TFilter, TData>
    {

        public List<TreeViewEvent> Events { get; } = new();
        private bool _sorted = false;

        public void AddEvent(TreeViewEvent treeViewEvent) => Events.Add(treeViewEvent);

        public TreeViewEventBuilder CreateEvent(TreeViewItemEvent itemEvent = TreeViewItemEvent.None)
        {
            return new TreeViewEventBuilder(this, itemEvent);
        }

        public TreeViewEventBuilder CreateEvent(string customEvent)
        {
            return new TreeViewEventBuilder(this, customEvent);
        }

        public void ShowRightClickMenu(TTreeViewItem item, Action<bool> refreshTreeView)
        {
            if (Events.IsNullOrEmpty()) return;
            SortEvents();

            GenericMenu menu = new();

            foreach (TreeViewEvent treeViewEvent in Events)
            {
                ShowRightClickMenuInternal(ref menu, item, refreshTreeView, treeViewEvent);
            }

            menu.ShowAsContext();
        }

        public void ShowEditWindowMenu(TTreeViewItem item, Action<IResult> refreshWindow)
        {
            if (Events.IsNullOrEmpty()) return;
            SortEvents();

            GenericMenu menu = new();

            foreach (TreeViewEvent treeViewEvent in Events)
            {
                ShowEditWindowMenuInternal(ref menu, item, refreshWindow, treeViewEvent);
            }

            menu.ShowAsContext();
        }

        private static void ShowRightClickMenuInternal(ref GenericMenu menu, TTreeViewItem item, Action<bool> refreshWindow, TreeViewEvent treeViewEvent)
        {
            if (!treeViewEvent.IsEmpty && treeViewEvent.ShowInRightClickMenu)
            {
                bool isVisible = treeViewEvent.IsVisible(item);

                if (isVisible)
                {
                    menu.AddItem(new GUIContent(treeViewEvent.MenuName), false, () => treeViewEvent.Execute(item, refreshWindow));
                }
                else
                {
                    menu.AddDisabledItem(new GUIContent(treeViewEvent.MenuName));
                }
            }
        }

        private static void ShowEditWindowMenuInternal(ref GenericMenu menu, TTreeViewItem item, Action<IResult> refreshWindow, TreeViewEvent treeViewEvent, IResult customResult = null)
        {
            if (!treeViewEvent.IsEmpty && treeViewEvent.ShowInEditWindowMenu)
            {
                bool isVisible = treeViewEvent.IsVisible(item);

                if (isVisible)
                {
                    menu.AddItem(new GUIContent(treeViewEvent.MenuName), false, () => treeViewEvent.Execute(item, (success) =>
                    {
                        refreshWindow(customResult ?? Result.Success());
                    }));
                }
                else
                {
                    menu.AddDisabledItem(new GUIContent(treeViewEvent.MenuName));
                }
            }
        }

        private void SortEvents()
        {
            if (_sorted) return;

            Events.Sort((a, b) => a.Index.CompareTo(b.Index));
            _sorted = true;
        }

        internal void AddOnItemAdded(Action<TTreeViewItem, bool> addAction)
        {
            if (Events.IsNullOrEmpty()) return;

            foreach (TreeViewEvent treeViewEvent in Events)
            {
                if (treeViewEvent.TreeViewItemEvent == TreeViewItemEvent.Add)
                {
                    treeViewEvent.Callback += addAction;
                }
            }
        }

        internal void AddOnItemUpdated(Action<TTreeViewItem, bool> updateAction)
        {
            if (Events.IsNullOrEmpty()) return;

            foreach (TreeViewEvent treeViewEvent in Events)
            {
                if (treeViewEvent.TreeViewItemEvent == TreeViewItemEvent.Save
                    || treeViewEvent.TreeViewItemEvent == TreeViewItemEvent.Paste)
                {
                    treeViewEvent.Callback += updateAction;
                }
            }
        }

        internal void AddOnItemRemoved(Action<TTreeViewItem, bool> removalAction)
        {
            if (Events.IsNullOrEmpty()) return;

            foreach (TreeViewEvent treeViewEvent in Events)
            {
                if (treeViewEvent.TreeViewItemEvent == TreeViewItemEvent.Remove)
                {
                    treeViewEvent.Callback += removalAction;
                }
            }
        }

        public class TreeViewEvent : IListEntry
        {
            public int Index { get; set; }
            public bool IsEmpty => Action == null;
            public bool ShowInRightClickMenu { get; set; } = true;
            public bool ShowInEditWindowMenu { get; set; } = true;
            public bool ShowConfirmationMessage { get; set; } = false;
            public TreeViewItemEvent TreeViewItemEvent { get; set; } = TreeViewItemEvent.None;
            public string MenuName { get; set; }
            public string ConfirmationMessage { get; set; }
            public Action<TTreeViewItem, Action<bool>> Action { get; set; }
            public Action<TTreeViewItem, bool> Callback { get; set; }
            public Func<TTreeViewItem, bool> VisibilityCheck { get; set; }
            
            public bool IsVisible(TTreeViewItem item) => VisibilityCheck == null || VisibilityCheck(item);
            public void Execute(TTreeViewItem item, Action<bool> onSuccess)
            {
                if (ShowConfirmationMessage)
                {
                    if (!EGUI.Confirmation(ConfirmationMessage))
                    {
                        return;
                    }
                }

                Action?.Invoke(item, (success) =>
                {
                    Callback?.Invoke(item, success);
                    onSuccess(success);
                });
            }
        }

        public class TreeViewEventBuilder
        {
            private static class ConfirmationMessages
            {
                internal const string ADD_ITEM = "Do you wish to add this item?";
                internal const string REVERT_CHANGES = "Do you wish to revert changes?";
                internal const string SAVE_ITEM = "Do you wish to save changes?";
                internal const string DELETE_ITEM = "Do you really wish to delete this item?";
                internal const string COPY_ITEM = "Do you wish to copy this item?";
                internal const string PASTE_ITEM = "Do you wish to paste this item?";
            }

            private static class MenuNames
            {
                internal const string ADD_ITEM = "Add";
                internal const string SAVE_ITEM = "Save";
                internal const string REVERT_CHANGES = "Revert Changes";
                internal const string DELETE_ITEM = "Delete";
                internal const string COPY_ITEM = "Copy";
                internal const string PASTE_ITEM = "Paste";
            }

            
            private readonly TreeViewEventHandler<TTreeViewItem, TData, TFilter> _handler;
            private readonly TreeViewEvent _event;

            public TreeViewEventBuilder(TreeViewEventHandler<TTreeViewItem, TData, TFilter> handler, TreeViewItemEvent itemEvent)
            {
                _handler = handler;
                _event = new TreeViewEvent();
                WithItemEvent(itemEvent);
            }

            public TreeViewEventBuilder(TreeViewEventHandler<TTreeViewItem, TData, TFilter> handler, string customEvent)
            {
                _handler = handler;
                _event = new TreeViewEvent();
                _event.TreeViewItemEvent = TreeViewItemEvent.Custom;
                _event.MenuName = customEvent;
            }

            public TreeViewEventBuilder WithItemEvent(TreeViewItemEvent itemEvent, bool setDefaultMenuName = true, bool setDefaultConfirmationMessage = true)
            {
                _event.TreeViewItemEvent = itemEvent;
                
                if (setDefaultMenuName)
                {
                    _event.MenuName = itemEvent switch
                    {
                        TreeViewItemEvent.Add => MenuNames.ADD_ITEM,
                        TreeViewItemEvent.Save => MenuNames.SAVE_ITEM,
                        TreeViewItemEvent.Remove => MenuNames.DELETE_ITEM,
                        TreeViewItemEvent.Copy => MenuNames.COPY_ITEM,
                        TreeViewItemEvent.Paste => MenuNames.PASTE_ITEM,
                        TreeViewItemEvent.Revert => MenuNames.REVERT_CHANGES,
                        _ => _event.MenuName
                    };
                }

                if (setDefaultConfirmationMessage)
                {
                    _event.ConfirmationMessage = itemEvent switch
                    {
                        TreeViewItemEvent.Add => ConfirmationMessages.ADD_ITEM,
                        TreeViewItemEvent.Save => ConfirmationMessages.SAVE_ITEM,
                        TreeViewItemEvent.Remove => ConfirmationMessages.DELETE_ITEM,
                        TreeViewItemEvent.Copy => ConfirmationMessages.COPY_ITEM,
                        TreeViewItemEvent.Paste => ConfirmationMessages.PASTE_ITEM,
                        TreeViewItemEvent.Revert => ConfirmationMessages.REVERT_CHANGES,
                        _ => _event.ConfirmationMessage
                    };
                }

                return this;
            }

            public TreeViewEventBuilder WithMenuName(string menuName)
            {
                _event.MenuName = menuName;
                return this;
            }

            public TreeViewEventBuilder WithConfirmationMessage(string confirmationMessage)
            {
                _event.ShowConfirmationMessage = true;
                _event.ConfirmationMessage = confirmationMessage;
                return this;
            }

            public TreeViewEventBuilder ShowConfirmationMessage(bool show = true)
            {
                _event.ShowConfirmationMessage = show;
                return this;
            }
         
            public TreeViewEventBuilder WithAction(Action<TTreeViewItem, Action<bool>> action)
            {
                _event.Action += action;
                return this;
            }

            public TreeViewEventBuilder WithCallback(Action<TTreeViewItem, bool> callback)
            {
                _event.Callback += callback;
                return this;
            }

            public TreeViewEventBuilder WithCondition(Func<TTreeViewItem, bool> visibilityCheck)
            {
                _event.VisibilityCheck += visibilityCheck;
                return this;
            }

            public TreeViewEventBuilder ShowInRightClickMenu(bool show = true)
            {
                _event.ShowInRightClickMenu = show;
                return this;
            }

            public TreeViewEventBuilder ShowInEditWindowMenu(bool show = true)
            {
                _event.ShowInEditWindowMenu = show;
                return this;
            }

            public void Add()
            {
                _handler.AddEvent(_event);
            }
        }
    }

}