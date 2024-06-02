namespace Glitch9.ExtendedEditor.IMGUI
{
    public abstract partial class TreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewChildWindow, TData, TFilter, TEventHandler>
        where TSelf : TreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewChildWindow, TData, TFilter, TEventHandler>
        where TTreeView : TreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewChildWindow, TData, TFilter, TEventHandler>.ExtendedTreeView
        where TTreeViewItem : ExtendedTreeViewItem<TData, TFilter>
        where TTreeViewChildWindow : TreeViewWindow<TSelf, TTreeView, TTreeViewItem, TTreeViewChildWindow, TData, TFilter, TEventHandler>.TreeViewChildWindow
        where TData : ITreeViewData<TData, TFilter>
        where TFilter : ITreeViewFilter<TFilter, TData>
        where TEventHandler : ITreeViewEventHandler<TTreeViewItem, TData, TFilter>
    {

        public abstract class TreeViewChildWindow : PaddedEditorWindow
        {
            public TData Data { get; set; }
        }
    }
}