namespace Glitch9
{

    public static class Vibrator
    {
        /*
            * int	DEFAULT_AMPLITUDE
            The default vibration strength of the device.

            int	EFFECT_CLICK
            A click effect.

            int	EFFECT_DOUBLE_CLICK
            A double click effect.

            int	EFFECT_HEAVY_CLICK
            A heavy click effect.

            int	EFFECT_TICK
            A tick effect.
        */

#if UNITY_EDITOR
#elif UNITY_ANDROID
        // Java Plugin
        private const string PACKAGE_NAME = "com.codeqo.routina.RoutinaPlugin";
        private static AndroidJavaObject _plugin;
#endif

        public static void Vibrate(long milliseconds, int amplitude = -1) /* 1~255 */
        {
#if UNITY_EDITOR
#elif UNITY_ANDROID
             using (var pc = new AndroidJavaObject(PACKAGE_NAME))
                _plugin = pc.CallStatic<AndroidJavaObject>("getInstance");
            _plugin.Call("vibrate", milliseconds, amplitude);
#elif UNITY_IOS  
            Handheld.Vibrate();
#endif
        }

        public static void KeyboardVibrate()
        {
            Vibrate(20, 20);
        }

        public static void Tick()
        {
            Vibrate(100, 100);
        }

        public static void Click()
        {
            Vibrate(300, 150);
        }
    }
}