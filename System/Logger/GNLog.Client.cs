using System;
using System.Runtime.CompilerServices;

namespace Glitch9
{
    public partial class GNLog
    {
        private const string NATIVE_PLUGIN_TAG = "NativePlugin";

        public static void Info(string msg, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(msg, LogType.Log, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void Info(string tag, string msg)
        {
            ContinueWithLogger(msg, LogType.Log, false, null, null, tag);
        }

        public static void Warning(string msg, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(msg, LogType.Warning, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void Error(string msg, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(msg, LogType.Error, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void Error(Issue issue, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            string msg = issue.ToString();
            ContinueWithLogger(msg, LogType.Error, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void ParseFail(Type targetType, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            string msg = $"Failed to convert <color=blue>object</color> to <color=blue>{targetType.Name}</color>";
            ContinueWithLogger(msg, LogType.Error, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void Critical(string msg, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(msg, LogType.Exception, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void Exception(Exception e, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            string msg = e.Message + "\n" + e.StackTrace;
            ContinueWithLogger(msg, LogType.Exception, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void Native(string msg)
        {
            ContinueWithLogger(msg, LogType.Native, false, null, null, NATIVE_PLUGIN_TAG);
        }

        public static void NativeWarning(string msg)
        {
            ContinueWithLogger(msg, LogType.NativeWarning, false, null, null, NATIVE_PLUGIN_TAG);
        }

        public static void NativeError(string msg)
        {
            ContinueWithLogger(msg, LogType.NativeError, false, null, null, NATIVE_PLUGIN_TAG);
        }
    }
}