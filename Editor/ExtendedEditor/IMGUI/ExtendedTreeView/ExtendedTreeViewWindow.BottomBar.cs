using UnityEditor;
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
        private const float BOTTOM_BAR_BTN_WIDTH = 100;
        
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

        public void DrawBottomBar()
        {
            GUILayout.BeginHorizontal(BottomBarStyle);
            {
                GUILayout.Label($"Showing {TreeView.ShowingCount} of {TreeView.TotalCount} items.");
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Reset Filter", GUILayout.Width(BOTTOM_BAR_BTN_WIDTH)))
                {
                    TreeView.ResetFilter();
                }

                if (GUILayout.Button("Reload Data", GUILayout.Width(BOTTOM_BAR_BTN_WIDTH)))
                {
                    TreeView.UpdateTreeView();
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}