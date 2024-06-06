using System;
using System.Collections.Generic;

namespace Glitch9
{
    public partial class Validate
    {
        public static class Collection
        {
            private const string k_DefaultCollectionName = "Collection";
            private const string k_DefaultArrayName = "Array";

            public static void NotNullOrEmpty<T>(ICollection<T> collection, string collectionName = k_DefaultCollectionName)
            {
                if (collection == null || collection.Count == 0) throw new ArgumentNullException(collectionName);
            }

            public static void NotNullOrEmpty<T>(IList<T> array, string arrayName = k_DefaultArrayName)
            {
                if (array == null || array.Count == 0) throw new ArgumentNullException(arrayName);
            }
        }
    }
}