using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public abstract partial class ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>
        where TTreeViewWindow : ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>
        where TTreeView : ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>.ExtendedTreeView
        where TTreeViewItem : ExtendedTreeViewItem<TTreeViewItem, TData, TFilter>
        where TTreeViewEditWindow : ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>.ExtendedTreeViewEditWindow
        where TData : class, IData<TData>
        where TFilter : class, IFilter<TFilter, TData>
        where TEventHandler : TreeViewEventHandler<TTreeViewItem, TData, TFilter>
    {
        /// <summary>
        /// Base class for child windows of the TreeViewWindow
        /// </summary>
        public abstract class ExtendedTreeViewEditWindow : PaddedEditorWindow
        {
            private static class GUIContents
            {
                internal static readonly GUIContent k_ToolMenu = new(EditorIcons.Menu, "Open the tool menu");
            }

            protected const string FALLBACK_TITLE = "Unknown Item";
            protected const float MIN_TEXT_FIELD_HEIGHT = 20;
            public TTreeViewItem Item { get; set; }
            public TTreeView TreeView { get; set; }
            public TEventHandler EventHandler { get; set; }

            public TData Data => Item?.Data;
            public TData UpdatedData { get; set; }
            public bool IsDirty => !Data.Equals(UpdatedData);
            public GUIContent Title { get; set; }
            
            private bool _isInitialized = false;
            private Vector2 _scrollPosition;

            public void SetData(TTreeViewItem item, TTreeView treeView, TEventHandler eventHandler)
            {
                Item = item;
                TreeView = treeView;
                EventHandler = eventHandler;
                Initialize();
            }

            protected virtual void Initialize()
            {
                if (_isInitialized) return;
                _isInitialized = true;
                CreateTitle();
                UpdatedData = Data;
            }

            protected virtual void CreateTitle()
            {
                Title = new GUIContent(!string.IsNullOrEmpty(Data.Name) ? Data.Name : FALLBACK_TITLE);
            }

            protected abstract void DrawSubtitle();
            protected abstract void DrawBody();


            protected override void OnGUIUpdate()
            {
                Initialize();

                if (Data == null)
                {
                    EditorGUILayout.LabelField("Data is null");
                    return;
                }

                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label(Title, TreeViewStyles.ChildWindowTitle);
                        GUILayout.FlexibleSpace();
                        DrawToolMenuButton();
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(5);

                    DrawSubtitle();

                    GUILayout.BeginVertical(TreeViewStyles.EditWindowBody);
                    {
                        DrawBody();
                    }
                    GUILayout.EndVertical();
                }
                GUILayout.EndScrollView();
            }

            public void RevertChanges()
            {
                UpdatedData = Data;
            }

            private void RepaintWindow(IResult result)
            {
                if (result.IsSuccess)
                {
                    Repaint();
                    if (result.IsUpdated) TreeView.UpdateData(Item.Data);
                }
            }

            private void DrawToolMenuButton()
            {
                if (GUILayout.Button(GUIContents.k_ToolMenu, EGUIStyles.miniButton))
                {
                    if (EventHandler == null) return;
                    EventHandler.ShowEditWindowMenu(Item, RepaintWindow);
                }
            }
        }
    }
}