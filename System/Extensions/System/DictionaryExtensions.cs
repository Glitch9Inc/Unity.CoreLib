using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Glitch9
{
    public static class DictionaryExtensions
    {
        public static bool IsValid<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            return dict != null && dict.Count > 0;
        }

        public static bool IsValid<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            return dict != null && dict.ContainsKey(key) && dict[key] != null;
        }

        public static bool IsNotValid<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            return dict == null || dict.Count == 0;
        }

        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key)) dict[key] = value;
            else dict.Add(key, value);
        }

        public static TValue SafeGet<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default)
        {
            if (key == null) return defaultValue;
            if (dict.TryGetValue(key, out TValue value)) return value;

            if (defaultValue == null)
            {
                if (typeof(TValue) == typeof(string)) return (TValue)(object)string.Empty;
                if (typeof(TValue).IsClass) return Activator.CreateInstance<TValue>();
            }

            return defaultValue;
        }

        public static TValue SafeGetCast<TValue>(this Dictionary<string, object> dict, string key, TValue defaultValue = default)
        {
            if (dict.TryGetValue(key, out object value))
            {
                if (value is TValue castedValue) return castedValue;
                else
                {
                    Debug.LogWarning($"Value {value} is not of type {typeof(TValue)}");
                    return defaultValue;
                }
            }

            return defaultValue;
        }

        public static IEnumerable<TKey> GetKeys<TKey, TValue>(this Dictionary<TKey, TValue> dict, TValue value)
        {
            foreach (KeyValuePair<TKey, TValue> pair in dict)
            {
                if (pair.Value.Equals(value)) yield return pair.Key;
            }
        }

        public static Dictionary<TKey, TValue> Sort<TKey, TValue, TSortKey>(this Dictionary<TKey, TValue> dictionary,
            Func<TKey, TSortKey> keySelector) where TSortKey : IComparable
        {
            // Convert the dictionary to a list of key-value pairs
            List<KeyValuePair<TKey, TValue>> sortedList = dictionary.ToList();

            // Sort the list based on the provided key selector
            sortedList.Sort((pair1, pair2) => keySelector(pair1.Key).CompareTo(keySelector(pair2.Key)));

            // Convert the sorted list back to a dictionary
            Dictionary<TKey, TValue> sortedDictionary = sortedList.ToDictionary(pair => pair.Key, pair => pair.Value);

            return sortedDictionary;
        }
    }
}