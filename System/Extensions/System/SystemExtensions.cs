using System;
using System.Collections.Generic;
using System.Linq;

namespace Glitch9
{
    public static class SystemExtensions
    {
        public static void DisposeAll<T>(this List<T> disposables) where T : IDisposable
        {
            if (disposables == null) return;
            foreach (T disposable in disposables) disposable.Dispose();
        }

        public static bool HasInterface<TInterface>(this Type type)
        {
            return type.GetInterfaces().Any(i => i == typeof(TInterface));
        }

        public static bool IsNumericType(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static int Next(this int index, int max)
        {
            if (index + 1 >= max) return 0;
            return index + 1;
        }

        public static int Previous(this int index, int max)
        {
            if (index - 1 < 0) return max - 1;
            return index - 1;
        }
    }
}