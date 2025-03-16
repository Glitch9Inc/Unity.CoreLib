using System;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    public static class RectExtensions
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

        public static Rect[] SplitVertically(this Rect rect, int count, float space = 0)
        {
            Rect[] rects = new Rect[count];
            float height = rect.height / count;
            for (int i = 0; i < count; i++)
            {
                rects[i] = new Rect(rect.x, rect.y + height * i + space * i, rect.width, height - space);
            }
            return rects;
        }

        public static Rect[] SplitHorizontally(this Rect rect, int count, float space = 0)
        {
            Rect[] rects = new Rect[count];
            float width = rect.width / count;
            for (int i = 0; i < count; i++)
            {
                rects[i] = new Rect(rect.x + width * i + space * i, rect.y, width - space, rect.height);
            }
            return rects;
        }

        public static Rect[] SplitHorizontally(this Rect rect, float space, params float[] weights)
        {
            Rect[] rects = new Rect[weights.Length];

            for (int i = 0; i < weights.Length; i++)
            {
                float width = rect.width * weights[i];
                float x = i == 0 ? rect.x : rects[i - 1].x + rects[i - 1].width + space;
                rects[i] = new Rect(x, rect.y, width, rect.height);
            }
            
            return rects;
        }

        public static Rect GetLabelRect(this Rect rowRect, float labelWidth = -1)
        {
            if (Math.Abs(labelWidth - (-1)) < EGUI.TOLERANCE) labelWidth = EditorGUIUtility.labelWidth;
            rowRect.width = labelWidth;
            return rowRect;
        }

        public static Rect GetValueRect(this Rect rowRect, float labelWidth = -1)
        {
            if (Math.Abs(labelWidth - (-1)) < EGUI.TOLERANCE)
            {
                labelWidth = EditorGUIUtility.labelWidth;
                labelWidth -= EditorGUI.indentLevel * 15;
            }
            rowRect.x += labelWidth;
            rowRect.width -= labelWidth;
            return rowRect;
        }
    }
}