using Glitch9.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using MessageType = UnityEditor.MessageType;
using Object = UnityEngine.Object;

namespace Glitch9.ExtendedEditor
{
    public partial class EGUILayout
    {
        #region Labels
        public static void Label(GUIContent label, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(TextAnchor.UpperLeft), options);
        public static void Label(GUIContent label, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(TextAnchor.UpperLeft, 12, color), options);
        public static void Label(GUIContent label, int fontSize, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(TextAnchor.UpperLeft, fontSize, color), options);
        public static void Label(GUIContent label, int fontSize, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(TextAnchor.UpperLeft, fontSize), options);
        public static void Label(GUIContent label, TextAnchor alignment, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(alignment, 12, color), options);
        public static void Label(GUIContent label, TextAnchor alignment = TextAnchor.UpperLeft, int fontSize = 12, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(alignment, fontSize, GUIColor.None), options);
        public static void Label(GUIContent label, TextAnchor alignment = TextAnchor.UpperLeft, int fontSize = 12, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(alignment, fontSize, color), options);

        public static void Label(string label, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(TextAnchor.UpperLeft), options);
        public static void Label(string label, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(TextAnchor.UpperLeft, 12, color), options);
        public static void Label(string label, int fontSize, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(TextAnchor.UpperLeft, fontSize, color), options);
        public static void Label(string label, int fontSize, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(TextAnchor.UpperLeft, fontSize), options);
        public static void Label(string label, TextAnchor alignment, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(alignment, 12, color), options);
        public static void Label(string label, TextAnchor alignment = TextAnchor.UpperLeft, int fontSize = 12, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(alignment, fontSize, GUIColor.None), options);
        public static void Label(string label, TextAnchor alignment = TextAnchor.UpperLeft, int fontSize = 12, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Label(alignment, fontSize, color), options);
        #endregion

        #region CheckBox
        public static bool CheckBox(string label, bool value)
        {
            GUILayout.BeginHorizontal();
            value = EditorGUILayout.Toggle(value, GUILayout.Width(10));
            GUILayout.Label(new GUIContent(label), GUILayout.MinWidth(10));
            GUILayout.EndHorizontal();
            return value;
        }

        #endregion

        #region Toggles
        public static bool Toggle(GUIContent content, bool isOn, params GUILayoutOption[] options)
        {
            if (isOn) GUI.backgroundColor = new Color(0.5f, 0.9f, 0.9f);
            if (GUILayout.Button(content, options))
            {
                isOn = !isOn;
            }
            GUI.backgroundColor = Color.white;
            return isOn;
        }
        public static bool Toggle(string label, bool isOn, params GUILayoutOption[] options)
            => Toggle(new GUIContent(label), isOn, options);
        public static bool Toggle(Texture2D tex, bool isOn, params GUILayoutOption[] options)
            => Toggle(new GUIContent(tex), isOn, options);
        #endregion

        #region Toolbar Toggles
        public static bool ToolbarMid(GUIContent content, bool isOn, params GUILayoutOption[] options)
            => GUILayout.Toggle(isOn, content, EditorStyles.miniButtonMid, options);
        public static bool ToolbarMid(string label, bool isOn, params GUILayoutOption[] options)
            => ToolbarMid(new GUIContent(label), isOn, options);
        public static bool ToolbarMid(Texture2D tex, bool isOn, params GUILayoutOption[] options)
            => ToolbarMid(new GUIContent(tex), isOn, options);
        public static bool ToolbarLeft(GUIContent content, bool isOn, params GUILayoutOption[] options)
            => GUILayout.Toggle(isOn, content, EditorStyles.miniButtonLeft, options);
        public static bool ToolbarLeft(string label, bool isOn, params GUILayoutOption[] options)
            => ToolbarLeft(new GUIContent(label), isOn, options);
        public static bool ToolbarLeft(Texture2D tex, bool isOn, params GUILayoutOption[] options)
            => ToolbarLeft(new GUIContent(tex), isOn, options);
        public static bool ToolbarRight(GUIContent content, bool isOn, params GUILayoutOption[] options)
            => GUILayout.Toggle(isOn, content, EditorStyles.miniButtonRight, options);
        public static bool ToolbarRight(string label, bool isOn, params GUILayoutOption[] options)
            => ToolbarRight(new GUIContent(label), isOn, options);
        public static bool ToolbarRight(Texture2D tex, bool isOn, params GUILayoutOption[] options)
            => ToolbarRight(new GUIContent(tex), isOn, options);
        #endregion

        #region BoxedLabel
        public static void BoxedLabel(GUIContent label, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Box(TextAnchor.MiddleCenter), options);
        public static void BoxedLabel(GUIContent label, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Box(TextAnchor.MiddleCenter, color), options);
        public static void BoxedLabel(GUIContent label, int fontSize, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Box(TextAnchor.MiddleCenter, fontSize, color), options);
        public static void BoxedLabel(GUIContent label, TextAnchor alignment, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Box(alignment, color), options);
        public static void BoxedLabel(GUIContent label, TextAnchor alignment = TextAnchor.MiddleCenter, int fontSize = 12, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Box(alignment, fontSize, color), options);

        public static void BoxedLabel(string label, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Box(TextAnchor.MiddleCenter), options);
        public static void BoxedLabel(string label, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Box(TextAnchor.MiddleCenter, color), options);
        public static void BoxedLabel(string label, int fontSize, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Box(TextAnchor.MiddleCenter, fontSize), options);
        public static void BoxedLabel(string label, int fontSize, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Box(TextAnchor.MiddleCenter, fontSize, color), options);
        public static void BoxedLabel(string label, int fontSize, TextAnchor alignment, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Box(alignment, fontSize, GUIColor.None), options);
        public static void BoxedLabel(string label, TextAnchor alignment, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Box(alignment, color), options);
        public static void BoxedLabel(string label, TextAnchor alignment = TextAnchor.MiddleCenter, int fontSize = 12, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, EGUIStyles.Box(alignment, fontSize, color), options);
        #endregion

        #region Boxed Layout

        public static void BoxedLayout(string label, Action callback, Texture texture = null)
        {
            Rect r = (Rect)EditorGUILayout.BeginVertical(EGUI.skin.box);
            if (texture == null) texture = EditorIcons.NoImageHighRes;
            /* Header */
            GUI.DrawTexture(new Rect(r.position.x + 10, r.position.y + 5, 24, 24), texture);
            GUI.skin.label.fontSize = 14;
            GUI.Label(EGUI.GetHeaderRect(r, indent: 40, width: r.width), label.ToUpper());
            GUI.skin.label.fontSize = 12;
            GUILayout.Space(30);
            callback?.Invoke();
            EditorGUILayout.EndVertical();
        }

        public static void BoxedLayout(GUIContent label, Action callback, Texture texture = null)
        {
            BoxedLayout(label.text, callback, texture);
        }

        #endregion

        #region Info Fields

        public static void InfoField(string label, string content, float labelWidth = -1f, int fontSize = 12, bool boldLabel = false, params GUILayoutOption[] options)
        {
            InfoField(label, new GUIContent(content), labelWidth, fontSize, boldLabel, options);
        }

        public static void InfoField(string label, GUIContent content, float labelWidth = -1f, int fontSize = 12, bool boldLabel = false, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(options);
            {
                if (Math.Abs(labelWidth - (-1f)) < EGUI.TOLERANCE) labelWidth = EditorGUIUtility.labelWidth;

                // label 스타일 생성
                GUIStyle labelStyle;
                GUIStyle infoStyle = new(EditorStyles.label);

                infoStyle.alignment = TextAnchor.MiddleLeft;
                infoStyle.fontSize = fontSize;

                if (boldLabel)
                {
                    labelStyle = new GUIStyle(EditorStyles.boldLabel);
                    labelStyle.alignment = TextAnchor.MiddleLeft;
                }
                else
                {
                    labelStyle = infoStyle;
                }

                if (string.IsNullOrEmpty(content.text)) content.text = "-";

                EditorGUILayout.LabelField(label, labelStyle, GUILayout.Width(labelWidth));
                EditorGUILayout.LabelField(content, infoStyle, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            }
            GUILayout.EndHorizontal();
        }


        #endregion

        #region Description Fields
        public static string DescriptionField(string text, params GUILayoutOption[] options)
            => EditorGUILayout.TextArea(text, EGUI.skin.textArea, options);
        public static string DescriptionField(SerializedProperty property, params GUILayoutOption[] options)
            => EditorGUILayout.TextArea(property.stringValue, EGUI.skin.textArea, options);
        #endregion

        #region Texture Fields

        public static void TextureField(Texture texture, Vector2? size = null, float yOffset = 0)
        {
            try
            {
                size ??= new Vector2(texture.width, texture.height);
                Rect rect = GUILayoutUtility.GetRect(size.Value.x, size.Value.y + yOffset);
                GUI.DrawTexture(rect, texture != null ? texture : EditorIcons.NoImageHighRes, ScaleMode.ScaleToFit);
            }
            catch
            {
                // do nothing
            }
        }
        public static void TextureField(Object asset, Vector2? size = null)
            => TextureField(asset == null ? null : AssetPreview.GetAssetPreview(asset), size);
        public static void TextureField(GUIContent content, Vector2? size = null)
            => TextureField(content.image, size);

        #endregion

        #region DateTime Fields

        public static DateTime DateTimeField(GUIContent label, DateTime dateTime, bool year, bool month, bool day, bool hour = false, bool minute = false, bool second = false, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal(options);

            if (label != null)
            {
                float labelWidth = EditorGUIUtility.labelWidth;
                EditorGUILayout.LabelField(label, GUILayout.Width(labelWidth));
            }

            int YY = 2000;
            int MM = 1;
            int DD = 1;
            int hh = 0;
            int mm = 0;
            int ss = 0;

            const float SMALL_SPACE = 10f;
            const float LARGE_SPACE = 20f;

            if (year)
            {
                YY = EditorGUILayout.IntField(dateTime.Year, GUILayout.Width(50), GUILayout.ExpandWidth(true));
                EditorGUILayout.LabelField("-", GUILayout.MaxWidth(SMALL_SPACE));
            }

            if (month)
            {
                MM = EditorGUILayout.IntField(dateTime.Month, GUILayout.Width(30), GUILayout.ExpandWidth(true));
                EditorGUILayout.LabelField("-", GUILayout.MaxWidth(SMALL_SPACE));
            }

            if (day)
            {
                DD = EditorGUILayout.IntField(dateTime.Day, GUILayout.MaxWidth(30), GUILayout.ExpandWidth(true));
            }

            if (hour)
            {
                GUILayout.Space(LARGE_SPACE);
                hh = EditorGUILayout.IntField(dateTime.Hour, GUILayout.Width(30), GUILayout.ExpandWidth(true));
                EditorGUILayout.LabelField(":", GUILayout.MaxWidth(SMALL_SPACE));
            }

            if (minute)
            {
                mm = EditorGUILayout.IntField(dateTime.Minute, GUILayout.Width(30), GUILayout.ExpandWidth(true));
                EditorGUILayout.LabelField(":", GUILayout.MaxWidth(SMALL_SPACE));
            }

            if (second)
            {
                ss = EditorGUILayout.IntField(dateTime.Second, GUILayout.Width(30), GUILayout.ExpandWidth(true));
            }

            EditorGUILayout.EndHorizontal();
            return new DateTime(YY, MM, DD, hh, mm, ss);
        }
        
        public static DateTime DateTimeField(string label, DateTime dateTime, bool year, bool month, bool day, bool hour = false, bool minute = false, bool second = false, params GUILayoutOption[] options)
            => DateTimeField(new GUIContent(label), dateTime, year, month, day, hour, minute, second, options);
        public static DateTime DateTimeField(DateTime dateTime, bool year, bool month, bool day, bool hour = false, bool minute = false, bool second = false, params GUILayoutOption[] options)
            => DateTimeField(GUIContent.none, dateTime, year, month, day, hour, minute, second, options);
        public static UnixTime UnixTimeField(string label, UnixTime unixTime, bool year, bool month, bool day, bool hour = false, bool minute = false, bool second = false, params GUILayoutOption[] options)
            => UnixTimeField(new GUIContent(label), unixTime, year, month, day, hour, minute, second, options);
        public static UnixTime UnixTimeField(GUIContent label, UnixTime unixTime, bool year, bool month, bool day, bool hour = false, bool minute = false, bool second = false, params GUILayoutOption[] options)
        {
            DateTime dateTime = unixTime.ToDateTime();
            return new UnixTime(DateTimeField(label, dateTime, year, month, day, hour, minute, second, options));
        }
        
        #endregion

        #region Button        
        public static bool Button(GUIContent content, params GUILayoutOption[] options)
            => Button(content, 12, GUIColor.None, false, options);
        public static bool Button(GUIContent content, int fontSize = 12, params GUILayoutOption[] options)
            => Button(content, fontSize, GUIColor.None, false, options);
        public static bool Button(GUIContent content, GUIColor color, params GUILayoutOption[] options)
            => Button(content, 12, color, false, options);
        public static bool Button(GUIContent content, GUIColor color, bool isSelected, params GUILayoutOption[] options)
            => Button(content, 12, color, isSelected, options);
        public static bool Button(GUIContent content, int fontSize, GUIColor color = GUIColor.None, bool isSelected = false, params GUILayoutOption[] options)
            => GUILayout.Button(content, EGUIStyles.Button(TextAnchor.MiddleCenter, fontSize, color, isSelected), options);
        public static bool Button(GUIContent content, TextAnchor alignment = TextAnchor.MiddleCenter, int fontSize = 12, params GUILayoutOption[] options)
            => GUILayout.Button(content, EGUIStyles.Button(alignment, fontSize, GUIColor.None, false), options);
        public static bool Button(string label, params GUILayoutOption[] options)
            => Button(label, 12, GUIColor.None, false, options);
        public static bool Button(string label, GUIColor color, params GUILayoutOption[] options)
            => Button(label, 12, color, false, options);
        public static bool Button(string label, int fontSize = 12, params GUILayoutOption[] options)
            => Button(label, fontSize, GUIColor.None, false, options);
        public static bool Button(string label, GUIColor color, bool isSelected, params GUILayoutOption[] options)
            => Button(label, 12, color, isSelected, options);
        public static bool Button(string label, int fontSize = 12, GUIColor color = GUIColor.None, bool isSelected = false, params GUILayoutOption[] options)
            => GUILayout.Button(label, EGUIStyles.Button(TextAnchor.MiddleCenter, fontSize, color, isSelected), options);
        public static bool Button(Texture2D label, params GUILayoutOption[] options)
            => Button(label, GUIColor.None, false, options);
        public static bool Button(Texture2D label, GUIColor color, params GUILayoutOption[] options)
            => Button(label, color, false, options);
        public static bool Button(Texture2D label, bool isSelected, params GUILayoutOption[] options)
            => Button(label, GUIColor.None, isSelected, options);
        public static bool Button(Texture2D label, GUIColor color = GUIColor.None, bool isSelected = false, params GUILayoutOption[] options)
            => GUILayout.Button(label, EGUIStyles.Button(TextAnchor.MiddleCenter, 12, color, isSelected), options);

        #endregion

        #region ExColorPicker
        public static Color ExColorPicker(GUIContent label, Color selectedColor)
        {
            Color defaultColor = GUI.backgroundColor;

            GUIStyle style = new();
            style.padding = new RectOffset(0, 0, 0, 0);
            style.margin = new RectOffset(0, 0, 0, 0);
            style.border = new RectOffset(0, 0, 0, 0);
            style.fixedWidth = 20;
            style.fixedHeight = 20;

            GUILayout.BeginHorizontal();

            //label
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            List<Color> colorList = new();
            // TODO: Add ExColors

            foreach (Color color in colorList)
            {
                // Change the background texture for the selected color entry
                style.normal.background = color == selectedColor ? EditorGUITextures.ToolBarButtonOn : EditorGUITextures.ToolBarButtonOff;
                GUI.backgroundColor = color;

                if (GUILayout.Button("", style))
                {
                    selectedColor = color;
                }
            }
            GUILayout.EndHorizontal();

            GUI.backgroundColor = defaultColor;
            return selectedColor;
        }
        public static Color ExColorPicker(string label, Color selectedColor)
            => ExColorPicker(label, selectedColor);
        #endregion

        #region ListDropdownField
        private static T GenericDropdownField<T>(T currentValue, IList<T> list, GUIContent label = null, params GUILayoutOption[] options)
        {
            if (list == null || list.Count == 0)
            {
                EditorGUILayout.HelpBox("No list found.", MessageType.None);
                return default;
            }

            int index = list.IndexOf(currentValue);
            List<string> stringArray = list.Select(enumValue => enumValue.ToString()).ToList();

            index = EditorGUILayout.Popup(label, index, stringArray.ToArray(), options);
            if (index < 0) index = 0;
            return list[index];
        }


        public static string StringListDropdown(string currentValue, List<string> list, GUIContent label = null, params GUILayoutOption[] options)
            => GenericDropdownField(currentValue, list, label, options);

        public static string StringListDropdown(string currentValue, string[] array, GUIContent label = null, params GUILayoutOption[] options)
            => GenericDropdownField(currentValue, array, label, options);


        #endregion

        #region ListToolbarField
        public static int StringListToolbar(int currentId, List<string> list, GUIContent label = null, int maxColumns = 3, params GUILayoutOption[] options)
        {
            if (list == null || list.Count == 0)
            {
                EditorGUILayout.HelpBox("No list found.", MessageType.Warning);
                return -1;
            }

            int index = currentId;

            float totalWidth = 0;
            GUILayout.BeginHorizontal();

            if (label != null)
            {
                GUILayout.Label(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            }

            int columnCount = 0;  // New variable to track the number of columns

            for (int i = 0; i < list.Count; i++)
            {
                float buttonWidth = EditorStyles.toolbarButton.CalcSize(new GUIContent(list[i])).x;
                if (totalWidth + buttonWidth > EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth || columnCount >= maxColumns)
                {
                    // Wrap to the next line if the button will exceed the width of the inspector or if the maxColumns limit is reached.
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();

                    if (label != null)
                    {
                        // Empty space
                        GUILayout.Space(EditorGUIUtility.labelWidth);
                    }

                    totalWidth = 0;
                    columnCount = 0;  // Reset column count for the new line
                }

                // Use toggle style buttons for the toolbar buttons
                bool isActive = GUILayout.Toggle(index == i, list[i], EditorStyles.miniButton, options);
                if (isActive) index = i;

                totalWidth += buttonWidth;
                columnCount++;  // Increment column count
            }
            GUILayout.EndHorizontal();

            return index;
        }


        #endregion

        #region Fields with Default Value Resetting Button (int, float, string, enum)

        private const int DEFAULT_FIELDS_BTN_WIDTH = 112;
        private const string DEFAULT_FIELDS_BTN_NAME = "Reset to Default";


        public static int IntFieldWithDefault(string label, int value, int defaultValue)
        {
            return IntFieldWithDefault(new GUIContent(label), value, defaultValue);
        }

        public static int IntFieldWithDefault(GUIContent label, int value, int defaultValue)
        {
            GUILayout.BeginHorizontal();
            //label += $" (Default:{defaultValue})";
            int newValue = EditorGUILayout.IntField(label, value);
            if (GUILayout.Button(DEFAULT_FIELDS_BTN_NAME, GUILayout.Width(DEFAULT_FIELDS_BTN_WIDTH)))
            {
                newValue = defaultValue;
            }

            GUILayout.EndHorizontal();
            return newValue;
        }


        public static float FloatFieldWithDefault(string label, float value, float defaultValue)
        {
            return FloatFieldWithDefault(new GUIContent(label), value, defaultValue);
        }

        public static float FloatFieldWithDefault(GUIContent label, float value, float defaultValue)
        {
            GUILayout.BeginHorizontal();
            //label += $" (Default:{defaultValue})";
            float newValue = EditorGUILayout.FloatField(label, value);
            if (GUILayout.Button(DEFAULT_FIELDS_BTN_NAME, GUILayout.Width(DEFAULT_FIELDS_BTN_WIDTH)))
            {
                newValue = defaultValue;
            }

            GUILayout.EndHorizontal();
            return newValue;
        }

        public static string StringFieldWithDefault(string label, string value, string defaultValue)
        {
            return StringFieldWithDefault(new GUIContent(label), value, defaultValue);
        }

        public static string StringFieldWithDefault(GUIContent label, string value, string defaultValue)
        {
            GUILayout.BeginHorizontal();
            //label += $" (Default:{defaultValue})";
            string newValue = EditorGUILayout.TextField(label, value);
            if (GUILayout.Button(DEFAULT_FIELDS_BTN_NAME, GUILayout.Width(DEFAULT_FIELDS_BTN_WIDTH)))
            {
                newValue = defaultValue;
            }

            GUILayout.EndHorizontal();
            return newValue;
        }

        private const float PERSISTENT_DATA_PATH_FIELD_WIDTH = 126;
        private const float ASSET_PATH_FIELD_WIDTH = 46;

        public static string PersistentDataPathFieldWithDefault(string label, string value, string defaultValue)
        {
            return PersistentDataPathFieldWithDefault(new GUIContent(label), value, defaultValue);
        }

        public static string PersistentDataPathFieldWithDefault(GUIContent label, string value, string defaultValue)
        {
            return DataPathFieldWithDefault("Persistent Data Path/", label, value, defaultValue, PERSISTENT_DATA_PATH_FIELD_WIDTH);
        }

        public static string AssetPathFieldWithDefault(string label, string value, string defaultValue)
        {
            return AssetPathFieldWithDefault(new GUIContent(label), value, defaultValue);
        }

        public static string AssetPathFieldWithDefault(GUIContent label, string value, string defaultValue)
        {
            return DataPathFieldWithDefault("Assets/", label, value, defaultValue, ASSET_PATH_FIELD_WIDTH);
        }

        private static string DataPathFieldWithDefault(string pathName, GUIContent label, string value, string defaultValue, float fieldWidth)
        {
            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            EditorGUILayout.LabelField(pathName, GUILayout.Width(fieldWidth));

            string newValue = EditorGUILayout.TextField(value, GUILayout.MinWidth(20));
            if (GUILayout.Button(DEFAULT_FIELDS_BTN_NAME, GUILayout.Width(DEFAULT_FIELDS_BTN_WIDTH)))
            {
                newValue = defaultValue;
            }

            GUILayout.EndHorizontal();
            return newValue;
        }

        public static T EnumPopupWithDefault<T>(string label, T value, T defaultValue) where T : Enum
        {
            return EnumPopupWithDefault(new GUIContent(label), value, defaultValue);
        }

        public static T EnumPopupWithDefault<T>(GUIContent label, T value, T defaultValue) where T : Enum
        {
            GUILayout.BeginHorizontal();

            string[] displayNames = EnumUtils.GetDisplayNames(typeof(T));
            int enumIndex = Convert.ToInt32(value);
            int newEnumIndex = EditorGUILayout.Popup(label, enumIndex, displayNames);
            T newValue = (T)Enum.ToObject(typeof(T), newEnumIndex);

            if (GUILayout.Button(DEFAULT_FIELDS_BTN_NAME, GUILayout.Width(DEFAULT_FIELDS_BTN_WIDTH)))
            {
                newValue = defaultValue;
            }

            GUILayout.EndHorizontal();
            return newValue;
        }

        public static T EnumPopup<T>(T value, params GUILayoutOption[] options) where T : Enum
        {
            return EnumPopup(GUIContent.none, value, options);
        }


        public static T EnumPopup<T>(string label, T value, params GUILayoutOption[] options) where T : Enum
        {
            return EnumPopup(new GUIContent(label), value, options);
        }

        public static T EnumPopup<T>(GUIContent label, T value, params GUILayoutOption[] options) where T : Enum
        {
            GUILayout.BeginHorizontal();

            string[] displayNames = EnumUtils.GetDisplayNames(typeof(T));
            int enumIndex = Convert.ToInt32(value);
            int newEnumIndex = EditorGUILayout.Popup(label, enumIndex, displayNames, options);
            T newValue = (T)Enum.ToObject(typeof(T), newEnumIndex);

            GUILayout.EndHorizontal();
            return newValue;
        }

        public static void IntFieldWithDefault(string label, SerializedProperty property, int defaultValue)
        {
            IntFieldWithDefault(new GUIContent(label), property, defaultValue);
        }

        public static void IntFieldWithDefault(GUIContent label, SerializedProperty property, int defaultValue)
        {
            if (property == null || property.propertyType != SerializedPropertyType.Integer)
            {
                Debug.LogError("SerializedProperty must be of integer type.");
                return;
            }

            GUILayout.BeginHorizontal();
            int newValue = EditorGUILayout.IntField(label, property.intValue);
            if (GUILayout.Button(DEFAULT_FIELDS_BTN_NAME, GUILayout.Width(DEFAULT_FIELDS_BTN_WIDTH)))
            {
                newValue = defaultValue;
            }
            if (newValue != property.intValue)
            {
                property.intValue = newValue;
            }
            GUILayout.EndHorizontal();
        }

        public static void FloatFieldWithDefault(string label, SerializedProperty property, float defaultValue)
        {
            FloatFieldWithDefault(new GUIContent(label), property, defaultValue);
        }

        public static void FloatFieldWithDefault(GUIContent label, SerializedProperty property, float defaultValue)
        {
            if (property == null || property.propertyType != SerializedPropertyType.Float)
            {
                Debug.LogError("SerializedProperty must be of float type.");
                return;
            }

            GUILayout.BeginHorizontal();
            float newValue = EditorGUILayout.FloatField(label, property.floatValue);
            if (GUILayout.Button(DEFAULT_FIELDS_BTN_NAME, GUILayout.Width(DEFAULT_FIELDS_BTN_WIDTH)))
            {
                newValue = defaultValue;
            }
            if (newValue != property.floatValue)
            {
                property.floatValue = newValue;
            }
            GUILayout.EndHorizontal();
        }

        public static void StringFieldWithDefault(string label, SerializedProperty property, string defaultValue)
        {
            StringFieldWithDefault(new GUIContent(label), property, defaultValue);
        }

        public static void StringFieldWithDefault(GUIContent label, SerializedProperty property, string defaultValue)
        {
            if (property == null || property.propertyType != SerializedPropertyType.String)
            {
                Debug.LogError("SerializedProperty must be of string type.");
                return;
            }

            GUILayout.BeginHorizontal();
            string newValue = EditorGUILayout.TextField(label, property.stringValue);
            if (GUILayout.Button(DEFAULT_FIELDS_BTN_NAME, GUILayout.Width(DEFAULT_FIELDS_BTN_WIDTH)))
            {
                newValue = defaultValue;
            }
            if (newValue != property.stringValue)
            {
                property.stringValue = newValue;
            }
            GUILayout.EndHorizontal();
        }

        public static void EnumPopupWithDefault<T>(string label, SerializedProperty property, T defaultValue) where T : Enum
        {
            EnumPopupWithDefault(new GUIContent(label), property, defaultValue);
        }

        public static void EnumPopupWithDefault<T>(GUIContent label, SerializedProperty property, T defaultValue) where T : Enum
        {
            if (property == null || property.propertyType != SerializedPropertyType.Enum)
            {
                Debug.LogError("SerializedProperty must be of enum type.");
                return;
            }

            GUILayout.BeginHorizontal();

            string[] displayNames = property.enumDisplayNames;
            int enumIndex = property.enumValueIndex;
            int newEnumIndex = EditorGUILayout.Popup(label, enumIndex, displayNames);

            if (newEnumIndex != enumIndex)
            {
                property.enumValueIndex = newEnumIndex;
            }

            if (GUILayout.Button(DEFAULT_FIELDS_BTN_NAME, GUILayout.Width(DEFAULT_FIELDS_BTN_WIDTH)))
            {
                property.enumValueIndex = Convert.ToInt32(defaultValue);
            }

            GUILayout.EndHorizontal();
        }

        #endregion

        #region Non-Editable Field

        public static void NonEditableField(string label, string value)
        {
            NonEditableField(new GUIContent(label), value);
        }

        public static void NonEditableField(GUIContent label, string value)
        {
            GUILayout.BeginHorizontal(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth), GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.SelectableLabel(value, EGUIStyles.disabledTextField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            GUILayout.EndHorizontal();
        }

        #endregion

        #region Reverse Bool Field

        public static bool ReverseBoolField(string label, SerializedProperty p)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(p, GUIContent.none, GUILayout.Width(10));
            label ??= p.displayName;
            GUILayout.Label(new GUIContent(label), GUILayout.MinWidth(10));
            GUILayout.EndHorizontal();
            return p.boolValue;
        }

        public static bool ReverseBoolField(SerializedProperty p)
        {
            return ReverseBoolField(null, p);
        }

        #endregion

        #region Foldout

        public static void Foldout(string label, Action callback)
        {
            int space = 3;
            EGUIUtility.DrawHorizontalLine(1);
            GUILayout.Space(space);
            bool b = EditorPrefs.GetBool(label, true);
            b = EditorGUILayout.Foldout(b, label, EGUIStyles.foldout);
            EditorPrefs.SetBool(label, b);

            if (b)
            {
                GUILayout.Space(space);
                EGUIUtility.DrawHorizontalLine(1);

                GUIStyle style = new();
                style.margin = new RectOffset(0, 0, 10, 10);

                GUILayout.BeginVertical(style);
                callback?.Invoke();
                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.Space(space);
                EGUIUtility.DrawHorizontalLine(1);
            }

            GUILayout.Space(-space);
        }

        #endregion

        #region Multi Buttons

        private const float MULTI_BUTTON_OFFSET = 0.5f;
        public static void TrueOrFalseButton(string label, Action<bool> action)
        {
            BoolMultiButton(label, action, "True", "False");
        }

        public static void SetOrUnsetButton(string label, Action<bool> action)
        {
            BoolMultiButton(label, action, "Set", "Unset");
        }

        public static void BoolMultiButton(string label, Action<bool> action, string yes, string no)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(label);

                float buttonWidth = (EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth) / 2;
                buttonWidth -= MULTI_BUTTON_OFFSET * 2;

                if (GUILayout.Button(yes, GUILayout.Width(buttonWidth)))
                {
                    action(true);
                }
                if (GUILayout.Button(no, GUILayout.Width(buttonWidth)))
                {
                    action(false);
                }
            }
            GUILayout.EndHorizontal();
        }

        public static void MultiButton(string label, params EGUIButtonEntry[] buttonEntries)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(label);

                float buttonWidth = (EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth) / buttonEntries.Length;
                buttonWidth -= MULTI_BUTTON_OFFSET * buttonEntries.Length;

                foreach (EGUIButtonEntry entry in buttonEntries)
                {
                    if (GUILayout.Button(entry.label, GUILayout.Width(buttonWidth)))
                    {
                        entry.action?.Invoke();
                    }
                }
            }
            GUILayout.EndHorizontal();
        }

        #endregion

        #region File Path

        public static string FilePathField(string label, string path, string extension, string defaultPath)
        {
            return FilePathField(new GUIContent(label), path, extension, defaultPath);
        }
        
        public static string FilePathField(GUIContent label, string path, string extension, string defaultPath)
        {
            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
                path = EditorGUILayout.TextField(path);
                if (GUILayout.Button("...", GUILayout.Width(30)))
                {
                    string directory = Path.GetDirectoryName(path);
                    string fileName = Path.GetFileName(path);
                    string newFilePath = EditorUtility.OpenFilePanel("Select File", directory, extension);
                    if (!string.IsNullOrEmpty(newFilePath))
                    {
                        path = newFilePath;
                    }
                }
            }
            GUILayout.EndHorizontal();
            return path;
        }

        public static string DirectoryPathField(string label, string path, string defaultPath)
        {
            return DirectoryPathField(new GUIContent(label), path, defaultPath);
        }

        public static string DirectoryPathField(GUIContent label, string path, string defaultPath)
        {
            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
                path = GUILayout.TextField(path);
                GUILayout.Space(-5);
                if (GUILayout.Button("...", GUILayout.Width(30)))
                {
                    string newDirectoryPath = EditorUtility.OpenFolderPanel("Select Directory", path, "");
                    if (!string.IsNullOrEmpty(newDirectoryPath))
                    {
                        path = newDirectoryPath;
                    }
                }
            }
            GUILayout.EndHorizontal();
            return path;
        }

        #endregion

        #region [Deprecated] Horizontal/VerticalLayout
        public static void HorizontalLayout(Action action, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(options);
            action();
            GUILayout.EndHorizontal();
        }

        public static void VerticalLayout(Action action, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(options);
            action();
            GUILayout.EndVertical();
        }

        public static void HorizontalLayout(GUIStyle style, Action action, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(style, options);
            action();
            GUILayout.EndHorizontal();
        }

        public static void VerticalLayout(GUIStyle style, Action action, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(style, options);
            action();
            GUILayout.EndVertical();
        }
        #endregion
    }
}

