using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Glitch9.ExtendedEditor
{
    /// <summary>
    /// A popup window that allows the user to select a value from a list of values.
    /// </summary>
    /// <typeparam name="TWindow">The type of the window.</typeparam>
    /// <typeparam name="TValue">The type of the value to be selected.</typeparam>
    public abstract class EditorSelectorPopup<TWindow, TValue> : EditorWindow
        where TWindow : EditorSelectorPopup<TWindow, TValue>
    {
        private static class Strings
        {
            internal const string DEFAULT_TITLE = "Editor Selector Window";
            internal const string DEFAULT_DESCRIPTION = "";
            internal const string DEFAULT_BUTTON_TEXT = "Select";
            internal const string DEFAULT_CANCEL_TEXT = "Cancel";
            internal const string ERROR_NO_CALLBACK = "No callback function provided.";
            internal const string ERROR_FAILED_TO_CREATE = "Failed to create window of type {0}.";
        }

        protected const float WINDOW_MIN_HEIGHT = 100f;
        protected const float WINDOW_MAX_HEIGHT = 980f;
        protected const float WINDOW_WIDTH = 340f;
        private Vector2 _scrollPosition;
        protected Action<TValue> Callback;

        protected string Title = Strings.DEFAULT_TITLE;
        protected string Description = Strings.DEFAULT_DESCRIPTION;
        protected TValue Value;
        protected List<TValue> ValueList;
        protected bool ShowTitle = true;

        private void OnGUI()
        {
            // Draw Title
            if (ShowTitle)
            {
                InternalGUI.Title(Title);
            }
            else
            {
                GUILayout.Space(7);
            }

            EditorGUILayout.LabelField(Description, EditorStyles.wordWrappedLabel);
            EditorGUILayout.Space();

            // Draw Content
            EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(true));
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            try
            {
                Value = DrawContent(Value);
            }
            finally
            {
                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndVertical();
            }

            // Draw Buttons
            DrawButtons();
        }

        public static void Show(string title, Action<TValue> onComplete, IEnumerable<TValue> valueList = null)
        {
            Show(title, null, default(TValue), true, onComplete, valueList);
        }

        public static void Show(string title, bool showTitle, Action<TValue> onComplete, IEnumerable<TValue> valueList = null)
        {
            Show(title, null, default(TValue), showTitle, onComplete, valueList);
        }

        public static void Show(string title, TValue defaultValue, Action<TValue> onComplete, IEnumerable<TValue> valueList = null)
        {
            Show(title, null, defaultValue, true, onComplete, valueList);
        }

        public static void Show(string title, TValue defaultValue, bool showTitle, Action<TValue> onComplete, IEnumerable<TValue> valueList = null)
        {
            Show(title, null, defaultValue, showTitle, onComplete, valueList);
        }

        public static void Show(string title, string description, Action<TValue> onComplete, IEnumerable<TValue> valueList = null)
        {
            Show(title, description, default(TValue), true, onComplete, valueList);
        }

        public static void Show(string title, string description, bool showTitle, Action<TValue> onComplete, IEnumerable<TValue> valueList = null)
        {
            Show(title, description, default(TValue), showTitle, onComplete, valueList);
        }

        public static void Show(string title, string description, TValue defaultValue, Action<TValue> onComplete, IEnumerable<TValue> valueList = null)
        {
            Show(title, description, defaultValue, true, onComplete, valueList);
        }

        public static void Show(string title, string description, TValue defaultValue, bool showTitle, Action<TValue> onComplete, IEnumerable<TValue> valueList = null)
        {
            if (onComplete == null) throw new ArgumentNullException(nameof(onComplete), Strings.ERROR_NO_CALLBACK);

            try
            {
                TWindow window = GetWindow<TWindow>(true, title, true);
                window.Title = title;
                window.Description = description;
                window.Callback = onComplete;
                window.Value = defaultValue;
                window.ValueList = valueList != null ? new List<TValue>(valueList) : null;
                window.ShowTitle = showTitle;
                window.Initialize();
                window.Show();
            }
            catch
            {
                Debug.LogError(string.Format(Strings.ERROR_FAILED_TO_CREATE, typeof(TWindow).Name));
            }
        }

        protected virtual void Initialize()
        {
            minSize = new Vector2(WINDOW_WIDTH, WINDOW_MIN_HEIGHT);
            maxSize = new Vector2(WINDOW_WIDTH, WINDOW_MAX_HEIGHT);
        }

        /// <summary>
        /// Abstract method to draw the content of the window. Must be implemented by derived classes to render the selection UI.
        /// </summary>
        /// <param name="value">The current selection value.</param>
        /// <returns>The updated value after user interaction.</returns>
        protected abstract TValue DrawContent(TValue value);

        /// <summary>
        /// Draws OK and Cancel buttons at the bottom of the popup.
        /// </summary>
        private void DrawButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(Strings.DEFAULT_BUTTON_TEXT, GUILayout.Height(24)))
            {
                Callback?.Invoke(Value);
                Close();
            }

            if (GUILayout.Button(Strings.DEFAULT_CANCEL_TEXT, GUILayout.Height(24)))
            {
                Close();
            }

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Sets the default value for the selector, used when the popup is displayed.
        /// </summary>
        /// <param name="value">The value to set as default.</param>
        protected void SetDefaultValue(TValue value)
        {
            Value = value;
        }
    }
}