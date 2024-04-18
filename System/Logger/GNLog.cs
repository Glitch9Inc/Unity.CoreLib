using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Glitch9
{
    public partial class GNLog : MonoBehaviour
    {
        public static GNLog Logger { get; private set; }
        public static Stack<LogData> CachedLogs = new();
        public static event Action<LogData> OnLogAdded;
        public static bool IgnoreEditorLogs { get; set; } = false;

        private static bool IsEnabled = true;
        private static bool UseColor = false;
        private static bool ShowScriptNames = false;
        private static bool ShowMethodNames = false;
        private static readonly Dictionary<LogType, string> CachedColorHex = new();
            
        private void Awake()
        {
            Logger = this;
            IsEnabled = LoggerSettings.EnableLogger;
            UseColor = LoggerSettings.EnableLogColors;
            ShowScriptNames = LoggerSettings.ShowScriptName;
            ShowMethodNames = LoggerSettings.ShowMethodName;
        }

        public static void ClearCachedColorHex() => CachedColorHex.Clear();
        public static string GetColorHex(LogType logType)
        {
            if (!CachedColorHex.ContainsKey(logType))
            {
                switch (logType)
                {
                    case LogType.Log: CachedColorHex.Add(logType, LoggerSettings.LogColor.ToHex()); break;
                    case LogType.Warning: CachedColorHex.Add(logType, LoggerSettings.WarningColor.ToHex()); break;
                    case LogType.Error: CachedColorHex.Add(logType, LoggerSettings.ErrorColor.ToHex()); break;
                    case LogType.Exception: CachedColorHex.Add(logType, LoggerSettings.CriticalColor.ToHex()); break;
                    case LogType.Native: CachedColorHex.Add(logType, LoggerSettings.NativeColor.ToHex()); break;
                    case LogType.NativeError: CachedColorHex.Add(logType, LoggerSettings.NativeErrorColor.ToHex()); break;
                }
            }
            return CachedColorHex[logType];
        }

        public static void ContinueWithLogger(string msg, LogType logType, string callerMemberName, string callerFilePath)
        {
            if (!IsEnabled) return;

            string className = Path.GetFileNameWithoutExtension(callerFilePath);
            string log;

            if (ShowScriptNames && ShowMethodNames)
            {
                log = $"<color=blue>[{className}:{callerMemberName}]</color> {msg}";
            }
            else if (ShowScriptNames)
            {
                log = $"<color=blue>[{className}]</color> {msg}";
            }
            else if (ShowMethodNames)
            {
                log = $"<color=blue>[{callerMemberName}]</color> {msg}";
            }
            else
            {
                log = msg;
            }

            if (UseColor)
            {
                string hexColor = GetColorHex(logType);
                log = $"<color={hexColor}>{log}</color>";
            }

            LogData logData = new(logType, HandlePlatformSpecificFormat(log));

            switch (logType)
            {
                case LogType.Log:
                case LogType.Native:
                    Debug.Log(log);
                    break;
                case LogType.Warning:
                case LogType.NativeWarning:
                    Debug.LogWarning(log);
                    break;
                case LogType.Error:
                case LogType.NativeError:
                case LogType.Exception:
#if UNITY_ANDROID
#elif UNITY_EDITOR
                    if (!Application.isPlaying && !IgnoreEditorLogs) EditorUtility.DisplayDialog("Error", log, "OK");
#endif
                    Debug.LogError(log);
                    break;
            }

            CachedLogs.Push(logData);
            OnLogAdded?.Invoke(logData);
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
#elif UNITY_EDITOR
            return log;
#endif
        }

        internal static void LogColorTest()
        {
            ClearCachedColorHex();
            Log("Log");
            Warning("Warning");
            Error("Error");
            Critical("Critical");
            Native("Native");
            NativeWarning("NativeWarning");
            NativeError("NativeError");
            Log("Green");
            Log("Blue");
        }
    }
}
