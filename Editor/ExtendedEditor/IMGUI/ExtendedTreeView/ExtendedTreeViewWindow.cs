using System;
using System.Collections.Generic;
using System.Linq;
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
        where TData : class, ITreeViewData<TData, TFilter>
        where TFilter : class, ITreeViewFilter<TFilter, TData>
        where TEventHandler : TreeViewEventHandler<TTreeViewItem, TData, TFilter>
    {

        protected MultiColumnHeader MultiColumnHeader;
        protected TTreeView TreeView;
        protected TreeViewState TreeViewState;

        public List<TData> AllData { get; private set; }
        public List<TData> FilteredData { get; private set; } = new();
        public TreeViewToolbar Toolbar { get; private set; }
        public TFilter Filter { get; set; }

        private EPrefs<TFilter> _filterSave;


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
            Toolbar = new TreeViewToolbar(CreateTopToolbar(), OnSearchStringUpdated);

            string filterPrefsKey = $"{GetType().Name}.Filter";
            _filterSave = new EPrefs<TFilter>(filterPrefsKey, Activator.CreateInstance<TFilter>());

            RefreshData();
        }

        private void OnSearchStringUpdated()
        {
            TreeView.CustomReload();
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
            Toolbar.OnGUI(position);
            DrawTreeView();
            DrawBottomBar();
        }

        internal void RefreshData()
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

            TreeView?.RefreshData();
        }

        protected void SetData(List<TData> data)
        {
            if (data == null) return;
            Debug.Log($"Setting data set of {data.Count} to TreeView.");
            AllData = data;
            RefreshData();
        }

        public void UpdateData(TData data)
        {
            if (data == null) return;
            Debug.Log($"Updating data {data.Id} in TreeView.");
            TData existingData = AllData.FirstOrDefault(d => d.Id == data.Id);
            if (existingData == null)
            {
                Debug.LogError($"Data with id {data.Id} not found in TreeView.");
                return;
            }

            // replace the existing data with the new data
            int index = AllData.IndexOf(existingData);
            AllData[index] = data;

            // update the filtered data
            if (FilteredData.Contains(existingData))
            {
                int filteredIndex = FilteredData.IndexOf(existingData);
                FilteredData[filteredIndex] = data;
            }

            RefreshData();
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

        public void RemoveItem(TTreeViewItem item)
        {
            if (item == null) return;

            if (item.Data != null)
            {
                AllData.Remove(item.Data);
                FilteredData.Remove(item.Data);
            }

            TreeView?.RemoveItem(item);
        }
    }
}