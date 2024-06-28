using Glitch9.UI;
using System;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public class TreeViewEGUI
    {
        private static class Strings
        {
            internal const string UNKNOWN_TIME = "-";
            internal const string UNIX_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
            internal const string UNIX_DATE_FORMAT = "yyyy-MM-dd";
            internal const string BOOL_TRUE = "Yes";
            internal const string BOOL_FALSE = "No";
        }

        #region Cell GUIs

        public static void StringCell(Rect cellRect, string text, GUIStyle style)
        {
            StringCell(cellRect, text, null, style);
        }

        public static void StringCell(Rect cellRect, string text, string defaultText = null, GUIStyle style = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                if (defaultText != null)
                {
                    //EGUI.Label(cellRect, text, ExColor.charcoal);
                    text = defaultText;
                }
                else return;
            }
            if (style == null) GUI.Label(cellRect, text);
            else GUI.Label(cellRect, text, style);
        }

        public static void UnixTimeCell(Rect cellRect, UnixTime? unixTime, GUIStyle style = null)
        {
            string timeString;
            if (unixTime == null || unixTime.Value < new UnixTime(1980, 1, 1, 1, 1, 1)) timeString = Strings.UNKNOWN_TIME;
            else timeString = unixTime.Value.ToString(Strings.UNIX_TIME_FORMAT);
            StringCell(cellRect, timeString, style);
        }

        public static void UnixDateCell(Rect cellRect, UnixTime? unixTime, GUIStyle style = null)
        {
            string timeString;
            if (unixTime == null || unixTime.Value < new UnixTime(1980, 1, 1, 1, 1, 1)) timeString = Strings.UNKNOWN_TIME;
            else timeString = unixTime.Value.ToString(Strings.UNIX_DATE_FORMAT);
            StringCell(cellRect, timeString, style);
        }

        public static void IconCell(Rect cellRect, Texture2D icon)
        {
            if (icon == null) return;
            GUI.Label(cellRect, icon, EditorStyles.centeredGreyMiniLabel);
        }

        public static void BoolCell(Rect cellRect, bool value)
        {
            GUI.Label(cellRect, value ? Strings.BOOL_TRUE : Strings.BOOL_FALSE);
        }

        public static bool CheckboxCell(Rect cellRect, bool value)
        {
            Rect toggleRect = cellRect;
            toggleRect.width = 20f;

            Rect labelRect = cellRect;
            labelRect.x += toggleRect.width;
            labelRect.width -= toggleRect.width;

            value = GUI.Toggle(toggleRect, value, "");
            GUI.Label(labelRect, value ? Strings.BOOL_TRUE : Strings.BOOL_FALSE);

            return value;
        }

        public static void PriceCell(Rect cellRect, float displayValue, string per)
        {
            if (displayValue == 0) return;

            Rect priceRect = cellRect;
            priceRect.width = 50f;

            Rect perRect = cellRect;
            perRect.width = cellRect.width - priceRect.width;
            perRect.x += priceRect.width;

            GUI.Label(priceRect, $"${displayValue}");
            GUI.Label(perRect, $"per {per}");
        }

        public static string DelayedTextCell(Rect cellRect, string value, GUIColor fieldColor)
        {
            return EditorGUI.DelayedTextField(cellRect, value, TreeViewStyles.TextField(12, fieldColor));
        }

        public static int DelayedIntCell(Rect cellRect, int value, int min, int max, GUIColor fieldColor)
        {
            int newValue = EditorGUI.DelayedIntField(cellRect, value, TreeViewStyles.TextField(12, fieldColor));
            return Mathf.Clamp(newValue, min, max);
        }

        public static TEnum EnumPopupCell<TEnum>(Rect cellRect, TEnum color) where TEnum : Enum
        {
            cellRect = EGUIUtility.AdjustTreeViewRect(cellRect);
            return EGUI.ResizableEnumPopup(cellRect, color);
        }

        private static readonly GUIStyle k_ToolbarDropDown = new(EditorStyles.toolbarDropDown)
        {
            padding = new RectOffset(6, 6, 2, 2),
            fontSize = 11,
            normal = { textColor = ExColor.charcoal },
        };

        public static int IntPopupMenu( string prefix, int selected, int[] optionValues, params GUILayoutOption[] options)
        {
            string[] optionNames = new string[optionValues.Length];
            for (int i = 0; i < optionValues.Length; i++)
            {
                optionNames[i] = $"{prefix}{optionValues[i]}";
            }

            return EditorGUILayout.IntPopup(selected, optionNames, optionValues, k_ToolbarDropDown, options);
        }


        #endregion

        #region Edit Window GUIs

        public static void LeftSubtitle(string text)
        {
            EditorGUILayout.LabelField(text, TreeViewStyles.ChildWindowSubtitleLeft);
        }

        public static void RightSubtitle(string text)
        {
            EditorGUILayout.LabelField(text, TreeViewStyles.ChildWindowSubtitleRight);
        }

        public static void Subtitle(string left, string right)
        {
            GUILayout.BeginHorizontal();
            {
                LeftSubtitle(left);
                RightSubtitle(right);
            }
            GUILayout.EndHorizontal();
        }

        #endregion
    }
}