using Glitch9.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public class TreeViewStyles
    {
        private static class EGUISkinStyles
        {
            internal const string TREEVIEW_ITEM = "treeviewitem";
            internal const string TREEVIEW_GROUP = "treeviewgroup";
        }

        private static readonly Dictionary<string, GUIStyle> _styles = new();
        private static GUIStyle Get(string key, GUIStyle defaultStyle)
        {
            if (!_styles.TryGetValue(key, out GUIStyle style))
            {
                style = new GUIStyle(defaultStyle);
                _styles[key] = style;
            }
            return style;
        }

        public static GUIStyle BottomBarStyle => Get(nameof(BottomBarStyle), new GUIStyle(EGUIStyles.Border(GUIBorder.Bottom))
        {
            fixedHeight = 34,
        });

        public static GUIStyle ChildWindowTitle => Get(nameof(ChildWindowTitle), new GUIStyle(GUI.skin.label)
        {
            fontStyle = FontStyle.Bold,
            fontSize = 16,
            fixedHeight = 20,
        });

        public static GUIStyle ChildWindowSubtitleLeft => Get(nameof(ChildWindowSubtitleLeft), new GUIStyle(GUI.skin.label)
        {
            fontSize = 11,
        });

        public static GUIStyle ChildWindowSubtitleRight => Get(nameof(ChildWindowSubtitleRight), new GUIStyle(GUI.skin.label)
        {
            fontSize = 11,
            alignment = TextAnchor.MiddleRight
        });

        public static GUIStyle EditWindowBody => Get(nameof(EditWindowBody), new GUIStyle()
        {
            padding = new RectOffset(5, 5, 10, 5)
        });

        public static GUIStyle WordWrapTextField => Get(nameof(WordWrapTextField), new GUIStyle(GUI.skin.textField)
        {
            wordWrap = true
        });

        internal static GUIStyle FindAndReplaceLayout => Get(nameof(FindAndReplaceLayout), new GUIStyle()
        {
            padding = new RectOffset(5, 5, 0, 5)
        });

        internal static GUIStyle TalkBubbleStyle => Get(nameof(TalkBubbleStyle), new GUIStyle()
        {
            normal =
            {
                textColor = ExColor.royalpurple,
                background = EditorGUITextures.Box(GUIColor.Purple),
                scaledBackgrounds = new Texture2D[] { EditorGUITextures.Box(GUIColor.Purple) }
            },
            margin = new RectOffset(5, 5, 0, 0),
            border = new RectOffset(5, 5, 5, 5),
            padding = new RectOffset(5, 5, 5, 5),
            fontSize = 11,
            wordWrap = true,
            stretchWidth = true,
            stretchHeight = true
        });

        public static GUIStyle TreeViewItem => Get(nameof(TreeViewItem), EGUI.skin.GetStyle(EGUISkinStyles.TREEVIEW_ITEM));
        public static GUIStyle TreeViewGroup => Get(nameof(TreeViewGroup), EGUI.skin.GetStyle(EGUISkinStyles.TREEVIEW_GROUP));

        public static GUIStyle TextField(int fontSize = 12, GUIColor color = GUIColor.None) => GetTextFieldStyle(fontSize, color, 3, 3);
        private static GUIStyle GetTextFieldStyle(int fontSize, GUIColor color, int leftOverflow, int rightOverflow)
        {
            string key = $"textfield_{fontSize}_{color}_{leftOverflow}_{rightOverflow}";
            Texture2D texDefault = EditorGUITextures.TextField(color);
            Texture2D texFocused = EditorGUITextures.TextField(color, true);
            if (texDefault == null || texFocused == null) Debug.LogError("TextField texture is null");

            return Get(key, new GUIStyle(GUI.skin.textArea)
            {
                border = new RectOffset(5, 5, 5, 5),
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(4, 4, 4, 4),
                overflow = new RectOffset(leftOverflow, rightOverflow, 0, 0),
                stretchWidth = true,
                stretchHeight = true,
                fontSize = fontSize,
                normal =
                {
                    background = texDefault,
                    scaledBackgrounds = new Texture2D[]{ texDefault }
                },
                focused =
                {
                    background = texFocused,
                    scaledBackgrounds = new Texture2D[]{ texFocused }
                },
                active =
                {
                    background = texFocused,
                    scaledBackgrounds = new Texture2D[]{ texFocused }
                },
                alignment = TextAnchor.UpperLeft,
                richText = true,
                wordWrap = true
            });
        }
    }
}