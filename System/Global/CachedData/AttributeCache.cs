using Glitch9.Collections;
using Glitch9.Reflection;
using System;
using System.Collections.Generic;

namespace Glitch9
{
    public static class AttributeCache<T> where T : Attribute
    {
        private static readonly ThreadSafeMap<object, T> k_TypeAttributeCache = new(ReflectionUtils.GetAttribute<T>);

        public static T Get(object type)
        {
            return k_TypeAttributeCache.Get(type);
        }

        public static Dictionary<object, T> GetDictionary()
        {
            return k_TypeAttributeCache.GetDictionary();
        }
    }
}