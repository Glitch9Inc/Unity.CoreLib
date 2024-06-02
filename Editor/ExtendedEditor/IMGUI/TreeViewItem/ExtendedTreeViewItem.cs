using UnityEditor.IMGUI.Controls;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public class ExtendedTreeViewItem<TData, TFilter> : TreeViewItem
        where TData : ITreeViewData<TData, TFilter>
        where TFilter : ITreeViewFilter<TFilter, TData>
    {
        public TData Data { get; private set; }

        public ExtendedTreeViewItem(int id, int depth, string displayName, TData data) : base(id, depth, displayName)
        {
            Data = data;
        }
    }
}