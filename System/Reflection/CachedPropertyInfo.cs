using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Glitch9
{
    public static class CachedPropertyInfo
    {
        private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> k_TypePropertyInfoCache = new();

        public static List<PropertyInfo> Get<T>()
        {
            return Get(typeof(T));
        }

        public static List<PropertyInfo> Get(Type type)
        {
            if (k_TypePropertyInfoCache.TryGetValue(type, out List<PropertyInfo> propertyInfos))
            {
                return propertyInfos;
            }

            propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            k_TypePropertyInfoCache.TryAdd(type, propertyInfos);

            return propertyInfos;
        }
    }
}