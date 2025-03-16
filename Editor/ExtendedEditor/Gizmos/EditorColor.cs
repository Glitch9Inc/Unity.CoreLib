using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    public class EditorColor
    {
        private static readonly Dictionary<string, Color> _colors = new();

        private static Color GetColor(string key, Color defaultColor)
        {
            if (_colors.TryGetValue(key, out Color color)) return color;

            color = EditorGUIUtility.isProSkin ? defaultColor.GetComplementaryColor() : defaultColor;
            _colors.Add(key, color);
            return color;
        }

        public static Color textColor => GetColor(nameof(textColor), Color.black);
        public static Color gray => GetColor(nameof(gray), new Color(0.3f, 0.3f, 0.3f));
    }
}