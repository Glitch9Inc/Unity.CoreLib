using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;

namespace Glitch9
{
    public partial class GNLog
    {
        public static Stack<LogData> CachedLogs = new();
        public static event Action<LogData> OnLogAdded;
        public static bool IgnoreEditorLogs { get; set; } = false;
        
        internal static readonly ObjectPool<StringBuilder> Pool = new(() => new StringBuilder(), null, sb => sb.Clear());
        public static StringBuilder Get() => Pool.Get();
        public static PooledObject<StringBuilder> Get(out StringBuilder value) => Pool.Get(out value);
        public static void Release(StringBuilder toRelease) => Pool.Release(toRelease);

        private static readonly Dictionary<LogType, string> k_CachedColors = new();
        private const bool COLOR_LOGS = false;
    
        public static void ClearCachedColorHex() => k_CachedColors.Clear();
        public static string GetColorHex(LogType logType)
        {
            if (k_CachedColors.TryGetValue(logType, out string hex)) return hex;
            switch (logType)
            {
                case LogType.Log: k_CachedColors.Add(logType, ExColor.charcoal.ToHex()); break;
                case LogType.Warning: k_CachedColors.Add(logType, ExColor.orange.ToHex()); break;
                case LogType.Error: k_CachedColors.Add(logType, ExColor.clementine.ToHex()); break;
                case LogType.Exception: k_CachedColors.Add(logType, ExColor.purple.ToHex()); break;
                case LogType.Native: k_CachedColors.Add(logType, ExColor.gold.ToHex()); break;
                case LogType.NativeError: k_CachedColors.Add(logType, ExColor.garnet.ToHex()); break;
            }
            return k_CachedColors[logType];
        }

        public static void ContinueWithLogger(string msg, LogType logType, bool showCallerInfo = false, string callerMemberName = null, string callerFilePath = null, string tag = null)
        {
            using (Get(out StringBuilder sb))
            {
                bool tagAvailable = !string.IsNullOrEmpty(tag);
                if (showCallerInfo || tagAvailable)
                {
                    sb.Append("<color=blue>[");
                    if (tagAvailable)
                    {
                        sb.Append(tag);
                    }
                    else
                    {
                        sb.Append(Path.GetFileNameWithoutExtension(callerFilePath));
                        sb.Append("|");
                        sb.Append(callerMemberName);
                    }
                 
                    sb.Append("]</color> ");
                }
              
                if (COLOR_LOGS) sb.Append($"<color={GetColorHex(logType)}>");
                sb.Append(msg);
                if (COLOR_LOGS) sb.Append("</color>");

                string log = sb.ToString();

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
            Info("Log");
            Warning("Warning");
            Error("Error");
            Critical("Critical");
            Native("Native");
            NativeWarning("NativeWarning");
            NativeError("NativeError");
            Info("Green");
            Info("Blue");
        }
    }
}
