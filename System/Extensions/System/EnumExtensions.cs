using System;
using System.Collections.Generic;
using UnityEngine;

namespace Glitch9
{
    public static class EnumExtensions
    {
        public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }

        public static T ToEnum<T>(this string enumAsString)
        {
            if (string.IsNullOrWhiteSpace(enumAsString))
            {
                Debug.LogError($"Value '{enumAsString}' is not a valid enum value of type {typeof(T).Name}.");
                return default;
            }

            enumAsString = enumAsString.Trim().Replace(" ", "");

            if (Enum.TryParse(typeof(T), enumAsString, true, out object result))
            {
                return (T)result;
            }
            else
            {
                Debug.LogError($"Value '{enumAsString}' is not a valid enum value of type {typeof(T).Name}.");
                return default;
            }
        }

        public static bool IsEqual<T>(this T enumValue1, T enumValue2) where T : Enum
        {
            return EqualityComparer<T>.Default.Equals(enumValue1, enumValue2);
        }
    }
}