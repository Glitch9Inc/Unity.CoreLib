using System;
using System.Diagnostics.CodeAnalysis;

namespace Glitch9
{
    public static class Validate
    {
        public static class Argument
        {
            public static void NotNull([NotNull] object value, string parameterName)
            {
                if (value == null)
                {
                    throw new ArgumentNullException(parameterName);
                }
            }
        }
    }
}