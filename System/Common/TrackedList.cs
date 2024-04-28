using System;
using System.Collections.Generic;

namespace Glitch9
{
    public class TrackedList<T> : List<T>
    {
        public event Action<T> OnAdd;
        public event Action<T> OnRemove;
        public event Action OnClear;

        public new void Add(T item)
        {
            base.Add(item);
            OnAdd?.Invoke(item);
        }

        public new void Remove(T item)
        {
            base.Remove(item);
            OnRemove?.Invoke(item);
        }

        public new void Clear()
        {
            base.Clear();
            OnClear?.Invoke();
        }
    }
}