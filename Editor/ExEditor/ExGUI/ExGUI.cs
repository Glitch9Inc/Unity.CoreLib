using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable All
#pragma warning disable IDE1006

namespace Glitch9.ExEditor
{
    public static partial class ExGUI
    {
        public static float TOLERANCE = 0.0001f;
        private static GUISkin _defaultSkin;
        private static GUIStyle _defaultBox;

        public static GUISkin skin
        {
            get
            {
                if (_defaultSkin == null) _defaultSkin = EditorSkin.skin;
                return _defaultSkin;
            }
        }

        public static GUIStyle box
        {
            get
            {
                _defaultBox ??= new GUIStyle(GUI.skin.box)
                {
                    margin = new RectOffset(2, 2, 2, 2),
                    border = new RectOffset(10, 10, 10, 10),
                    padding = new RectOffset(6, 6, 6, 6)
                };
                return _defaultBox;
            }
        }

        public static bool IsDarkMode
        {
            get
            {
                try
                {
                    return EditorGUIUtility.isProSkin;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static Color GetFontColor(GUIColor color)
        {
            return color switch
            {
                GUIColor.Green => ExColor.teal,
                GUIColor.Blue => ExColor.azure,
                GUIColor.Yellow => ExColor.citrine,
                GUIColor.Purple => ExColor.purple,
                GUIColor.Red => ExColor.rose,
                GUIColor.Orange => ExColor.orange,
                GUIColor.Gray => Color.gray,
                _ => Color.black,
            };
        }

        public static void Title(Rect rect, string label)
        {
            rect.y = rect.y;
            EditorGUI.LabelField(rect, label, ExEditorStyles.title);
            float thickness = 1.2f;
            float height = 5f;
            Rect r = new(rect.x, rect.y + rect.height - 5, rect.width, height);
            r.height = thickness;
            r.width -= 4;
            EditorGUI.DrawRect(r, Color.gray);
        }

        public static Rect GetHeaderRect(Rect r, float indent = 10, float margin = 6, float width = 22, float height = 22) =>
            new(r.x + indent, r.y + margin, width, height);


        #region Box

        // Margin goes first.
        public static GUIStyle Box(int margin = 0, GUIColor color = GUIColor.None) => ExEditorStyles.Box(color, new RectOffset(margin, margin, margin, margin));
        public static GUIStyle Box(RectOffset margin, GUIColor color = GUIColor.None) => ExEditorStyles.Box(color, margin);

        // Margin + Padding
        public static GUIStyle Box(int margin, int padding, GUIColor color = GUIColor.None) => ExEditorStyles.Box(color, new RectOffset(margin, margin, margin, margin), new RectOffset(padding, padding, padding, padding));
        public static GUIStyle Box(RectOffset margin, RectOffset padding, GUIColor color = GUIColor.None) => ExEditorStyles.Box(color, margin, padding);

        // Color goes last. When only need color
        public static GUIStyle Box(GUIColor color) => ExEditorStyles.Box(color, new RectOffset(0, 0, 0, 0));


        #endregion

        #region DrawTexture

        public static void DrawTexture(Rect textureRect, Texture texture) =>
            GUI.DrawTexture(textureRect, texture, ScaleMode.ScaleToFit);

        public static void DrawContent(Rect textureRect, GUIContent content) =>
            GUI.DrawTexture(textureRect, content.image, ScaleMode.ScaleToFit);

        #endregion

        #region Label

        public static void Label(Rect rect, GUIContent label, Color textColor, bool bold = false) =>
            Label(rect, label, TextAnchor.MiddleLeft, 12, textColor, bold);

        public static void Label(Rect rect, GUIContent label, int fontSize, bool bold = false) =>
            Label(rect, label, TextAnchor.MiddleLeft, fontSize, Color.black, bold);

        public static void Label(Rect rect, GUIContent label, int fontSize, Color textColor, bool bold = false) =>
            Label(rect, label, TextAnchor.MiddleLeft, fontSize, textColor, bold);

        public static void Label(Rect rect, GUIContent label, TextAnchor alignment = TextAnchor.MiddleLeft,
            int fontSize = 10, bool bold = false) =>
            Label(rect, label, alignment, fontSize, Color.black, bold);

        public static void Label(Rect rect, GUIContent label, TextAnchor alignment, int fontSize, Color textColor,
            bool bold = false)
        {
            GUIStyle style = bold ? new GUIStyle(EditorStyles.boldLabel) : new GUIStyle(GUI.skin.label);
            style.normal.textColor = textColor;
            EditorGUI.LabelField(rect, label, ExEditorStyles.Label(alignment, fontSize, textColor, bold));
        }

        public static void Label(Rect rect, string label, Color textColor, bool bold = false) =>
            Label(rect, label, TextAnchor.MiddleLeft, 12, textColor, bold);

        public static void Label(Rect rect, string label, int fontSize, bool bold = false) =>
            Label(rect, label, TextAnchor.MiddleLeft, fontSize, Color.black, bold);

        public static void Label(Rect rect, string label, int fontSize, Color textColor, bool bold = false) =>
            Label(rect, label, TextAnchor.MiddleLeft, fontSize, textColor, bold);

        public static void Label(Rect rect, string label, TextAnchor alignment = TextAnchor.MiddleLeft,
            int fontSize = 10, bool bold = false) =>
            Label(rect, label, alignment, fontSize, Color.black, bold);

        public static void Label(Rect rect, string label, TextAnchor alignment, int fontSize, Color textColor,
            bool bold = false)
        {
            GUIStyle style = bold ? new GUIStyle(EditorStyles.boldLabel) : new GUIStyle(GUI.skin.label);
            style.normal.textColor = textColor;
            EditorGUI.LabelField(rect, label, ExEditorStyles.Label(alignment, fontSize, textColor, bold));
        }

        #endregion


        #region BoxField
        public static void BoxField(Rect rect, string label, GUIColor color) =>
            EditorGUI.LabelField(rect, label, ExEditorStyles.Box(TextAnchor.MiddleCenter, color));

        public static void BoxField(Rect rect, string label, TextAnchor alignment, GUIColor color) =>
            EditorGUI.LabelField(rect, label, ExEditorStyles.Box(alignment, color));

        public static void BoxField(Rect rect, string label, TextAnchor alignment, int fontSize = 12,
            GUIColor color = GUIColor.None) =>
            EditorGUI.LabelField(rect, label, ExEditorStyles.Box(alignment, fontSize, color));

        #endregion

        #region TreeViewField

        public static void TreeViewField(Rect rect, string label, GUIColor color) =>
            EditorGUI.LabelField(rect, label, ExEditorStyles.TreeViewField(TextAnchor.MiddleCenter, color));

        public static void TreeViewField(Rect rect, string label, TextAnchor alignment, GUIColor color) =>
            EditorGUI.LabelField(rect, label, ExEditorStyles.TreeViewField(alignment, color));

        public static void TreeViewField(Rect rect, string label, TextAnchor alignment = TextAnchor.MiddleCenter,
            int fontSize = 12, GUIColor color = GUIColor.None) =>
            EditorGUI.LabelField(rect, label, ExEditorStyles.TreeViewField(alignment, fontSize, color));

        #endregion


        #region ListPopup

        private static T GenericListPopup<T>(Rect rect, T currentValue, IList<T> list, GUIContent label = null)
        {
            if (list == null || list.Count == 0)
            {
                EditorGUI.HelpBox(rect, "No list found.", MessageType.None);
                return default;
            }

            if (label != null)
            {
                rect = EditorGUI.PrefixLabel(rect, label);
            }

            int index = list.IndexOf(currentValue);
            string[] options = Array.ConvertAll(list.ToArray(), item => item.ToString());

            index = EditorGUI.Popup(rect, index, options);
            return list[Mathf.Max(index, 0)];
        }

        public static string ListPopup(Rect rect, string currentValue, IList<string> list, GUIContent label = null)
            => GenericListPopup(rect, currentValue, list, label);


        private static T GenericDropdownField<T>(Rect rect, T currentValue, IList<T> list, GUIContent label = null, params GUILayoutOption[] options)
        {
            if (list == null || list.Count == 0)
            {
                EditorGUI.HelpBox(rect, "No list found.", MessageType.None);
                return default;
            }

            int index = list.IndexOf(currentValue);
            List<string> stringArray = list.Select(enumValue => enumValue.ToString()).ToList();

            index = EditorGUI.Popup(rect, index, stringArray.ToArray());

            if (index < 0) index = 0;
            return list[index];
        }

        public static string StringListDropdown(Rect rect, string currentValue, IList<string> list, GUIContent label = null)
            => GenericDropdownField(rect, currentValue, list, label);

        #endregion

        public static string ResizableListPopup(Rect rect, string currentValue, IList<string> list, GUIContent label = null)
        {
            if (list == null || list.Count == 0)
            {
                EditorGUI.HelpBox(rect, "No list found.", MessageType.None);
                return default;
            }

            if (label != null)
            {
                rect = EditorGUI.PrefixLabel(rect, label);
            }

            int index = list.IndexOf(currentValue);
            string[] options = Array.ConvertAll(list.ToArray(), item => item.ToString());

            // The regular EnumPopup has a fixed height, so we need to use a custom implementation to make it resizable.
            // Make a button that shows the currently selected enum value, with a dropdown arrow to the right.
            GUIStyle dropdownStyle = new(EditorStyles.popup);
            dropdownStyle.fixedHeight = rect.height;

            if (GUI.Button(rect, options[Mathf.Max(index, 0)], dropdownStyle))
            {
                // When the button is clicked, show the dropdown.
                GenericMenu menu = new();
                for (int i = 0; i < options.Length; i++)
                {
                    int indexCopy = i;
                    menu.AddItem(new GUIContent(options[i]), i == index, () => index = indexCopy);
                }

                menu.DropDown(rect);
            }

            return list[Mathf.Max(index, 0)];
        }

        public static void ResizableEnumPopup<TEnum>(Rect rect, TEnum selected, Action<TEnum> onSelect) where TEnum : Enum
        {
            string[] names = Enum.GetNames(typeof(TEnum));
            GUIContent label = new(selected.ToString());

            GUIStyle resizablePopupStyle = new(EditorStyles.popup);
            resizablePopupStyle.fixedHeight = rect.height;

            if (GUI.Button(rect, label, resizablePopupStyle))
            {
                GenericMenu menu = new();
                for (int i = 0; i < names.Length; i++)
                {
                    TEnum localValue = (TEnum)Enum.Parse(typeof(TEnum), names[i]);
                    menu.AddItem(new GUIContent(names[i]), Equals(selected, localValue), () =>
                    {
                        onSelect(localValue);
                    });
                }
                menu.DropDown(rect);
            }
        }

        private static int s_ProgressBarHash = "ProgressBar".GetHashCode();

        public static bool ProgressBar(
          Rect position,
          float value,
          GUIContent content,
          GUIStyle progressBarBackgroundStyle,
          GUIStyle progressBarStyle,
          GUIStyle progressBarTextStyle)
        {
            bool isHover = position.Contains(UnityEngine.Event.current.mousePosition);
            switch (UnityEngine.Event.current.GetTypeForControl(GUIUtility.GetControlID(s_ProgressBarHash, FocusType.Keyboard, position)))
            {
                case UnityEngine.EventType.MouseDown:
                    if (isHover)
                    {
                        UnityEngine.Event.current.Use();
                        return true;
                    }
                    break;
                case UnityEngine.EventType.Repaint:
                    progressBarBackgroundStyle.Draw(position, isHover, false, false, false);
                    if ((double)value > 0.0)
                    {
                        value = Mathf.Clamp01(value);
                        Rect position1 = new Rect(position);
                        position1.width *= value;
                        if ((double)position1.width >= 1.0)
                            progressBarStyle.Draw(position1, GUIContent.none, isHover, false, false, false);
                    }
                    else if ((double)value == -1.0)
                    {
                        float width = position.width * 0.2f;
                        float num1 = width / 2f;
                        float num2 = Mathf.Cos((float)EditorApplication.timeSinceStartup * 2f);
                        float num3 = position.x + num1;
                        float num4 = (float)(((double)(position.xMax - num1) - (double)num3) / 2.0);
                        float num5 = num4 * num2;
                        Rect position2 = new Rect(position.x + num5 + num4, position.y, width, position.height);
                        progressBarStyle.Draw(position2, GUIContent.none, isHover, false, false, false);
                    }
                    GUIContent content1 = content;
                    float x = progressBarTextStyle.CalcSize(content1).x;
                    if ((double)x > (double)position.width)
                    {
                        int num6 = (int)((double)position.width / (double)x * (double)content.text.Length);
                        int num7 = 0;
                        do
                        {
                            int length = num6 / 2 - 2 - num7;
                            content1.text = content.text.Substring(0, length) + "..." + content.text.Substring(content.text.Length - length, length);
                            ++num7;
                        }
                        while ((double)progressBarTextStyle.CalcSize(content1).x > (double)position.width);
                    }
                    progressBarTextStyle.Draw(position, content1, isHover, false, false, false);
                    break;
            }
            return false;
        }
    }
}