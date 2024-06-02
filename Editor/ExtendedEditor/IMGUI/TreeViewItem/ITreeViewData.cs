namespace Glitch9.ExtendedEditor.IMGUI
{
    public interface ITreeViewData<TSelf, in TFilter>
        where TSelf : ITreeViewData<TSelf, TFilter>
        where TFilter : ITreeViewFilter<TFilter, TSelf>
    {
        string Id { get; }
        bool SetFilter(TFilter filter);
    }
}