using System;
using Glitch9.Collections;
using UnityEngine;

namespace Glitch9.ScriptableObjects
{
    [Serializable]
    public class RepositoryEntry<TData> : IKeyValuePair<string, TData>
        where TData : class, IData<TData>, new()
    {
        public string Key
        {
            get => key;
            set => key = value;
        }

        public TData Value
        {
            get => value;
            set => this.value = value;
        }

        public string key;
        [SerializeReference] public TData value;

        public RepositoryEntry()
        {
        }

        public RepositoryEntry(TData data)
        {
            key = data.Id;
            value = data;
        }

        public override string ToString()
        {
            return $"{key}: {value}";
        }
    }
}