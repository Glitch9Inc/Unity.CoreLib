using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Glitch9
{
    public static class NullCheck
    {
        private static string ParseFilename(string callerFilaPath)
        {
            return System.IO.Path.GetFileNameWithoutExtension(callerFilaPath);
        }

        private static void HandleNullLog(string callerMemberName, string callerFilePath, string typeName, LogType logType)
        {
            GNLog.ContinueWithLogger($"{ParseFilename(callerFilePath)}'s {typeName} is null.", logType, callerMemberName, callerFilePath);
        }

        private static void HandleEmptyLog(string callerMemberName, string callerFilePath, string typeName, LogType logType)
        {
            GNLog.ContinueWithLogger($"{ParseFilename(callerFilePath)}'s {typeName} is empty.", logType, callerMemberName, callerFilePath);
        }

        public static bool IsNull<T>(this T obj, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (obj == null)
            {
                HandleNullLog(callerMemberName, callerFilePath, typeof(T).Name, LogType.Error);
                return true;
            }
            return false;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (list == null)
            {
                HandleNullLog(callerMemberName, callerFilePath, typeof(T).Name, LogType.Error);
                return true;
            }

            using IEnumerator<T> enumerator = list.GetEnumerator();
            if (enumerator.MoveNext()) return false;
            HandleEmptyLog(callerMemberName, callerFilePath, typeof(T).Name, LogType.Error);
            return true;
        }

        public static bool IsNullOrEmpty<T>(this List<T> list, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (list == null || list.Count == 0)
            {
                HandleNullLog(callerMemberName, callerFilePath, typeof(T).Name, LogType.Error);
                return true;
            }

            if (list.Count == 0)
            {
                HandleEmptyLog(callerMemberName, callerFilePath, typeof(T).Name, LogType.Error);
                return true;
            }

            return false;
        }

        public static bool IsNullOrEmpty<T>(this T[] array, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (array == null)
            {
                HandleNullLog(callerMemberName, callerFilePath, typeof(T).Name, LogType.Error);
                return true;
            }

            if (array.Length == 0)
            {
                HandleEmptyLog(callerMemberName, callerFilePath, typeof(T).Name, LogType.Error);
                return true;
            }

            return false;
        }

        public static bool IsNullOrEmpty<V, T>(this Dictionary<V, T> dict, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (dict == null)
            {
                HandleNullLog(callerMemberName, callerFilePath, typeof(T).Name, LogType.Error);
                return true;
            }

            if (dict.Count == 0)
            {
                HandleEmptyLog(callerMemberName, callerFilePath, typeof(T).Name, LogType.Error);
                return true;
            }

            return false;
        }

        public static bool IsNullOrEmpty<T>(this Queue<T> queue, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (queue == null)
            {
                HandleNullLog(callerMemberName, callerFilePath, typeof(T).Name, LogType.Error);
                return true;
            }

            if (queue.Count == 0)
            {
                HandleEmptyLog(callerMemberName, callerFilePath, typeof(T).Name, LogType.Error);
                return true;
            }

            return false;
        }

        public static bool IsNullOrWhiteSpace(this string text, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (text == null)
            {
                HandleNullLog(callerMemberName, callerFilePath, typeof(string).Name, LogType.Error);
                return true;
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                HandleEmptyLog(callerMemberName, callerFilePath, typeof(string).Name, LogType.Error);
                return true;
            }

            return false;
        }

        public static bool IsSetInInspector<T>(this T obj, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (obj != null) return true;
            GNLog.ContinueWithLogger($"<color=blue><{callerMemberName}></color> is not set in inspector", LogType.Error, callerMemberName, callerFilePath);
            return false;
        }

        public static bool IsNotSetInInspector<T>(this T obj, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (obj == null)
            {
                GNLog.ContinueWithLogger($"<color=blue><{callerMemberName}></color> is not set in inspector", LogType.Error, callerMemberName, callerFilePath);
                return true;
            }
            return false;
        }
    }
}
