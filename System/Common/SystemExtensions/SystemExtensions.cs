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

        /// <summary>
        /// Returns a single string from args: args[0] as string;
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetStringFromArgs(params object[] args)
        {
            if (args.IsNullOrEmpty() || args[0] is not string str) return null;
            return str;
        }

        /// <summary>
        /// Returns a single object from args: args[0] as T;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T GetFromArgs<T>(params object[] args) where T : class
        {
            if (args.IsNullOrEmpty() || args[0] is not T obj) return null;
            return obj;
        }
    }
}