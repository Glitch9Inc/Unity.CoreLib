using System;
using System.Collections.Generic;

namespace Glitch9
{
    public static class SystemExtensions
    {
        public static void DisposeAll<T>(this List<T> disposables) where T : IDisposable
        {
            if (disposables == null) return;
            foreach (T disposable in disposables) disposable.Dispose();
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

        public static string ToSenderName(this object sender)
        {
            if (sender == null)
            {
                return string.Empty;
            }
            
            if (sender is Type type)
            {
                return type.Name;
            }

            if (sender is string str)
            {
                return str;
            }

            return sender.GetType().Name;
        }
    }
}