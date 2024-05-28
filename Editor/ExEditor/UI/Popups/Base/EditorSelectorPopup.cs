using System;
using System.Collections.Generic;
using Glitch9.ExEditor;
using UnityEditor;
using UnityEngine;

namespace Glitch9
{
    public abstract class EditorSelectorPopup<TWindow, TValue> : EditorWindow where TWindow : EditorSelectorPopup<TWindow, TValue>
    {
        protected const float WINDOW_MIN_HEIGHT = 100f;
        protected const float WINDOW_MAX_HEIGHT = 980f;
        protected const float WINDOW_WIDTH = 340f;
        private Vector2 _scrollPos;
        protected Action<TValue> Callback;

        protected string Title = "Editor Selector Window";
        protected string Description = "";
        protected TValue Value;
        protected List<TValue> ValueList;
        protected bool ShowTitle = true;

        private void OnGUI()
        {
            // Draw Title
            if (ShowTitle)
            {
                ExGUILayout.Title(Title);
            }
            else
            {
                GUILayout.Space(7);
            }
        
            EditorGUILayout.LabelField(Description, EditorStyles.wordWrappedLabel);
            EditorGUILayout.Space();

            // Draw Content
            EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(true));
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
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
            if (onComplete == null) throw new ArgumentNullException(nameof(onComplete), "Callback cannot be null");

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
                Debug.LogError("Failed to create window of type " + typeof(TWindow));
            }
        }

        protected virtual void Initialize()
        {
            minSize = new Vector2(WINDOW_WIDTH, WINDOW_MIN_HEIGHT);
            maxSize = new Vector2(WINDOW_WIDTH, WINDOW_MAX_HEIGHT);
        }


        protected abstract TValue DrawContent(TValue value);

        private void DrawButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("OK", GUILayout.Height(24)))
            {
                Callback?.Invoke(Value);
                Close();
            }

            if (GUILayout.Button("Cancel", GUILayout.Height(24)))
            {
                Close();
            }

            EditorGUILayout.EndHorizontal();
        }

        protected void SetDefaultValue(TValue value)
        {
            Value = value;
        }
    }
}