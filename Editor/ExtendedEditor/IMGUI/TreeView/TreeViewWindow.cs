using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public abstract partial class TreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewChildWindow, TData, TFilter, TEventHandler> : EditorWindow
        where TSelf : TreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewChildWindow, TData, TFilter, TEventHandler>
        where TTreeView : TreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewChildWindow, TData, TFilter, TEventHandler>.ExtendedTreeView
        where TTreeViewItem : ExtendedTreeViewItem<TData, TFilter>
        where TTreeViewChildWindow : TreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewChildWindow, TData, TFilter, TEventHandler>.TreeViewChildWindow
        where TData : ITreeViewData<TData, TFilter>
        where TFilter : ITreeViewFilter<TFilter, TData>
        where TEventHandler : ITreeViewEventHandler<TTreeViewItem, TData, TFilter>
    {

        private MultiColumnHeader _multiColumnHeader;
        private TTreeView _treeView;
        private TreeViewState _treeViewState;
        private TreeViewToolbar _toolbar;
        public List<TData> AllData { get; private set; }
        public List<TData> FilteredData { get; private set; } = new();
        public TFilter Filter { get; private set; }

        private EPrefs<TFilter> _filterSave;

        private GUIStyle BottomBarStyle
        {
            get
            {
                if (_bottomBarStyle == null)
                {
                    _bottomBarStyle = ExtendedEditorStyles.Border(GUIBorder.Bottom);
                    _bottomBarStyle.fixedHeight = 34;
                }

                return _bottomBarStyle;
            }
        }
        private GUIStyle _bottomBarStyle;

        protected static TSelf Initialize(string name = null)
        {
            name ??= typeof(TSelf).Name;
            TSelf window = (TSelf)GetWindow(typeof(TSelf), false, name);
            window.Show();
            window.autoRepaintOnSceneChange = true;
            return window;
        }


        protected abstract List<TreeViewColumnData> CreateColumns();
        protected abstract IEnumerable<TreeViewToolbarItem> CreateTopToolbar();
        protected abstract IEnumerable<TData> GetAllDataFromSource();
        protected abstract TEventHandler CreateEventHandler();

        protected virtual void OnEnable()
        {
            AllData = GetAllDataFromSource().ToList();
            _toolbar = new TreeViewToolbar(CreateTopToolbar());

            string filterPrefsKey = $"{GetType().Name}.Filter";
            _filterSave = new EPrefs<TFilter>(filterPrefsKey, Activator.CreateInstance<TFilter>());

            RefreshData();
        }

        protected virtual void OnDestroy()
        {
            if (_filterSave != null)
            {
                _filterSave.Value = Filter;
            }
        }

        protected virtual void OnGUI()
        {
            _toolbar.OnGUI();
            DrawTreeView();
            DrawBottomBar();
        }

        protected virtual void DrawBottomBar()
        {
            GUILayout.BeginHorizontal(BottomBarStyle);
            {
                GUILayout.Label($"Showing {FilteredData.Count} of {AllData.Count} items.");
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Reset Filter", GUILayout.Width(100)))
                {
                    FilteredData = null;
                    RefreshData();
                }
            }
            GUILayout.EndHorizontal();
        }

        protected void RefreshData()
        {
            if (AllData == null) return;

            if (Filter == null)
            {
                FilteredData = AllData.ToList();
                return;
            }

            FilteredData.Clear();

            foreach (TData data in AllData)
            {
                bool visible = data.SetFilter(Filter);

                if (visible)
                {
                    FilteredData.Add(data);
                }
            }

            _treeView?.RefreshData();
        }

        protected void SetData(List<TData> data)
        {
            if (data == null) return;
            Debug.Log($"Setting data set of {data.Count} to TreeView.");
            AllData = data;
            RefreshData();
        }

        private void DrawTreeView()
        {
            _treeView ??= CreateThreeView();

            Rect reservedRect = GUILayoutUtility.GetRect(600f, 400f, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            _treeView?.OnGUI(reservedRect);
        }

        private TTreeView CreateThreeView()
        {
            _treeViewState = new TreeViewState();

            if (_multiColumnHeader == null)
            {
                float currentViewWidth = position.width;
                MultiColumnHeaderState.Column[] columns = TreeViewColumnConverter.Convert(currentViewWidth, CreateColumns());
                if (columns == null) return null;

                MultiColumnHeaderState headerState = new(columns);
                _multiColumnHeader = new MultiColumnHeader(headerState);


                if (MultiColumnHeaderState.CanOverwriteSerializedFields(_multiColumnHeader.state, headerState))
                    MultiColumnHeaderState.OverwriteSerializedFields(_multiColumnHeader.state, headerState);
            }

            return Activator.CreateInstance(typeof(TTreeView), this, CreateEventHandler(), _treeViewState, _multiColumnHeader) as TTreeView;
        }
    }
}