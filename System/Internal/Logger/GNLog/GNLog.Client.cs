using System;
using System.Runtime.CompilerServices;

namespace Glitch9
{
    public partial class GNLog
    {
        private const string NATIVE_PLUGIN_TAG = "NativePlugin";

        public static void Info(string msg,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(callerMemberName, msg, LogType.Info, callerMemberName, callerFilePath);
        }

        public static void Info(object sender, string msg)
        {
            ContinueWithLogger(sender, msg, LogType.Info);
        }

        public static void Warning(string msg,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(callerMemberName, msg, LogType.Warning, callerMemberName, callerFilePath);
        }

        public static void Warning(object sender, string msg)
        {
            ContinueWithLogger(sender, msg, LogType.Warning);
        }

        public static void Error(string msg,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(callerMemberName, msg, LogType.Error, callerMemberName, callerFilePath);
        }

        public static void Error(object sender, string msg)
        {
            ContinueWithLogger(sender, msg, LogType.Error);
        }

        public static void Error(Issue issue,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(callerMemberName, issue, LogType.Error, callerMemberName, callerFilePath);
        }

        public static void Error(object sender, Issue issue)
        {
            ContinueWithLogger(sender, issue, LogType.Error);
        }

        public static void ParseFail(Type targetType,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            string msg = $"Failed to convert <color=blue>object</color> to <color=blue>{targetType.Name}</color>";
            ContinueWithLogger(callerMemberName, msg, LogType.Error, callerMemberName, callerFilePath);
        }

        public static void ParseFail(object sender, Type targetType)
        {
            string msg = $"Failed to convert <color=blue>object</color> to <color=blue>{targetType.Name}</color>";
            ContinueWithLogger(sender, msg, LogType.Error);
        }

        public static void Critical(string msg,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(callerMemberName, msg, LogType.Critical, callerMemberName, callerFilePath);
        }

        public static void Critical(object sender, string msg)
        {
            ContinueWithLogger(sender, msg, LogType.Critical);
        }

        public static void Exception(Exception e,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            string msg = e.Message + "\n" + e.StackTrace;
            ContinueWithLogger(callerMemberName, msg, LogType.Critical, callerMemberName, callerFilePath);
        }

        public static void Exception(object sender, Exception e)
        {
            string msg = e.Message + "\n" + e.StackTrace;
            ContinueWithLogger(sender, msg, LogType.Critical);
        }

        /// <summary>
        /// Temporary log for debugging purposes
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="showCallerInfo"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerFilePath"></param>
        public static void Debug(string msg,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(callerMemberName, msg, LogType.Debug, callerMemberName, callerFilePath);
        }

        /// <summary>
        /// Temporary log for debugging purposes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        public static void Debug(object sender, string msg)
        {
            ContinueWithLogger(sender, msg, LogType.Debug);
        }

        public static void Native(string msg)
        {
            ContinueWithLogger(NATIVE_PLUGIN_TAG, msg, LogType.NativeInfo);
        }

        public static void NativeWarning(string msg)
        {
            ContinueWithLogger(NATIVE_PLUGIN_TAG, msg, LogType.NativeWarning);
        }

        public static void NativeError(string msg)
        {
            ContinueWithLogger(NATIVE_PLUGIN_TAG, msg, LogType.NativeError);
        }

        internal static void Error(Error error, [CallerMemberName] string callerMemberName = "")
        {
            if (!LogErrorOnErrorInstanceCreated) return;
            ContinueWithLogger(callerMemberName, error.ToString(LogStackTraceOnErrorInstanceCreated), LogType.Error);
        }

        internal static void Error<T>(Error<T> error, [CallerMemberName] string callerMemberName = "")
        {
            if (!LogErrorOnErrorInstanceCreated) return;
            ContinueWithLogger(callerMemberName, error.ToString(LogStackTraceOnErrorInstanceCreated), LogType.Error);
        }
    }
}