using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public abstract partial class ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler> : EditorWindow
        where TTreeViewWindow : ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>
        where TTreeView : ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>.ExtendedTreeView
        where TTreeViewItem : ExtendedTreeViewItem<TTreeViewItem, TData, TFilter>
        where TTreeViewEditWindow : ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>.ExtendedTreeViewEditWindow
        where TData : class, IData<TData>
        where TFilter : class, IFilter<TFilter, TData>
        where TEventHandler : TreeViewEventHandler<TTreeViewItem, TData, TFilter>
    {
        private const int MAX_INIT_COUNT = 3;
        private const float BOTTOM_BAR_BTN_WIDTH = 100;

        protected MultiColumnHeader MultiColumnHeader;
        protected TTreeView TreeView;
        protected TreeViewState TreeViewState;

        /// <summary>
        /// Often accessed from ExtendedTreeView
        /// </summary>
        public TreeViewMenu Menu { get; private set; }

        private bool _isInitialized = false;
        private int _initCount = 0;

        protected static TTreeViewWindow InitializeWindow(string name = null)
        {
            name ??= typeof(TTreeViewWindow).Name;
            TTreeViewWindow window = (TTreeViewWindow)GetWindow(typeof(TTreeViewWindow), false, name);
            window.Show();
            window.autoRepaintOnSceneChange = true;
            return window;
        }

        protected abstract List<TreeViewColumnData> CreateColumns();
        protected abstract IEnumerable<TreeViewMenuItem> CreateTopToolbar();
        protected abstract TEventHandler CreateEventHandler();

        private void Initialize()
        {
            if (_isInitialized || _initCount >= MAX_INIT_COUNT) return;
            _isInitialized = true;
            _initCount++;
            TreeView = CreateThreeView();
            Menu = new TreeViewMenu(CreateTopToolbar(), TreeView.OnSearchStringChanged);
        }

        protected virtual void OnDestroy()
        {
            TreeView.OnDestroy();
        }

        protected virtual void OnGUI()
        {
            try
            {
                Initialize();
                DrawMenu();
                DrawTreeView();
                DrawBottomBar();
            }
            catch (Exception e)
            {
                _isInitialized = false;
                EditorGUILayout.HelpBox(e.Message, MessageType.Error);
                
                if (GUILayout.Button("Show Error"))
                {
                    Debug.LogError(e);
                }

                if (GUILayout.Button("Try Again"))
                {
                    _initCount = 0;
                }
            }
        }

        private void DrawMenu()
        {
            if (Menu == null) throw new NullReferenceException("Tree View Menu is null");
            Menu.OnGUI(position);
        }

        private void DrawTreeView()
        {
            if (TreeView == null) throw new NullReferenceException("Tree View is null");
            Rect reservedRect = GUILayoutUtility.GetRect(600f, 400f, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            TreeView.OnGUI(reservedRect);
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
        
        private void DrawBottomBar()
        {
            if (TreeView == null) return;

            GUILayout.BeginHorizontal(TreeViewStyles.BottomBarStyle);
            {
                GUILayout.Label($"Showing {TreeView.ShowingCount} of {TreeView.TotalCount} items.");
                GUILayout.FlexibleSpace();
                BottomBar();
            }
            GUILayout.EndHorizontal();
        }


        /// <summary>
        /// Override this method to add custom bottom bar
        /// </summary>
        protected virtual void BottomBar()
        {

            if (GUILayout.Button("Reset Filter", GUILayout.Width(BOTTOM_BAR_BTN_WIDTH)))
            {
                TreeView.ResetFilter();
            }

            if (GUILayout.Button("Reload Data", GUILayout.Width(BOTTOM_BAR_BTN_WIDTH)))
            {
                TreeView.UpdateTreeView();
            }
        }
    }
}