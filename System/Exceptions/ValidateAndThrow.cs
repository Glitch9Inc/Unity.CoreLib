using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Glitch9
{
    /// <summary>
    /// Validate objects and throw exceptions when a condition is not met.
    /// </summary>
    public class ValidateAndThrow
    {
        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if the value is null.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ArgumentNotNull([NotNull] object value, string paramName)
        {
            if (value == null) throw new ArgumentNullException(paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if the value is null.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ArgumentNotNull<T>([NotNull] T value)
        {
            if (value != null) return;
            string paramName = typeof(T).Name;
            throw new ArgumentNullException(paramName);
        }

        public static void ArgumentNotNull(params object[] values)
        {
            foreach (object value in values)
            {
                if (value == null)
                {
                    string paramName = value.GetType().Name;
                    throw new ArgumentNullException(paramName);
                }
            }
        }
  
        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if the string is null or empty.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void StringNotNullOrEmpty(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value)) throw new StringNullOrEmptyException(paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if the string is null or whitespace.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void StringNotNullOrWhitespace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new StringNullOrWhiteSpaceException(paramName);
        }

        public static void NotDefault<T>(T value, string paramName)
        {
            if (value.Equals(default(T))) throw new ArgumentNullException(paramName);
        }

        public static void NotLessThanZero(int value, string paramName)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(paramName);
        }

        public static void NotLessThanZero(float value, string paramName)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(paramName);
        }

        public static void NotLessThanZero(double value, string paramName)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(paramName);
        }

        public static void NotLessThanZero(decimal value, string paramName)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(paramName);
        }

        public static void NotLessThanZero(long value, string paramName)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(paramName);
        }
        

        private const string k_Endpoint = "Endpoint";
        public static void EndpointNotNull([NotNull] string value, string endpointName = k_Endpoint)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(endpointName);
        }

        private const string k_DefaultCurrencyName = "Funds";
        public static void InsufficientCurrency(float balance, float amount, string currencyName = k_DefaultCurrencyName)
        {
            if (balance < amount)
            {
                string message = $"Insufficient {currencyName.ToLower()}.";
                throw new InvalidOperationException(message);
            }
        }

        private const string k_DefaultCollectionName = "Collection";
        private const string k_DefaultArrayName = "Array";

        public static void CollectionNotNullOrEmpty<T>(ICollection<T> collection, string collectionName = k_DefaultCollectionName)
        {
            if (collection == null || collection.Count == 0) throw new ArgumentNullException(collectionName);
        }

        public static void ListNotNullOrEmpty<T>(IList<T> array, string arrayName = k_DefaultArrayName)
        {
            if (array == null || array.Count == 0) throw new ArgumentNullException(arrayName);
        }
    }
}