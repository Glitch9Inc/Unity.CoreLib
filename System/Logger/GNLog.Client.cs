using System;
using System.Runtime.CompilerServices;

namespace Glitch9
{
    public partial class GNLog
    {
        public static void Log(string msg, [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(msg, LogType.Log, callerMemberName, callerFilePath);
        }
        
        public static void Super(string tag, string msg, [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            string log = $"<color=blue>[ {tag} ]</color> {msg}";
            ContinueWithLogger(log, LogType.Log, callerMemberName, callerFilePath);
        }

        public static void Warning(string msg, [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(msg, LogType.Warning, callerMemberName, callerFilePath);
        }

        public static void Error(string msg, [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(msg, LogType.Error, callerMemberName, callerFilePath);
        }

        public static void Error(Issue issue, string arg = null, [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            //string msg = LocalizedText.Error.GetErrorText(issue, arg);
            string msg = issue.ToString();
            ContinueWithLogger(msg, LogType.Error, callerMemberName, callerFilePath);
        }

        public static void ParseFail(Type targetType, [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        { 
            string msg = $"Failed to convert <color=blue>object</color> to <color=blue>{targetType.Name}</color>";
            ContinueWithLogger(msg, LogType.Error, callerMemberName, callerFilePath);
        }

        public static void Critical(string msg, [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            ContinueWithLogger(msg, LogType.Exception, callerMemberName, callerFilePath);
        }

        public static void Exception(Exception e, [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            string msg = e.Message + "\n" + e.StackTrace;
            ContinueWithLogger(msg, LogType.Exception, callerMemberName, callerFilePath);
        }

        public static void Native(string msg)
        {
            ContinueWithLogger(msg, LogType.Native, "NativePlugin", "");
        }

        public static void NativeWarning(string msg)
        {
            ContinueWithLogger(msg, LogType.NativeWarning, "NativePlugin", "");
        }

        public static void NativeError(string msg)
        {
            ContinueWithLogger(msg, LogType.NativeError, "NativePlugin", "");
        }
    }
}