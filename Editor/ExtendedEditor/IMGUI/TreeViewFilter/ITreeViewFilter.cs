namespace Glitch9.ExtendedEditor.IMGUI
{
    public interface ITreeViewFilter<TSelf, in TData>
        where TSelf : class, ITreeViewFilter<TSelf, TData>
        where TData : class, ITreeViewData<TData>
    {
        bool SetFilter(TData data);
    }
}