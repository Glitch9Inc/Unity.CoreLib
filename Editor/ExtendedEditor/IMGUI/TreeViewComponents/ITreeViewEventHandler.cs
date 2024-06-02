using System;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public interface ITreeViewEventHandler<in TTreeViewItem, TData, TFilter>
        where TTreeViewItem : ExtendedTreeViewItem<TData, TFilter>
        where TData : ITreeViewData<TData, TFilter>
        where TFilter : ITreeViewFilter<TFilter, TData>
    {
        public bool EmptyActions
        {
            get
            {
                bool empty = true;
                empty &= CopyItem == null;
                empty &= PasteItem == null;
                empty &= DeleteItem == null;
                // Add more here...

                return empty;
            }
        }

        // Actions
        Action<TTreeViewItem> CopyItem { get; }
        Action<TTreeViewItem, Action<bool>> PasteItem { get; }
        Action<TTreeViewItem, Action<bool>> DeleteItem { get; }
    }
}