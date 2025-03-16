using System;
using System.Runtime.CompilerServices;

namespace Glitch9
{
    public partial class GNLog
    {
        private const string NATIVE_PLUGINS_TAG = "Native (Android/iOS)";

        public static void Info(string msg,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(LogType.Info, null, msg, callerMemberName, callerFilePath);
        }

        public static void Info(string msg, object sender,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(LogType.Info, sender, msg, callerMemberName, callerFilePath);
        }

        public static void Warning(string msg,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(LogType.Warning, null, msg,  callerMemberName, callerFilePath);
        }

        public static void Warning(string msg, object sender,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(LogType.Warning, sender, msg, callerMemberName, callerFilePath);
        }

        public static void Error(string msg,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(LogType.Error, null, msg, callerMemberName, callerFilePath);
        }

        public static void Error(string msg, object sender,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(LogType.Error, sender, msg, callerMemberName, callerFilePath);
        }

        public static void Error(Issue issue,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(LogType.Error, null, issue, callerMemberName, callerFilePath);
        }

        public static void Error(Issue issue, object sender,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(LogType.Error, sender, issue, callerMemberName, callerFilePath);
        }

        internal static void Error(Error error, 
            [CallerMemberName] string callerMemberName = "", 
            [CallerFilePath] string callerFilePath = "")
        {
            if (!LogErrorOnErrorInstanceCreated) return;
            ContinueWithLogger(LogType.Error, null, error.ToString(LogStackTraceOnErrorInstanceCreated), callerMemberName, callerFilePath);
        }

        internal static void Error(Error error, object sender,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            if (!LogErrorOnErrorInstanceCreated) return;
            ContinueWithLogger(LogType.Error, sender, error.ToString(LogStackTraceOnErrorInstanceCreated), callerMemberName, callerFilePath);
        }

        internal static void Error<T>(Error<T> error,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            if (!LogErrorOnErrorInstanceCreated) return;
            ContinueWithLogger(LogType.Error, null, error.ToString(LogStackTraceOnErrorInstanceCreated), callerMemberName, callerFilePath);
        }

        internal static void Error<T>(Error<T> error, object sender,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            if (!LogErrorOnErrorInstanceCreated) return;
            ContinueWithLogger(LogType.Error, sender, error.ToString(LogStackTraceOnErrorInstanceCreated), callerMemberName, callerFilePath);
        }
        
        public static void Exception(Exception e,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            string msg = e.Message + "\n" + e.StackTrace;
            ContinueWithLogger(LogType.Exception, null, msg, callerMemberName, callerFilePath);
        }

        public static void Exception(Exception e, object sender, 
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            string msg = e.Message + "\n" + e.StackTrace;
            ContinueWithLogger(LogType.Exception, sender, msg, callerMemberName, callerFilePath);
        }

        /// <summary>
        /// Temporary log for debugging purposes
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerFilePath"></param>
        public static void Debug(string msg,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(LogType.Debug,null, msg, callerMemberName, callerFilePath);
        }

        public static void Debug(string msg, object sender,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(LogType.Debug, sender, msg, callerMemberName, callerFilePath);
        }

        public static void Native(string msg, object sender = null)
        {
            ContinueWithLogger(LogType.NativeInfo, sender, msg, NATIVE_PLUGINS_TAG);
        }

        public static void NativeWarning(string msg, object sender = null)
        {
            ContinueWithLogger(LogType.NativeWarning, sender, msg, NATIVE_PLUGINS_TAG);
        }

        public static void NativeError(string msg, object sender = null)
        {
            ContinueWithLogger(LogType.NativeError, sender, msg, NATIVE_PLUGINS_TAG);
        }
    }
}