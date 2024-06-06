using System;
using System.Diagnostics.CodeAnalysis;

namespace Glitch9
{
    public partial class Validate
    {
        public static class Endpoint
        {
            private const string k_Endpoint = "Endpoint";
            public static void NotNull([NotNull] string value, string endpointName = k_Endpoint)
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(endpointName);
            }
        }
    }
}