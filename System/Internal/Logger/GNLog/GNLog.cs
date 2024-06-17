using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Glitch9
{
    public partial class GNLog
    {
        public static event Action<LogData> OnLogAdded;

        private static class PrefsKeys
        {
            internal const string ENABLED = "GNLog.Enabled";
            internal const string COLORED = "GNLog.Colored";
            internal const string LOG_ERROR_ON_ERROR_INSTANCE_CREATED = "GNLog.LogErrorOnErrorInstanceCreated";
            internal const string LOG_STACKTRACE_ON_ERROR_INSTANCE_CREATED = "GNLog.LogStackTraceOnErrorInstanceCreated";
            internal const string SHOW_CALLER_FILE_PATH = "GNLog.ShowCallerFilePath";
        }

        private static class DefaultValues
        {
            internal const string UNKNOWN_SENDER = "Unknown";
            internal const bool ENABLED = true;
            internal const bool COLORED = false;
            internal const bool SHOW_CALLER_FILE_PATH = false;
            internal const bool LOG_ERROR_ON_ERROR_INSTANCE_CREATED = true;
            internal const bool LOG_STACKTRACE_ON_ERROR_INSTANCE_CREATED = false;
        }

        public static Prefs<bool> IsEnabled { get; set; } = new(PrefsKeys.ENABLED, DefaultValues.ENABLED);
        public static Prefs<bool> IsColored { get; set; } = new(PrefsKeys.COLORED, DefaultValues.COLORED);
        public static Prefs<bool> ShowCallerFilePath { get; set; } = new(PrefsKeys.SHOW_CALLER_FILE_PATH, DefaultValues.SHOW_CALLER_FILE_PATH);
        public static Prefs<bool> LogErrorOnErrorInstanceCreated { get; set; } = new(PrefsKeys.LOG_ERROR_ON_ERROR_INSTANCE_CREATED, DefaultValues.LOG_ERROR_ON_ERROR_INSTANCE_CREATED);
        public static Prefs<bool> LogStackTraceOnErrorInstanceCreated { get; set; } = new(PrefsKeys.LOG_STACKTRACE_ON_ERROR_INSTANCE_CREATED, DefaultValues.LOG_STACKTRACE_ON_ERROR_INSTANCE_CREATED);


        private static readonly Dictionary<LogType, string> k_CachedColors = new();

        public static Stack<LogData> CachedLogs = new();


        public static void ClearCachedColorHex() => k_CachedColors.Clear();
        public static string GetColorHex(LogType logType)
        {
            if (k_CachedColors.TryGetValue(logType, out string hex)) return hex;
            switch (logType)
            {
                case LogType.Info: k_CachedColors.Add(logType, Color.black.ToHex()); break;
                case LogType.Verbose: k_CachedColors.Add(logType, Color.blue.ToHex()); break;
                case LogType.Warning: k_CachedColors.Add(logType, ExColor.orange.ToHex()); break;
                case LogType.Error: k_CachedColors.Add(logType, ExColor.clementine.ToHex()); break;
                case LogType.Critical: k_CachedColors.Add(logType, ExColor.purple.ToHex()); break;
                case LogType.NativeInfo: k_CachedColors.Add(logType, ExColor.gold.ToHex()); break;
                case LogType.NativeError: k_CachedColors.Add(logType, ExColor.garnet.ToHex()); break;
            }
            return k_CachedColors[logType];
        }

        public static void ContinueWithLogger(object sender, Issue issue, LogType logType, string callerMemberName = null, string callerFilePath = null)
        {
            ContinueWithLogger(sender, issue.ToString(), logType, callerMemberName, callerFilePath);
        }

        public static void ContinueWithLogger(object sender, string msg, LogType logType, string callerMemberName = null, string callerFilePath = null)
        {
            using (StringBuilderPool.Get(out StringBuilder sb))
            {
                string senderAsString = ParseSender(sender, callerMemberName);

                sb.Append("<color=blue>[");
                sb.Append(senderAsString);
                
                if (ShowCallerFilePath && !string.IsNullOrEmpty(callerFilePath))
                {
                    sb.Append(Path.GetFileNameWithoutExtension(callerFilePath));
                }

                sb.Append("]</color> ");

                bool isColored = IsColored;

                if (isColored) sb.Append($"<color={GetColorHex(logType)}>");
                
                sb.Append(msg);
                
                if (isColored) sb.Append("</color>");

                string log = sb.ToString();

                LogData logData = new(logType, HandlePlatformSpecificFormat(log));

                switch (logType)
                {
                    case LogType.Info:
                    case LogType.Verbose:
                    case LogType.NativeInfo:
                    case LogType.NativeVerbose:
                    case LogType.Debug:
                        UnityEngine.Debug.Log(log);
                        break;
                    case LogType.Warning:
                    case LogType.NativeWarning:
                        UnityEngine.Debug.LogWarning(log);
                        break;
                    case LogType.Error:
                    case LogType.NativeError:
                    case LogType.Critical:
                    case LogType.NativeCritical:
                        UnityEngine.Debug.LogError(log);
                        break;
                }

                CachedLogs.Push(logData);
                OnLogAdded?.Invoke(logData);
            }
        }


        private static string HandlePlatformSpecificFormat(string log)
        {
#if UNITY_ANDROID
            if (log.Contains("\\"))
            {
                log = log.Substring(log.LastIndexOf("\\"));
                log = log.Replace("\\", "");
                log = "[ " + log;
            }
            return log;
#else
            return log;
#endif
        }
  

        private static string ParseSender(object sender, string callerMemberName)
        {
            if (sender == null)
            {
                if (string.IsNullOrEmpty(callerMemberName)) return DefaultValues.UNKNOWN_SENDER;
                return callerMemberName;
            }

            if (sender is string s)
            {
                if (string.IsNullOrEmpty(s))
                {
                    if (string.IsNullOrEmpty(callerMemberName)) return DefaultValues.UNKNOWN_SENDER;
                    return callerMemberName;
                }
                return s;
            }

            return sender.GetType().Name;
        }
    }
}
