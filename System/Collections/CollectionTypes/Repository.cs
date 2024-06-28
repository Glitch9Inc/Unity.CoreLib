using Glitch9.Collections;
using System;

namespace Glitch9.ScriptableObjects
{

    [Serializable]
    public class Repository<TData> : ReferencedDictionary<string, TData>
        where TData : class, IData<TData>, new()
    {
    }
}