using System;
using Glitch9.Collections;

namespace Glitch9.ScriptableObjects
{
    
    [Serializable]
    public class Repository<TData> : ReferencedDictionary<RepositoryEntry<TData>, string, TData>
        where TData : class, IData<TData>, new()
    {
    }
}