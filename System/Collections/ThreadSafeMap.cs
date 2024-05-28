using System;
using System.Collections.Concurrent;

namespace Glitch9.Collections
{
    public class ThreadSafeMap<TKey, TValue> 
        where TKey : notnull
    {
        private readonly ConcurrentDictionary<TKey, TValue> _concurrentStore;
        private readonly Func<TKey, TValue> _creator;

        public ThreadSafeMap(Func<TKey, TValue> creator)
        {
            Validate.Argument.NotNull(creator, nameof(creator));

            _creator = creator;
            _concurrentStore = new ConcurrentDictionary<TKey, TValue>();
        }

        public TValue Get(TKey key)
        {
            return _concurrentStore.GetOrAdd(key, _creator);
        }
    }
}