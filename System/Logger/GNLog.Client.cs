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
            ContinueWithLogger(msg, LogType.Info, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void Info(string tag, string msg)
        {
            ContinueWithLogger(msg, LogType.Info, false, null, null, tag);
        }

        public static void Warning(string msg, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(msg, LogType.Warning, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void Warning(string tag, string msg)
        {
            ContinueWithLogger(msg, LogType.Warning, false, null, null, tag);
        }

        public static void Error(string msg, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(msg, LogType.Error, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void Error(string tag, string msg)
        {
            ContinueWithLogger(msg, LogType.Error, false, null, null, tag);
        }
        
        public static void Error(Issue issue, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(issue, LogType.Error, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void Error(string tag, Issue issue)
        {
            ContinueWithLogger(issue, LogType.Error, false, null, null, tag);
        }

        public static void ParseFail(Type targetType, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            string msg = $"Failed to convert <color=blue>object</color> to <color=blue>{targetType.Name}</color>";
            ContinueWithLogger(msg, LogType.Error, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void ParseFail(string tag, Type targetType)
        {
            string msg = $"Failed to convert <color=blue>object</color> to <color=blue>{targetType.Name}</color>";
            ContinueWithLogger(msg, LogType.Error, false, null, null, tag);
        }

        public static void Critical(string msg, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(msg, LogType.Critical, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void Critical(string tag, string msg)
        {
            ContinueWithLogger(msg, LogType.Critical, false, null, null, tag);
        }

        public static void Exception(Exception e, bool showCallerInfo = false,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            string msg = e.Message + "\n" + e.StackTrace;
            ContinueWithLogger(msg, LogType.Critical, showCallerInfo, callerMemberName, callerFilePath);
        }

        public static void Exception(string tag, Exception e)
        {
            string msg = e.Message + "\n" + e.StackTrace;
            ContinueWithLogger(msg, LogType.Critical, false, null, null, tag);
        }

        public static void Native(string msg)
        {
            ContinueWithLogger(msg, LogType.NativeInfo, false, null, null, NATIVE_PLUGIN_TAG);
        }

        public static void NativeWarning(string msg)
        {
            ContinueWithLogger(msg, LogType.NativeWarning, false, null, null, NATIVE_PLUGIN_TAG);
        }

        public static void NativeError(string msg)
        {
            ContinueWithLogger(msg, LogType.NativeError, false, null, null, NATIVE_PLUGIN_TAG);
        }

        internal static void Error(Error error)
        {
            if (!LogErrorOnErrorInstanceCreated) return;
            ContinueWithLogger(error.ToString(LogStackTraceOnErrorInstanceCreated), LogType.Error, false, null, null);
        }

        internal static void Error<T>(Error<T> error)
        {
            if (!LogErrorOnErrorInstanceCreated) return;
            ContinueWithLogger(error.ToString(LogStackTraceOnErrorInstanceCreated), LogType.Error, false, null, null);
        }
    }
}