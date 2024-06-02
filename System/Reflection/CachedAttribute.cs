using Glitch9.Collections;
using System;

namespace Glitch9
{
    public static class CachedAttribute<T> where T : Attribute
    {
        private static readonly ThreadSafeMap<object, T> k_TypeAttributeCache = new(ReflectionUtils.GetAttribute<T>);

        public static T Get(object type)
        {
            return k_TypeAttributeCache.Get(type);
        }
    }
}