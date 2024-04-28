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

        // Helper method to handle logging.
        private static void LogError(string message, bool showCallerInfo = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            GNLog.ContinueWithLogger(message, LogType.Error, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static bool IsNull<T>(this T obj, bool showCallerInfo = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (obj == null)
            {
                LogError($"{ParseFilename(callerFilePath)}'s {typeof(T).Name} is null.", showCallerInfo, callerMemberName, callerFilePath);
                return true;
            }
            return false;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection, bool showCallerInfo = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (collection == null)
            {
                LogError($"{ParseFilename(callerFilePath)}'s {typeof(T).Name} collection is null.", showCallerInfo, callerMemberName, callerFilePath);
                return true;
            }

            // Optimizing for ICollection<T> to use Count when available
            if (collection is ICollection<T> coll)
            {
                if (coll.Count == 0)
                {
                    LogError($"{ParseFilename(callerFilePath)}'s {typeof(T).Name} collection is empty.", showCallerInfo, callerMemberName, callerFilePath);
                    return true;
                }
            }
            else
            {
                using IEnumerator<T> enumerator = collection.GetEnumerator();
                if (!enumerator.MoveNext())
                {
                    LogError($"{ParseFilename(callerFilePath)}'s {typeof(T).Name} collection is empty.", showCallerInfo, callerMemberName, callerFilePath);
                    return true;
                }
            }

            return false;
        }

        public static bool IsNullOrWhiteSpace(this string text, bool showCallerInfo = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                LogError($"{ParseFilename(callerFilePath)}'s string is null or whitespace.", showCallerInfo, callerMemberName, callerFilePath);
                return true;
            }
            return false;
        }

        public static bool IsSetInInspector<T>(this T obj, bool showCallerInfo = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (obj != null) return true;
            GNLog.ContinueWithLogger($"<color=blue><{callerMemberName}></color> is not set in inspector", LogType.Error, showCallerInfo, callerMemberName, callerFilePath);
            return false;
        }

        public static bool IsNotSetInInspector<T>(this T obj, bool showCallerInfo = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (obj == null)
            {
                GNLog.ContinueWithLogger($"<color=blue><{callerMemberName}></color> is not set in inspector", LogType.Error, showCallerInfo, callerMemberName, callerFilePath);
                return true;
            }
            return false;
        }
    }
}
