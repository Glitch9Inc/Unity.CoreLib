using System;

namespace Glitch9.Caching
{
    public class CacheItem<T>
    {
        public T Value { get; set; }
        public DateTime Expiration { get; set; }

        public CacheItem(T value, DateTime expiration)
        {
            Value = value;
            Expiration = expiration;
        }
    }
}