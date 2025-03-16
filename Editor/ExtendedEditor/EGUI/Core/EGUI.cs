using Glitch9.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// ReSharper disable All
#pragma warning disable IDE1006

namespace Glitch9.ExtendedEditor
{
    public static partial class EGUI
    {
        public static float TOLERANCE = 0.0001f;
        private static GUISkin _defaultSkin;
        private static GUIStyle _defaultBox;

        public static GUISkin skin
        {
            get
            {
                if (_defaultSkin == null) _defaultSkin = EGUISkin.skin;
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
                _ => EGUI.IsDarkMode ? Color.white : Color.black,
            };
        }

        public static string GetHexColor(GUIColor color)
        {
            return color switch
            {
                GUIColor.Green => "#0dff0d",
                GUIColor.Blue => "#4592ff",
                GUIColor.Yellow => "#FFFF00",
                GUIColor.Purple => "#a538ff",
                GUIColor.Red => "#ff3838",
                GUIColor.Orange => "#FFA500",
                GUIColor.Gray => "#808080",
                _ => EGUI.IsDarkMode ? "#FFFFFF" : "#000000",
            };
        }

        public static void Title(Rect rect, string label)
        {
            rect.y = rect.y;
            EditorGUI.LabelField(rect, label, EGUIStyles.title);
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
        public static GUIStyle Box(int margin = 0, GUIColor color = GUIColor.None)
            => EGUIStyles.Box(color, new RectOffset(margin, margin, margin, margin));
        public static GUIStyle Box(RectOffset margin, GUIColor color = GUIColor.None)
            => EGUIStyles.Box(color, margin);

        // Margin + Padding
        public static GUIStyle Box(int margin, int padding, GUIColor color = GUIColor.None)
            => EGUIStyles.Box(color, new RectOffset(margin, margin, margin, margin), new RectOffset(padding, padding, padding, padding));
        public static GUIStyle Box(RectOffset margin, RectOffset padding, GUIColor color = GUIColor.None)
            => EGUIStyles.Box(color, margin, padding);

        // Color goes last. When only need color
        public static GUIStyle Box(GUIColor color) => EGUIStyles.Box(color, new RectOffset(0, 0, 0, 0));


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
            EditorGUI.LabelField(rect, label, EGUIStyles.Label(alignment, fontSize, textColor, bold));
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
            EditorGUI.LabelField(rect, label, EGUIStyles.Label(alignment, fontSize, textColor, bold));
        }

        #endregion

        #region BoxField
        public static void BoxField(Rect rect, string label, GUIColor color) =>
            EditorGUI.LabelField(rect, label, EGUIStyles.Box(TextAnchor.MiddleCenter, color));

        public static void BoxField(Rect rect, string label, TextAnchor alignment, GUIColor color) =>
            EditorGUI.LabelField(rect, label, EGUIStyles.Box(alignment, color));

        public static void BoxField(Rect rect, string label, TextAnchor alignment, int fontSize = 12,
            GUIColor color = GUIColor.None) =>
            EditorGUI.LabelField(rect, label, EGUIStyles.Box(alignment, fontSize, color));

        #endregion

        #region TreeViewField

        public static void TreeViewField(Rect rect, string label, GUIColor color) =>
            EditorGUI.LabelField(rect, label, EGUIStyles.TreeViewField(TextAnchor.MiddleCenter, color));

        public static void TreeViewField(Rect rect, string label, TextAnchor alignment, GUIColor color) =>
            EditorGUI.LabelField(rect, label, EGUIStyles.TreeViewField(alignment, color));

        public static void TreeViewField(Rect rect, string label, TextAnchor alignment = TextAnchor.MiddleCenter,
            int fontSize = 12, GUIColor color = GUIColor.None) =>
            EditorGUI.LabelField(rect, label, EGUIStyles.TreeViewField(alignment, fontSize, color));

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

        public static string ResizableStringPopup(Rect rect, string currentValue, IList<string> list, GUIContent label = null)
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

        public static int ResizableIntPopup(Rect rect, string prefix, int selected, int[] optionsValues, GUIStyle style = null, GUIContent label = null)
        {
            if (label != null)
            {
                rect = EditorGUI.PrefixLabel(rect, label);
            }

            string[] options = Array.ConvertAll(optionsValues, item => item.ToString());
            int index = Array.IndexOf(optionsValues, selected);

            GUIStyle resizablePopupStyle = style ?? new GUIStyle(EditorStyles.popup);
            resizablePopupStyle.fixedHeight = rect.height;

            string selectedString;

            if (!string.IsNullOrEmpty(prefix)) selectedString = $"{prefix} {options[index]}";
            else selectedString = options[index];

            if (GUI.Button(rect, selectedString, resizablePopupStyle))
            {
                GenericMenu menu = new();
                for (int i = 0; i < options.Length; i++)
                {
                    int localValue = optionsValues[i];
                    menu.AddItem(new GUIContent(options[i]), i == index, () => selected = localValue);
                }

                menu.DropDown(rect);
            }

            return selected;
        }

        #endregion

        #region EnumPopup

        public static T EnumPopup<T>(Rect rect, T value) where T : Enum
        {
            return EnumPopup(rect, GUIContent.none, value);
        }

        public static T EnumPopup<T>(Rect rect, string label, T value) where T : Enum
        {
            return EnumPopup(rect, new GUIContent(label), value);
        }

        public static T EnumPopup<T>(Rect rect, GUIContent label, T value) where T : Enum
        {
            if (label != GUIContent.none)
            {
                rect = EditorGUI.PrefixLabel(rect, label);
            }
            string[] displayNames = EnumUtils.GetDisplayNames(typeof(T));
            int enumIndex = Convert.ToInt32(value);

            GUIStyle resizablePopupStyle = new(EditorStyles.popup);
            resizablePopupStyle.fixedHeight = rect.height;
            int newEnumIndex = EditorGUI.Popup(rect, enumIndex, displayNames, resizablePopupStyle);
            T newValue = (T)Enum.ToObject(typeof(T), newEnumIndex);
            return newValue;
        }

        public static TEnum ResizableEnumPopup<TEnum>(Rect rect, TEnum selected) where TEnum : Enum
        {
            string[] names = Enum.GetNames(typeof(TEnum));
            GUIContent label = new(selected.ToString());

            GUIStyle resizablePopupStyle = new(EditorStyles.popup);
            resizablePopupStyle.fixedHeight = rect.height;
            TEnum selectedCopy = selected;

            if (GUI.Button(rect, label, resizablePopupStyle))
            {
                GenericMenu menu = new();
                for (int i = 0; i < names.Length; i++)
                {
                    TEnum localValue = (TEnum)Enum.Parse(typeof(TEnum), names[i]);
                    menu.AddItem(new GUIContent(names[i]), Equals(selected, localValue), () =>
                    {
                        selectedCopy = localValue;
                    });
                }
                menu.DropDown(rect);
            }

            return selectedCopy;
        }

        public static GUIColor GUIColorPopup(Rect rect, GUIColor color)
        {
            string[] names = Enum.GetNames(typeof(GUIColor));
            GUIContent label = new(color.ToString());

            GUIStyle resizablePopupStyle = new(EditorStyles.popup)
            {
                fixedHeight = rect.height
            };
            GUIColor selected = color;

            if (GUI.Button(rect, label, resizablePopupStyle))
            {
                GenericMenu menu = new();
                for (int i = 0; i < names.Length; i++)
                {
                    GUIColor localValue = (GUIColor)Enum.Parse(typeof(GUIColor), names[i]);

                    menu.AddItem(new GUIContent(names[i]), Equals(selected, localValue), () =>
                    {
                        selected = localValue;
                    });
                }
                menu.DropDown(rect);
            }

            return selected;
        }

        #endregion

        #region ProgressBar

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
                        Rect position1 = new(position);
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
                        Rect position2 = new(position.x + num5 + num4, position.y, width, position.height);
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

        #endregion

        #region TimeField

        public static DateTime DateTimeField(Rect rect, GUIContent label, DateTime dateTime, bool year, bool month, bool day, bool hour = false, bool minute = false, bool second = false)
        {
            float originalX = rect.x;
            const float SMALL_SPACE = 10f;
            const float LARGE_SPACE = 20f;

            if (label != null)
            {
                float labelWidth = EditorGUIUtility.labelWidth;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, rect.height), label);
                rect.x += labelWidth;
            }

            int YY = dateTime.Year;
            int MM = dateTime.Month;
            int DD = dateTime.Day;
            int hh = dateTime.Hour;
            int mm = dateTime.Minute;
            int ss = dateTime.Second;

            if (year)
            {
                rect.width = 50;
                YY = EditorGUI.IntField(rect, YY);
                rect.x += rect.width + SMALL_SPACE;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, SMALL_SPACE, rect.height), "-");
                rect.x += SMALL_SPACE;
            }

            if (month)
            {
                rect.width = 30;
                MM = EditorGUI.IntField(rect, MM);
                rect.x += rect.width + SMALL_SPACE;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, SMALL_SPACE, rect.height), "-");
                rect.x += SMALL_SPACE;
            }

            if (day)
            {
                rect.width = 30;
                DD = EditorGUI.IntField(rect, DD);
                rect.x += rect.width;
            }

            if (hour)
            {
                rect.x += LARGE_SPACE;
                rect.width = 30;
                hh = EditorGUI.IntField(rect, hh);
                rect.x += rect.width + SMALL_SPACE;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, SMALL_SPACE, rect.height), ":");
                rect.x += SMALL_SPACE;
            }

            if (minute)
            {
                rect.width = 30;
                mm = EditorGUI.IntField(rect, mm);
                rect.x += rect.width + SMALL_SPACE;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, SMALL_SPACE, rect.height), ":");
                rect.x += SMALL_SPACE;
            }

            if (second)
            {
                rect.width = 30;
                ss = EditorGUI.IntField(rect, ss);
            }

            return new DateTime(YY, MM, DD, hh, mm, ss);
        }

        public static UnixTime UnixTimeField(Rect rect, GUIContent label, UnixTime unixTime, bool year, bool month, bool day, bool hour = false, bool minute = false, bool second = false)
        {
            float originalX = rect.x;
            const float SMALL_SPACE = 10f;
            const float LARGE_SPACE = 20f;

            if (label != null)
            {
                float labelWidth = EditorGUIUtility.labelWidth;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, rect.height), label);
                rect.x += labelWidth;
            }

            int YY = unixTime.Year;
            int MM = unixTime.Month;
            int DD = unixTime.Day;
            int hh = unixTime.Hour;
            int mm = unixTime.Minute;
            int ss = unixTime.Second;

            if (year)
            {
                rect.width = 50;
                YY = EditorGUI.IntField(rect, YY);
                rect.x += rect.width + SMALL_SPACE;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, SMALL_SPACE, rect.height), "-");
                rect.x += SMALL_SPACE;
            }

            if (month)
            {
                rect.width = 30;
                MM = EditorGUI.IntField(rect, MM);
                rect.x += rect.width + SMALL_SPACE;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, SMALL_SPACE, rect.height), "-");
                rect.x += SMALL_SPACE;
            }

            if (day)
            {
                rect.width = 30;
                DD = EditorGUI.IntField(rect, DD);
                rect.x += rect.width;
            }

            if (hour)
            {
                rect.x += LARGE_SPACE;
                rect.width = 30;
                hh = EditorGUI.IntField(rect, hh);
                rect.x += rect.width + SMALL_SPACE;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, SMALL_SPACE, rect.height), ":");
                rect.x += SMALL_SPACE;
            }

            if (minute)
            {
                rect.width = 30;
                mm = EditorGUI.IntField(rect, mm);
                rect.x += rect.width + SMALL_SPACE;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, SMALL_SPACE, rect.height), ":");
                rect.x += SMALL_SPACE;
            }

            if (second)
            {
                rect.width = 30;
                ss = EditorGUI.IntField(rect, ss);
            }

            return new UnixTime(YY, MM, DD, hh, mm, ss);
        }

        #endregion
    }
}