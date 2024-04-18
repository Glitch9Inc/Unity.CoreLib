using UnityEditor;
using UnityEngine;

namespace Glitch9.ExEditor
{
    public abstract class ExEditorWindow<TWindowClass> : EditorWindow where TWindowClass : EditorWindow
    {
        private const float DEFAULT_IMAGE_HEIGHT = 74f;
        private const float DEFAULT_IMAGE_WIDTH = 74f;
        private const float DEFAULT_ICON_HEIGHT = 32f;
        private const float DEFAULT_ICON_WIDTH = 32f;
        private const float DEFAULT_MIN_WINDOW_HEIGHT = 400f;
        private const float DEFAULT_MIN_WINDOW_WIDTH = 420f;
        private const float DEFAULT_MAX_WINDOW_HEIGHT = 1200f;
        private const float DEFAULT_MAX_WINDOW_WIDTH = 1800f;
        private const float DEFAULT_BUTTON_HEIGHT = 30f;
        private const float DEFAULT_BUTTON_WIDTH = 120f;
        private const float DEFAULT_MINI_BUTTON_HEIGHT = 20f;
        private const float DEFAULT_MINI_BUTTON_WIDTH = 80f;

        protected GUILayoutOption[] ButtonOptions;
        protected GUILayoutOption[] MiniButtonOptions;
        protected EPrefs<string> WindowName;
        protected bool IsShowingSettings;

        private EPrefs<Vector2> _imageSize;
        private EPrefs<Vector2> _iconSize;
        private EPrefs<Vector2> _maxWindowSize;
        private EPrefs<Vector2> _minWindowSize;
        private EPrefs<Vector2> _buttonSize;
        private EPrefs<Vector2> _miniButtonSize;

        protected virtual void OnEnable()
        {
            WindowName = new EPrefs<string>(typeof(TWindowClass).Name + ".WindowName", GetType().Name);
            _imageSize = new EPrefs<Vector2>(typeof(TWindowClass).Name + ".ImageSize", new Vector2(DEFAULT_IMAGE_WIDTH, DEFAULT_IMAGE_HEIGHT));
            _iconSize = new EPrefs<Vector2>(typeof(TWindowClass).Name + ".IconSize", new Vector2(DEFAULT_ICON_WIDTH, DEFAULT_ICON_HEIGHT));
            _maxWindowSize = new EPrefs<Vector2>(typeof(TWindowClass).Name + ".MaxWindowSize", new Vector2(DEFAULT_MAX_WINDOW_WIDTH, DEFAULT_MAX_WINDOW_HEIGHT));
            _minWindowSize = new EPrefs<Vector2>(typeof(TWindowClass).Name + ".MinWindowSize", new Vector2(DEFAULT_MIN_WINDOW_WIDTH, DEFAULT_MIN_WINDOW_HEIGHT));
            _buttonSize = new EPrefs<Vector2>(typeof(TWindowClass).Name + ".ButtonSize", new Vector2(DEFAULT_BUTTON_WIDTH, DEFAULT_BUTTON_HEIGHT));
            _miniButtonSize = new EPrefs<Vector2>(typeof(TWindowClass).Name + ".MiniButtonSize", new Vector2(DEFAULT_MINI_BUTTON_WIDTH, DEFAULT_MINI_BUTTON_HEIGHT));

            // check if _minWindowSize and _maxWindowSize aren't too small
            if (_minWindowSize.Value.x < 100 || _minWindowSize.Value.y < 100)
            {
                _minWindowSize.Value = new Vector2(DEFAULT_MIN_WINDOW_WIDTH, DEFAULT_MIN_WINDOW_HEIGHT);
            }

            if (_maxWindowSize.Value.x < 100 || _maxWindowSize.Value.y < 100)
            {
                _maxWindowSize.Value = new Vector2(DEFAULT_MAX_WINDOW_WIDTH, DEFAULT_MAX_WINDOW_HEIGHT);
            }

            this.minSize = _minWindowSize;
            this.maxSize = _maxWindowSize;

            ButtonOptions = new GUILayoutOption[]
            {
                GUILayout.Width(_buttonSize.Value.x), GUILayout.Height(_buttonSize.Value.y), GUILayout.ExpandWidth(true)
            };
            MiniButtonOptions = new GUILayoutOption[]
            {
                GUILayout.Width(_miniButtonSize.Value.x), GUILayout.Height(_miniButtonSize.Value.y), GUILayout.ExpandWidth(true)
            };
        }

        protected static TWindowClass Initialize(string name = null)
        {
            name ??= typeof(TWindowClass).Name;
            TWindowClass window = (TWindowClass)GetWindow(typeof(TWindowClass), false, name);
            window.Show();
            window.autoRepaintOnSceneChange = true;
            return window;
        }

        protected void InternalMenuHeader(string title, ref bool boolValue)
        {
            /* centered button with width 100 */
            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Close"))
                {
                    boolValue = false;
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
        }

        protected void OpenSettings()
        {
            ExGUILayout.VerticalLayout(ExGUI.Box(10, GUIColor.None), () =>
            {
                float currentWindowSizeX = position.width;
                float currentWindowSizeY = position.height;
                InternalMenuHeader("Window Settings", ref IsShowingSettings);

                ExGUILayout.VerticalLayout(ExGUI.box,
                    () =>
                    {
                        EditorGUILayout.LabelField("Current Window Size : " + currentWindowSizeX + " x " +
                                                   currentWindowSizeY);
                    });

                GUILayout.Space(5);

                EditorGUILayout.LabelField("UI Window", EditorStyles.boldLabel);

                ExGUILayout.HorizontalLayout(() =>
                {
                    WindowName.Value = EditorGUILayout.TextField("Window Name", WindowName);
                    if (GUILayout.Button("Set Type Name", GUILayout.Width(100)))
                    {
                        WindowName.Value = GetType().Name;
                    }
                });

                ExGUILayout.HorizontalLayout(() =>
                {
                    _minWindowSize.Value = EditorGUILayout.Vector2Field("Minimum Window Size", _minWindowSize);
                    _maxWindowSize.Value = EditorGUILayout.Vector2Field("Maximum Window Size", _maxWindowSize);
                });

                GUILayout.Space(5);

                EditorGUILayout.LabelField("UI Buttons", EditorStyles.boldLabel);

                ExGUILayout.HorizontalLayout(() =>
                {
                    _buttonSize.Value = EditorGUILayout.Vector2Field("Button Size", _buttonSize);
                    _miniButtonSize.Value = EditorGUILayout.Vector2Field("Inner Button Size", _miniButtonSize);
                });

                GUILayout.Space(5);

                EditorGUILayout.LabelField("UI Images (Textures/Icons)", EditorStyles.boldLabel);

                ExGUILayout.HorizontalLayout(() =>
                {
                    _imageSize.Value = EditorGUILayout.Vector2Field("Image Size", _imageSize);
                    _iconSize.Value = EditorGUILayout.Vector2Field("Icon Size", _iconSize);
                });
            });
        }
    }
}