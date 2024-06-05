using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public abstract partial class ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler> : EditorWindow
        where TTreeViewWindow : ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>
        where TTreeView : ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>.ExtendedTreeView
        where TTreeViewItem : ExtendedTreeViewItem<TTreeViewItem, TData, TFilter>
        where TTreeViewEditWindow : ExtendedTreeViewWindow<TTreeViewWindow, TTreeView, TTreeViewItem, TTreeViewEditWindow, TData, TFilter, TEventHandler>.ExtendedTreeViewEditWindow
        where TData : class, ITreeViewData<TData>
        where TFilter : class, ITreeViewFilter<TFilter, TData>
        where TEventHandler : TreeViewEventHandler<TTreeViewItem, TData, TFilter>
    {
        protected const float BOTTOM_BAR_BTN_WIDTH = 100;

        protected virtual void DrawBottomBar()
        {
            if (TreeView == null) return;
            
            GUILayout.BeginHorizontal(TreeViewStyles.BottomBarStyle);
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