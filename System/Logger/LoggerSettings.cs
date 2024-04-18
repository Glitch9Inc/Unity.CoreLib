using UnityEngine;

namespace Glitch9
{
    public class LoggerSettings : ScriptableSettings<LoggerSettings>
    {
        [SerializeField] private bool enableLogger = true;
        [SerializeField] private bool enableLogColors;
        [SerializeField] private bool showScriptName;
        [SerializeField] private bool showMethodName;
 
        [SerializeField] private Color logColor = Color.white;
        [SerializeField] private Color warningColor = Color.yellow;
        [SerializeField] private Color errorColor = Color.red;
        [SerializeField] private Color criticalColor = Color.magenta;
        [SerializeField] private Color nativeColor = Color.cyan;
        [SerializeField] private Color nativeWarning = Color.yellow;
        [SerializeField] private Color nativeErrorColor = Color.red;
        [SerializeField] private Color greenColor = Color.green;
        [SerializeField] private Color blueColor = Color.blue;


        public static bool EnableLogger => Instance.enableLogger;
        public static bool EnableLogColors => Instance.enableLogColors;
        public static bool ShowScriptName => Instance.showScriptName;
        public static bool ShowMethodName => Instance.showMethodName;
  
        public static Color LogColor => Instance.logColor;
        public static Color WarningColor => Instance.warningColor;
        public static Color ErrorColor => Instance.errorColor;
        public static Color CriticalColor => Instance.criticalColor;
        public static Color NativeColor => Instance.nativeColor;
        public static Color NativeWarningColor => Instance.nativeWarning;
        public static Color NativeErrorColor => Instance.nativeErrorColor;
        public static Color GreenColor => Instance.greenColor;
        public static Color BlueColor => Instance.blueColor;
    }
}