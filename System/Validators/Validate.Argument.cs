using System;
using System.Diagnostics.CodeAnalysis;

namespace Glitch9
{
    /// <summary>
    /// Validate objects and throw exceptions when a condition is not met.
    /// </summary>
    public partial class Validate
    {
        public static class Argument
        {
            /// <summary>
            /// Throws <see cref="ArgumentNullException"/> if the value is null.
            /// </summary>
            /// <param name="value">The value to check.</param>
            /// <param name="parameterName">The name of the parameter.</param>
            /// <exception cref="ArgumentNullException"></exception>
            public static void NotNull([NotNull] object value, string parameterName)
            {
                if (value == null) throw new ArgumentNullException(parameterName);
            }

            /// <summary>
            /// Throws <see cref="ArgumentNullException"/> if the value is null.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value to check.</param>
            /// <exception cref="ArgumentNullException"></exception>
            public static void NotNull<T>([NotNull] T value)
            {
                if (value != null) return;
                string parameterName = typeof(T).Name;
                throw new ArgumentNullException(parameterName);
            }

            /// <summary>
            /// Throws <see cref="ArgumentNullException"/> if the string is null or empty.
            /// </summary>
            /// <param name="value">The value to check.</param>
            /// <param name="parameterName">The name of the parameter.</param>
            /// <exception cref="ArgumentNullException"></exception>
            public static void NotNullOrEmpty(string value, string parameterName)
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(parameterName);
            }

            /// <summary>
            /// Throws <see cref="ArgumentNullException"/> if the string is null or whitespace.
            /// </summary>
            /// <param name="value">The value to check.</param>
            /// <param name="parameterName">The name of the parameter.</param>
            /// <exception cref="ArgumentNullException"></exception>
            public static void NotNullOrWhitespace(string value, string parameterName)
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(parameterName);
            }

            public static void NotDefaultEnum<T>(T value, string parameterName)
                where T : Enum
            {
                if (value.Equals(default(T))) throw new ArgumentNullException(parameterName);
            }

            public static void NotLessThanZero(int value, string parameterName)
            {
                if (value < 0) throw new ArgumentOutOfRangeException(parameterName);
            }

            public static void NotLessThanZero(float value, string parameterName)
            {
                if (value < 0) throw new ArgumentOutOfRangeException(parameterName);
            }

            public static void NotLessThanZero(double value, string parameterName)
            {
                if (value < 0) throw new ArgumentOutOfRangeException(parameterName);
            }

            public static void NotLessThanZero(decimal value, string parameterName)
            {
                if (value < 0) throw new ArgumentOutOfRangeException(parameterName);
            }

            public static void NotLessThanZero(long value, string parameterName)
            {
                if (value < 0) throw new ArgumentOutOfRangeException(parameterName);
            }
        }
    }
}