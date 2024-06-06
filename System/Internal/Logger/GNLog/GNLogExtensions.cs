using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Glitch9
{
    public static class GNLogExtensions
    {
        public static bool LogIfNull<T>(this T obj, bool showCallerInfo = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (obj == null)
            {
                HandleError($"{ParseFilename(callerFilePath)}'s {typeof(T).Name} is null.", showCallerInfo, callerMemberName, callerFilePath);
                return true;
            }
            return false;
        }

        public static bool LogIfNullOrEmpty<T>(this IEnumerable<T> collection, bool showCallerInfo = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (collection == null)
            {
                HandleError($"{ParseFilename(callerFilePath)}'s {typeof(T).Name} collection is null.", showCallerInfo, callerMemberName, callerFilePath);
                return true;
            }

            // Optimizing for ICollection<T> to use Count when available
            if (collection is ICollection<T> coll)
            {
                if (coll.Count == 0)
                {
                    HandleError($"{ParseFilename(callerFilePath)}'s {typeof(T).Name} collection is empty.", showCallerInfo, callerMemberName, callerFilePath);
                    return true;
                }
            }
            else
            {
                using IEnumerator<T> enumerator = collection.GetEnumerator();
                if (!enumerator.MoveNext())
                {
                    HandleError($"{ParseFilename(callerFilePath)}'s {typeof(T).Name} collection is empty.", showCallerInfo, callerMemberName, callerFilePath);
                    return true;
                }
            }

            return false;
        }

        public static bool LogIfNullOrWhiteSpace(this string text, bool showCallerInfo = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                HandleError($"{ParseFilename(callerFilePath)}'s string is null or whitespace.", showCallerInfo, callerMemberName, callerFilePath);
                return true;
            }
            return false;
        }

        public static bool LogIfSetInInspector<T>(this T obj, bool showCallerInfo = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (obj == null)
            {
                GNLog.ContinueWithLogger($"<color=blue><{callerMemberName}></color> is not set in inspector", LogType.Error, showCallerInfo, callerMemberName, callerFilePath);
                return false;
            }
            return true;
        }

        public static bool LogIfNotSetInInspector<T>(this T obj, bool showCallerInfo = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            if (obj == null)
            {
                GNLog.ContinueWithLogger($"<color=blue><{callerMemberName}></color> is not set in inspector", LogType.Error, showCallerInfo, callerMemberName, callerFilePath);
                return true;
            }
            return false;
        }

        private static string ParseFilename(string callerFilaPath)
        {
            return System.IO.Path.GetFileNameWithoutExtension(callerFilaPath);
        }

        // Helper method to handle logging.
        private static void HandleError(string message, bool showCallerInfo = false, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "")
        {
            GNLog.ContinueWithLogger(message, LogType.Error, showCallerInfo, callerMemberName, callerFilePath);
        }
    }
}
