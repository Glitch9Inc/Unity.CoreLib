using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Glitch9
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines whether the collection is <c>null</c> or empty.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns><c>true</c> if the collection is <c>null</c> or empty; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty<T>(ICollection<T> collection)
        {
            if (collection != null)
            {
                return (collection.Count == 0);
            }
            return true;
        }

        /// <summary>
        /// Determines whether the list is valid (not null and contains elements).
        /// </summary>
        public static bool IsValid<T>(this IList<T> list)
        {
            return list != null && list.Count > 0;
        }

        /// <summary>
        /// Determines whether the hash set is <c>null</c> or empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this HashSet<T> hashSet)
        {
            return hashSet == null || hashSet.Count == 0;
        }

        /// <summary>
        /// Determines whether the collection is valid (not null and contains elements).
        /// </summary>
        public static bool IsValid<T>(this IEnumerable<T> collection)
        {
            return collection != null && collection.Any();
        }

        /// <summary>
        /// Determines whether the collection is <c>null</c> or empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        /// <summary>
        /// Determines whether the queue is valid (not null and contains elements).
        /// </summary>
        public static bool IsValid<T>(this Queue<T> queue)
        {
            return queue != null && queue.Count > 0;
        }

        /// <summary>
        /// Determines whether the queue is <c>null</c> or empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this Queue<T> queue)
        {
            return queue == null || queue.Count == 0;
        }

        /// <summary>
        /// Adds a collection of elements to a hash set.
        /// </summary>
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> range)
        {
            foreach (T obj in range)
                hashSet.Add(obj);
        }

        /// <summary>
        /// Determines whether the list is <c>null</c> or empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return list == null || list.Count == 0;;
        }

        /// <summary>
        /// Sets all items in the list to the given value.
        /// </summary>
        public static void Populate<T>(this IList<T> list, T item)
        {
            int count = list.Count;
            for (int index = 0; index < count; ++index)
                list[index] = item;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the IList&lt;T&gt;.
        /// </summary>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> collection)
        {
            if (list is List<T> list1)
            {
                list1.AddRange(collection);
            }
            else
            {
                foreach (T obj in collection)
                    list.Add(obj);
            }
        }

        /// <summary>
        /// Sorts an IList using a specified comparison.
        /// </summary>
        public static void Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (list is List<T> list1)
            {
                list1.Sort(comparison);
            }
            else
            {
                List<T> objList = new((IEnumerable<T>)list);
                objList.Sort(comparison);
                for (int index = 0; index < list.Count; ++index)
                    list[index] = objList[index];
            }
        }

        /// <summary>
        /// Sorts an IList.
        /// </summary>
        public static void Sort<T>(this IList<T> list)
        {
            if (list is List<T>)
            {
                ((List<T>)list).Sort();
            }
            else
            {
                List<T> objList = new((IEnumerable<T>)list);
                objList.Sort();
                for (int index = 0; index < list.Count; ++index)
                    list[index] = objList[index];
            }
        }

        /// <summary>
        /// Determines whether the specified type is a dictionary type.
        /// </summary>
        public static bool IsDictionaryType(Type type)
        {
            ThrowIf.ArgumentIsNull(type, nameof(type));

            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                return true;
            }
            if (ReflectionUtils.ImplementsGenericDefinition(type, typeof(IDictionary<,>)))
            {
                return true;
            }

            if (ReflectionUtils.ImplementsGenericDefinition(type, typeof(IReadOnlyDictionary<,>)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Resolves the constructor for a collection type that takes an IEnumerable of the item type.
        /// </summary>
        public static ConstructorInfo ResolveEnumerableCollectionConstructor(Type collectionType, Type collectionItemType)
        {
            Type genericConstructorArgument = typeof(IList<>).MakeGenericType(collectionItemType);
            return ResolveEnumerableCollectionConstructor(collectionType, collectionItemType, genericConstructorArgument);
        }

        /// <summary>
        /// Resolves the constructor for a collection type that takes a specific type of argument.
        /// </summary>
        public static ConstructorInfo ResolveEnumerableCollectionConstructor(Type collectionType, Type collectionItemType, Type constructorArgumentType)
        {
            Type genericEnumerable = typeof(IEnumerable<>).MakeGenericType(collectionItemType);
            ConstructorInfo match = null;

            foreach (ConstructorInfo constructor in collectionType.GetConstructors(BindingFlags.Public | BindingFlags.Instance))
            {
                IList<ParameterInfo> parameters = constructor.GetParameters();

                if (parameters.Count == 1)
                {
                    Type parameterType = parameters[0].ParameterType;

                    if (genericEnumerable == parameterType)
                    {
                        // Exact match
                        match = constructor;
                        break;
                    }

                    // Use the first inexact match if exact match not found
                    if (match == null)
                    {
                        if (parameterType.IsAssignableFrom(constructorArgumentType))
                        {
                            match = constructor;
                        }
                    }
                }
            }

            return match;
        }

        /// <summary>
        /// Adds an element to the list if it does not already contain it, based on the default equality comparer.
        /// </summary>
        public static bool AddDistinct<T>(this IList<T> list, T value)
        {
            return list.AddDistinct(value, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Adds an element to the list if it does not already contain it, based on the specified equality comparer.
        /// </summary>
        public static bool AddDistinct<T>(this IList<T> list, T value, IEqualityComparer<T> comparer)
        {
            if (list.ContainsValue(value, comparer))
            {
                return false;
            }

            list.Add(value);
            return true;
        }

        /// <summary>
        /// Determines whether the collection contains the specified value, using the specified equality comparer.
        /// </summary>
        public static bool ContainsValue<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TSource>.Default;
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (TSource local in source)
            {
                if (comparer.Equals(local, value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds a range of elements to the list, ensuring each element is added only once.
        /// </summary>
        public static bool AddRangeDistinct<T>(this IList<T> list, IEnumerable<T> values, IEqualityComparer<T> comparer)
        {
            bool allAdded = true;
            foreach (T value in values)
            {
                if (!list.AddDistinct(value, comparer))
                {
                    allAdded = false;
                }
            }

            return allAdded;
        }

        /// <summary>
        /// Finds the index of the first element in the collection that matches the specified predicate.
        /// </summary>
        public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            int index = 0;
            foreach (T value in collection)
            {
                if (predicate(value))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Determines whether the list contains the specified value, using the specified equality comparer.
        /// </summary>
        public static bool Contains<T>(this List<T> list, T value, IEqualityComparer comparer)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (comparer.Equals(value, list[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Finds the index of the specified item in the list by reference equality.
        /// </summary>
        public static int IndexOfReference<T>(this List<T> list, T item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (ReferenceEquals(item, list[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets the dimensions of a jagged array.
        /// </summary>
        private static IList<int> GetDimensions(IList values, int dimensionsCount)
        {
            IList<int> dimensions = new List<int>();

            IList currentArray = values;
            while (true)
            {
                dimensions.Add(currentArray.Count);

                // Don't keep calculating dimensions for arrays inside the value array
                if (dimensions.Count == dimensionsCount)
                {
                    break;
                }

                if (currentArray.Count == 0)
                {
                    break;
                }

                object v = currentArray[0];
                if (v is IList list)
                {
                    currentArray = list;
                }
                else
                {
                    break;
                }
            }

            return dimensions;
        }

        /// <summary>
        /// Copies values from a jagged array to a multidimensional array.
        /// </summary>
        private static void CopyFromJaggedToMultidimensionalArray(IList values, Array multidimensionalArray, int[] indices)
        {
            int dimension = indices.Length;
            if (dimension == multidimensionalArray.Rank)
            {
                multidimensionalArray.SetValue(JaggedArrayGetValue(values, indices), indices);
                return;
            }

            int dimensionLength = multidimensionalArray.GetLength(dimension);
            IList list = (IList)JaggedArrayGetValue(values, indices);
            int currentValuesLength = list.Count;
            if (currentValuesLength != dimensionLength)
            {
                throw new Exception("Cannot deserialize non-cubical array as multidimensional array.");
            }

            int[] newIndices = new int[dimension + 1];
            for (int i = 0; i < dimension; i++)
            {
                newIndices[i] = indices[i];
            }

            for (int i = 0; i < multidimensionalArray.GetLength(dimension); i++)
            {
                newIndices[dimension] = i;
                CopyFromJaggedToMultidimensionalArray(values, multidimensionalArray, newIndices);
            }
        }

        /// <summary>
        /// Gets a value from a jagged array.
        /// </summary>
        private static object JaggedArrayGetValue(IList values, int[] indices)
        {
            IList currentList = values;
            for (int i = 0; i < indices.Length; i++)
            {
                int index = indices[i];
                if (i == indices.Length - 1)
                {
                    return currentList[index]!;
                }
                else
                {
                    currentList = (IList)currentList[index]!;
                }
            }
            return currentList;
        }

        /// <summary>
        /// Converts a jagged array to a multidimensional array.
        /// </summary>
        public static Array ToMultidimensionalArray(IList values, Type type, int rank)
        {
            IList<int> dimensions = GetDimensions(values, rank);

            while (dimensions.Count < rank)
            {
                dimensions.Add(0);
            }

            Array multidimensionalArray = Array.CreateInstance(type, dimensions.ToArray());
            CopyFromJaggedToMultidimensionalArray(values, multidimensionalArray, Array.Empty<int>());

            return multidimensionalArray;
        }

        /// <summary>
        /// Swaps elements in a list at the specified indices.
        /// </summary>
        public static void SwapElements<T>(this IList<T> list, int index1, int index2)
        {
            (list[index1], list[index2]) = (list[index2], list[index1]);
        }

        /// <summary>
        /// Gets a random element from the list.
        /// </summary>
        public static T GetRandom<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0) return default;
            return list[Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Converts a list to a stack.
        /// </summary>
        public static Stack<T> ToStack<T>(this IList<T> list)
        {
            Stack<T> stack = new();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                stack.Push(list[i]);
            }
            return stack;
        }

        /// <summary>
        /// Tries to get an element from the list at the specified index.
        /// </summary>
        public static T TryGet<T>(this IList<T> list, int index)
        {
            if (list == null || list.Count <= index) return default;
            return list[index];
        }

        public static bool IsValid<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            return !dict.IsNullOrEmpty();
        }

        public static bool IsValid<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            return dict != null && dict.ContainsKey(key) && dict[key] != null;
        }

        public static bool IsNullOrEmpty<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            return dict == null || dict.Count == 0;
        }

        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key)) dict[key] = value;
            else dict.Add(key, value);
        }

        public static TValue SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default)
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

        public static TValue SafeGetCast<TValue>(this IDictionary<string, object> dict, string key, TValue defaultValue = default)
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

        public static IEnumerable<TKey> GetKeys<TKey, TValue>(this IDictionary<TKey, TValue> dict, TValue value)
        {
            foreach (KeyValuePair<TKey, TValue> pair in dict)
            {
                if (pair.Value.Equals(value)) yield return pair.Key;
            }
        }

        public static Dictionary<TKey, TValue> SetNullIfEmpty<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            if (dict.IsNullOrEmpty()) dict = null;
            return dict;
        }

        public static List<T> SetNullIfEmpty<T>(this List<T> list)
        {
            if (list.IsNullOrEmpty()) list = null;
            return list;
        }
    }
}
