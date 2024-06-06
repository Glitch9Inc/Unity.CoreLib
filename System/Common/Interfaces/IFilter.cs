namespace Glitch9
{
    public interface IFilter<TSelf, in TData>
        where TSelf : class, IFilter<TSelf, TData>
        where TData : class, IData<TData>
    {
        bool IsFiltered(TData data);
    }
}