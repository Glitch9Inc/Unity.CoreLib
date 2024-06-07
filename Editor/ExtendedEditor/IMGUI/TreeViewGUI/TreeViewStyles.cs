using Glitch9.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public class TreeViewStyles
    {
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

        internal const string STYLE_TREEVIEW_ITEM = "treeviewitem";
        internal const string STYLE_TREEVIEW_GROUP = "treeviewgroup";
        public static GUIStyle TreeViewItem => Get(nameof(TreeViewItem), EGUI.skin.GetStyle(STYLE_TREEVIEW_ITEM));
        public static GUIStyle TreeViewGroup => Get(nameof(TreeViewGroup), EGUI.skin.GetStyle(STYLE_TREEVIEW_GROUP));
    }
}