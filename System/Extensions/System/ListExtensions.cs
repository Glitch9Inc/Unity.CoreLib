using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Glitch9
{
    public static class ListExtensions
    {
        public static bool IsValid<T>(this List<T> list)
        {
            return list != null && list.Count > 0;
        }

        public static bool IsInvalid<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static bool IsValid<T>(this HashSet<T> hashSet)
        {
            return hashSet != null && hashSet.Count > 0;
        }

        public static bool IsInvalid<T>(this HashSet<T> hashSet)
        {
            return hashSet == null || hashSet.Count == 0;
        }

        public static bool IsValid<T>(this IEnumerable<T> collection)
        {
            return collection != null && collection.Any();
        }

        public static bool IsInvalid<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static bool IsValid<T>(this Queue<T> queue)
        {
            return queue != null && queue.Count > 0;
        }

        public static bool IsInvalid<T>(this Queue<T> queue)
        {
            return queue == null || queue.Count == 0;
        }

        public static void SwapElements<T>(this List<T> list, int index1, int index2)
        {
            (list[index1], list[index2]) = (list[index2], list[index1]);
        }

        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0) return default;
            return list[Random.Range(0, list.Count)];
        }

        public static Stack<T> ToStack<T>(this IList<T> list)
        {
            Stack<T> stack = new();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                stack.Push(list[i]);
            }
            return stack;
        }

        public static T TryGet<T>(this List<T> list, int index)
        {
            if (list == null || list.Count <= index) return default;
            return list[index];
        }
    }
}