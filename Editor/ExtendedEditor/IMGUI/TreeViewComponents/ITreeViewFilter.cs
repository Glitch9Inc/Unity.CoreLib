namespace Glitch9.ExtendedEditor.IMGUI
{
    public interface ITreeViewFilter<TSelf, in TData>
        where TSelf : ITreeViewFilter<TSelf, TData>
        where TData : ITreeViewData<TData, TSelf>
    {
        bool SetFilter(TData data);
    }
}