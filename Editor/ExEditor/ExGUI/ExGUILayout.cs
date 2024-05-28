using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using MessageType = UnityEditor.MessageType;
using Object = UnityEngine.Object;

namespace Glitch9.ExEditor
{
    public partial class ExGUILayout
    {
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

        #region Toggle
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

        #region ToolBarToggle
        public static bool ToolBarMid(GUIContent content, bool isOn, params GUILayoutOption[] options)
            => GUILayout.Toggle(isOn, content, EditorStyles.miniButtonMid, options);
        public static bool ToolBarMid(string label, bool isOn, params GUILayoutOption[] options)
            => ToolBarMid(new GUIContent(label), isOn, options);
        public static bool ToolBarMid(Texture2D tex, bool isOn, params GUILayoutOption[] options)
            => ToolBarMid(new GUIContent(tex), isOn, options);
        public static bool ToolBarLeft(GUIContent content, bool isOn, params GUILayoutOption[] options)
            => GUILayout.Toggle(isOn, content, EditorStyles.miniButtonLeft, options);
        public static bool ToolBarLeft(string label, bool isOn, params GUILayoutOption[] options)
            => ToolBarLeft(new GUIContent(label), isOn, options);
        public static bool ToolBarLeft(Texture2D tex, bool isOn, params GUILayoutOption[] options)
            => ToolBarLeft(new GUIContent(tex), isOn, options);
        public static bool ToolBarRight(GUIContent content, bool isOn, params GUILayoutOption[] options)
            => GUILayout.Toggle(isOn, content, EditorStyles.miniButtonRight, options);
        public static bool ToolBarRight(string label, bool isOn, params GUILayoutOption[] options)
            => ToolBarRight(new GUIContent(label), isOn, options);
        public static bool ToolBarRight(Texture2D tex, bool isOn, params GUILayoutOption[] options)
            => ToolBarRight(new GUIContent(tex), isOn, options);
        #endregion

        #region BoxedLabel
        public static void BoxedLabel(GUIContent label, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Box(TextAnchor.MiddleCenter), options);
        public static void BoxedLabel(GUIContent label, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Box(TextAnchor.MiddleCenter, color), options);
        public static void BoxedLabel(GUIContent label, int fontSize, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Box(TextAnchor.MiddleCenter, fontSize, color), options);
        public static void BoxedLabel(GUIContent label, TextAnchor alignment, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Box(alignment, color), options);
        public static void BoxedLabel(GUIContent label, TextAnchor alignment = TextAnchor.MiddleCenter, int fontSize = 12, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Box(alignment, fontSize, color), options);

        public static void BoxedLabel(string label, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Box(TextAnchor.MiddleCenter), options);
        public static void BoxedLabel(string label, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Box(TextAnchor.MiddleCenter, color), options);
        public static void BoxedLabel(string label, int fontSize, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Box(TextAnchor.MiddleCenter, fontSize), options);
        public static void BoxedLabel(string label, int fontSize, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Box(TextAnchor.MiddleCenter, fontSize, color), options);
        public static void BoxedLabel(string label, int fontSize, TextAnchor alignment, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Box(alignment, fontSize, GUIColor.None), options);
        public static void BoxedLabel(string label, TextAnchor alignment, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Box(alignment, color), options);
        public static void BoxedLabel(string label, TextAnchor alignment = TextAnchor.MiddleCenter, int fontSize = 12, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Box(alignment, fontSize, color), options);
        #endregion

        #region TableBox
        public static void TableBox(string label, int fontSize = 12, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.TableBox(fontSize, color), options);
        public static void TableBox(string label, int fontSize = 12, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.TableBox(fontSize, GUIColor.None), options);
        #endregion

        #region Label
        public static void Label(GUIContent label, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(TextAnchor.UpperLeft), options);
        public static void Label(GUIContent label, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(TextAnchor.UpperLeft, 12, color), options);
        public static void Label(GUIContent label, int fontSize, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(TextAnchor.UpperLeft, fontSize, color), options);
        public static void Label(GUIContent label, int fontSize, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(TextAnchor.UpperLeft, fontSize), options);
        public static void Label(GUIContent label, TextAnchor alignment, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(alignment, 12, color), options);
        public static void Label(GUIContent label, TextAnchor alignment = TextAnchor.UpperLeft, int fontSize = 12, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(alignment, fontSize, GUIColor.None), options);
        public static void Label(GUIContent label, TextAnchor alignment = TextAnchor.UpperLeft, int fontSize = 12, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(alignment, fontSize, color), options);

        public static void Label(string label, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(TextAnchor.UpperLeft), options);
        public static void Label(string label, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(TextAnchor.UpperLeft, 12, color), options);
        public static void Label(string label, int fontSize, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(TextAnchor.UpperLeft, fontSize, color), options);
        public static void Label(string label, int fontSize, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(TextAnchor.UpperLeft, fontSize), options);
        public static void Label(string label, TextAnchor alignment, GUIColor color, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(alignment, 12, color), options);
        public static void Label(string label, TextAnchor alignment = TextAnchor.UpperLeft, int fontSize = 12, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(alignment, fontSize, GUIColor.None), options);
        public static void Label(string label, TextAnchor alignment = TextAnchor.UpperLeft, int fontSize = 12, GUIColor color = GUIColor.None, params GUILayoutOption[] options)
            => EditorGUILayout.LabelField(label, ExEditorStyles.Label(alignment, fontSize, color), options);
        #endregion

        #region InfoLabel

        public static void InfoField(string label, string info, float labelWidth = -1f, int infoFontSize = 12, bool boldLabel = false, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(options);
            {
                if (Math.Abs(labelWidth - (-1f)) < ExGUI.TOLERANCE) labelWidth = EditorGUIUtility.labelWidth;

                // label 스타일 생성
                GUIStyle labelStyle;
                GUIStyle infoStyle = new(EditorStyles.label);

                infoStyle.alignment = TextAnchor.MiddleLeft;
                infoStyle.fontSize = infoFontSize;

                if (boldLabel)
                {
                    labelStyle = new GUIStyle(EditorStyles.boldLabel);
                    labelStyle.alignment = TextAnchor.MiddleLeft;
                }
                else
                {
                    labelStyle = infoStyle;
                }

                EditorGUILayout.LabelField(label, labelStyle, GUILayout.Width(labelWidth));
                EditorGUILayout.LabelField(info, infoStyle, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            }
            GUILayout.EndHorizontal();
        }


        #endregion

        #region DescriptionField
        public static string DescriptionField(string text, params GUILayoutOption[] options)
            => EditorGUILayout.TextArea(text, ExGUI.skin.textArea, options);
        public static string DescriptionField(SerializedProperty property, params GUILayoutOption[] options)
            => EditorGUILayout.TextArea(property.stringValue, ExGUI.skin.textArea, options);
        #endregion

        #region Horizontal/VerticalLayout
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

        #region TextureField
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

        #region DateTimeField       
        public static DateTime DateTimeField(string label, DateTime dateTime, bool year, bool month, bool day, bool hour = false, bool minute = false, bool second = false, params GUILayoutOption[] options)
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

            if (year)
            {
                YY = EditorGUILayout.IntField(dateTime.Year, GUILayout.Width(50));
                EditorGUILayout.LabelField("Year", GUILayout.Width(20));
            }

            if (month)
            {
                MM = EditorGUILayout.IntField(dateTime.Month, GUILayout.Width(30));
                EditorGUILayout.LabelField("Month", GUILayout.Width(20));
            }

            if (day)
            {
                DD = EditorGUILayout.IntField(dateTime.Day, GUILayout.Width(30));
                EditorGUILayout.LabelField("Day", GUILayout.Width(20));
            }

            if (hour)
            {
                hh = EditorGUILayout.IntField(dateTime.Hour, GUILayout.Width(30));
                EditorGUILayout.LabelField("Hour", GUILayout.Width(20));
            }

            if (minute)
            {
                mm = EditorGUILayout.IntField(dateTime.Minute, GUILayout.Width(30));
                EditorGUILayout.LabelField("Minute", GUILayout.Width(20));
            }

            if (second)
            {
                ss = EditorGUILayout.IntField(dateTime.Second, GUILayout.Width(30));
                EditorGUILayout.LabelField("Second", GUILayout.Width(20));
            }

            EditorGUILayout.EndHorizontal();
            return new DateTime(YY, MM, DD, hh, mm, ss);
        }
        public static DateTime DateTimeField(DateTime dateTime, bool year, bool month, bool day, bool hour = false, bool minute = false, bool second = false, params GUILayoutOption[] options)
            => DateTimeField(null, dateTime, year, month, day, hour, minute, second, options);
        public static UnixTime UnixTimeField(string label, UnixTime unixTime, bool year, bool month, bool day, bool hour = false, bool minute = false, bool second = false, params GUILayoutOption[] options)
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

            if (year)
            {
                YY = EditorGUILayout.IntField(unixTime.Year, GUILayout.Width(50));
                EditorGUILayout.LabelField("Year", GUILayout.Width(20));
            }

            if (month)
            {
                MM = EditorGUILayout.IntField(unixTime.Month, GUILayout.Width(30));
                EditorGUILayout.LabelField("Month", GUILayout.Width(20));
            }

            if (day)
            {
                DD = EditorGUILayout.IntField(unixTime.Day, GUILayout.Width(30));
                EditorGUILayout.LabelField("Day", GUILayout.Width(20));
            }

            if (hour)
            {
                hh = EditorGUILayout.IntField(unixTime.Hour, GUILayout.Width(30));
                EditorGUILayout.LabelField("Hour", GUILayout.Width(20));
            }

            if (minute)
            {
                mm = EditorGUILayout.IntField(unixTime.Minute, GUILayout.Width(30));
                EditorGUILayout.LabelField("Minute", GUILayout.Width(20));
            }

            if (second)
            {
                ss = EditorGUILayout.IntField(unixTime.Second, GUILayout.Width(30));
                EditorGUILayout.LabelField("Second", GUILayout.Width(20));
            }


            EditorGUILayout.EndHorizontal();
            return new UnixTime(YY, MM, DD, hh, mm, ss);
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
            => GUILayout.Button(content, ExEditorStyles.Button(TextAnchor.MiddleCenter, fontSize, color, isSelected), options);
        public static bool Button(GUIContent content, TextAnchor alignment = TextAnchor.MiddleCenter, int fontSize = 12, params GUILayoutOption[] options)
            => GUILayout.Button(content, ExEditorStyles.Button(alignment, fontSize, GUIColor.None, false), options);
        public static bool Button(string label, params GUILayoutOption[] options)
            => Button(label, 12, GUIColor.None, false, options);
        public static bool Button(string label, GUIColor color, params GUILayoutOption[] options)
            => Button(label, 12, color, false, options);
        public static bool Button(string label, int fontSize = 12, params GUILayoutOption[] options)
            => Button(label, fontSize, GUIColor.None, false, options);
        public static bool Button(string label, GUIColor color, bool isSelected, params GUILayoutOption[] options)
            => Button(label, 12, color, isSelected, options);
        public static bool Button(string label, int fontSize = 12, GUIColor color = GUIColor.None, bool isSelected = false, params GUILayoutOption[] options)
            => GUILayout.Button(label, ExEditorStyles.Button(TextAnchor.MiddleCenter, fontSize, color, isSelected), options);
        public static bool Button(Texture2D label, params GUILayoutOption[] options)
            => Button(label, GUIColor.None, false, options);
        public static bool Button(Texture2D label, GUIColor color, params GUILayoutOption[] options)
            => Button(label, color, false, options);
        public static bool Button(Texture2D label, bool isSelected, params GUILayoutOption[] options)
            => Button(label, GUIColor.None, isSelected, options);
        public static bool Button(Texture2D label, GUIColor color = GUIColor.None, bool isSelected = false, params GUILayoutOption[] options)
            => GUILayout.Button(label, ExEditorStyles.Button(TextAnchor.MiddleCenter, 12, color, isSelected), options);

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

            // can't use EnumPopup because it doesn't retrieve the ExEnum names
            // T newValue = (T)EditorGUILayout.EnumPopup(label, value);

            // #1
            //string[] names = Enum.GetNames(typeof(T));
            //string[] displayNames = ExEnum.GetNames(typeof(T));
            //int index = Array.IndexOf(displayNames, value.ToString());
            //int newIndex = EditorGUILayout.Popup(label, index, displayNames);
            //T newValue = (T)Enum.Parse(typeof(T), names[newIndex]);

            // #2
            string[] displayNames = EnumUtils.GetNames(typeof(T));
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

        #endregion

        public static TEnum ResizableEnumPopup<TEnum>(TEnum selected, GUIContent label, params GUILayoutOption[] options) where TEnum : Enum
        {
            string[] names = EnumUtils.GetNames(typeof(TEnum));
            int index = Array.IndexOf(names, selected.ToString());
            // The regular EnumPopup has a fixed height, so we need to use a custom implementation to make it resizable.
            // Make a button that shows the currently selected enum value, with a dropdown arrow to the right.
            if (EditorGUILayout.DropdownButton(label, FocusType.Passive, options))
            {
                // When the button is clicked, show a dropdown with all the enum values.
                GenericMenu menu = new();
                for (int i = 0; i < names.Length; i++)
                {
                    int localIndex = i;
                    menu.AddItem(new GUIContent(names[i]), i == index, () =>
                    {
                        index = localIndex;
                    });
                }
                menu.DropDown(new Rect(Event.current.mousePosition, Vector2.zero));
            }

            return (TEnum)Enum.Parse(typeof(TEnum), names[index]);
        }
    }
}

