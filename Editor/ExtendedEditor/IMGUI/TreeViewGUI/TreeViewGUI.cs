using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public class TreeViewGUI
    {
        private static class Strings
        {
            internal const string UNKNOWN_TIME = "Unknown";
            internal const string UNIX_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        }

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
            string timeString = unixTime == null ? Strings.UNKNOWN_TIME : unixTime.Value.ToString(Strings.UNIX_TIME_FORMAT);
            StringCell(cellRect, timeString, style);
        }

        public static void BoolCell(Rect cellRect, bool value)
        {
            GUI.Label(cellRect, value ? "Yes" : "No");
        }

        public static bool CheckboxCell(Rect cellRect, bool value)
        {
            Rect toggleRect = cellRect;
            toggleRect.width = 20f;

            Rect labelRect = cellRect;
            labelRect.x += toggleRect.width;
            labelRect.width -= toggleRect.width;

            value = GUI.Toggle(toggleRect, value, "");
            GUI.Label(labelRect, value ? "Yes" : "No");

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
    }
}