using System;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExEditor
{
    public static class ExGUIExtensions
    {
        public static Rect GetSingleLightHeightRow(this Rect rect, int row)
        {
            if (row == 0)
            {
                Debug.LogError("Row index must be greater than 0");
                return rect;
            }
            rect.height = EditorGUIUtility.singleLineHeight;
            rect.y += EditorGUIUtility.singleLineHeight * row;
            return rect;
        }

        public static Rect GetLabelRect(this Rect rowRect, float labelWidth = -1)
        {
            if (Math.Abs(labelWidth - (-1)) < ExGUI.TOLERANCE) labelWidth = EditorGUIUtility.labelWidth;
            rowRect.width = labelWidth;
            return rowRect;
        }

        public static Rect GetValueRect(this Rect rowRect, float labelWidth = -1)
        {
            if (Math.Abs(labelWidth - (-1)) < ExGUI.TOLERANCE) labelWidth = EditorGUIUtility.labelWidth;
            rowRect.x += labelWidth;
            rowRect.width -= labelWidth;
            return rowRect;
        }
    }
}