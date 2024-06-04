using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public abstract partial class ExtendedTreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler> : EditorWindow
        where TSelf : ExtendedTreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>
        where TTreeView : ExtendedTreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>.ExtendedTreeView
        where TTreeViewItem : ExtendedTreeViewItem<TTreeViewItem, TData, TFilter>
        where TTreeViewEditWindow : ExtendedTreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>.ExtendedTreeViewEditWindow
        where TData : class, ITreeViewData<TData>
        where TFilter : class, ITreeViewFilter<TFilter, TData>
        where TEventHandler : TreeViewEventHandler<TTreeViewItem, TData, TFilter>
    {
        protected MultiColumnHeader MultiColumnHeader;
        protected TTreeView TreeView;
        protected TreeViewState TreeViewState;

        /// <summary>
        /// Often accessed from ExtendedTreeView
        /// </summary>
        public TreeViewMenu Menu { get; private set; }


        protected static TSelf Initialize(string name = null)
        {
            name ??= typeof(TSelf).Name;
            TSelf window = (TSelf)GetWindow(typeof(TSelf), false, name);
            window.Show();
            window.autoRepaintOnSceneChange = true;
            return window;
        }


        protected abstract List<TreeViewColumnData> CreateColumns();
        protected abstract IEnumerable<TreeViewMenuItem> CreateTopToolbar();
        protected abstract TEventHandler CreateEventHandler();

        protected virtual void OnEnable()
        {
            Menu = new TreeViewMenu(CreateTopToolbar(), TreeView.OnSearchStringChanged);
        }

        protected virtual void OnDestroy()
        {
            TreeView.OnDestroy();
        }

        protected virtual void OnGUI()
        {
            Menu.OnGUI(position);
            DrawTreeView();
            DrawBottomBar();
        }

        private void DrawTreeView()
        {
            TreeView ??= CreateThreeView();

            Rect reservedRect = GUILayoutUtility.GetRect(600f, 400f, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            TreeView?.OnGUI(reservedRect);
        }

        private TTreeView CreateThreeView()
        {
            TreeViewState = new TreeViewState();

            if (MultiColumnHeader == null)
            {
                float currentViewWidth = position.width;
                MultiColumnHeaderState.Column[] columns = TreeViewColumnConverter.Convert(currentViewWidth, CreateColumns());
                if (columns == null) return null;

                MultiColumnHeaderState headerState = new(columns);
                MultiColumnHeader = new MultiColumnHeader(headerState);

                if (MultiColumnHeaderState.CanOverwriteSerializedFields(MultiColumnHeader.state, headerState))
                    MultiColumnHeaderState.OverwriteSerializedFields(MultiColumnHeader.state, headerState);
            }

            return Activator.CreateInstance(typeof(TTreeView), this, CreateEventHandler(), TreeViewState, MultiColumnHeader) as TTreeView;
        }

        protected bool NullCheckItem(TTreeViewItem item)
        {
            if (item == null) return false;
            if (item.Data == null) return false;
            if (string.IsNullOrEmpty(item.Data.Id)) return false;
            return true;
        }
    }
}