using System.Collections.Generic;
using Glitch9.UI;
using UnityEditor;
using UnityEngine;
// ReSharper disable All

namespace Glitch9.ExtendedEditor
{
    public static class EGUIStyles
    {
        private static readonly Dictionary<string, GUIStyle> _cache = new();
        private static GUIStyle Get(string key, GUIStyle defaultStyle)
        {
            if (!_cache.TryGetValue(key, out GUIStyle style))
            {
                style = new GUIStyle(defaultStyle);
                _cache[key] = style;
            }
            return style;
        }

        private static readonly RectOffset _defaultBoxMargin = new(2, 2, 2, 2);
        private static readonly RectOffset _defaultBoxPadding = new(2, 2, 2, 2);


        public static GUIStyle centeredRedMiniLabel => Get(nameof(centeredRedMiniLabel), new GUIStyle(EditorStyles.centeredGreyMiniLabel)
        {
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = Color.red }
        });

        public static GUIStyle centeredBlueMiniLabel => Get(nameof(centeredBlueMiniLabel), new GUIStyle(EditorStyles.centeredGreyMiniLabel)
        {
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = Color.blue }
        });

        public static GUIStyle background => Get(nameof(background), new GUIStyle
        {
            padding = new RectOffset(7, 7, 7, 7),
            normal = { background = EGUIUtility.grayTexture }
        });

        public static GUIStyle foldout => Get(nameof(foldout), new GUIStyle(EditorStyles.foldout)
        {
            fontStyle = FontStyle.Bold
        });


        public static GUIStyle title => Get(nameof(title), new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 14,
            margin = new RectOffset(0, 0, 10, 10)
        });

        public static GUIStyle array => Get(nameof(array), new GUIStyle(EditorStyles.helpBox)
        {
            padding = new RectOffset(5, 5, 5, 5),
            margin = new RectOffset(0, 0, 3, 5)
        });

        public static GUIStyle centeredIconButton => Get(nameof(centeredIconButton), new GUIStyle(EditorStyles.iconButton)
        {
            alignment = TextAnchor.MiddleCenter,
            fixedHeight = EditorGUIUtility.singleLineHeight + 2,
        });

        public static GUIStyle miniButton => Get(nameof(miniButton), new GUIStyle(EditorStyles.miniButton)
        {
            padding = new RectOffset(1, 1, 1, 1),
            margin = new RectOffset(2, 2, 2, 2),
            fixedHeight = 20,
            fixedWidth = 20,
        });

        public static GUIStyle iconButton => Get(nameof(iconButton), new GUIStyle(EditorStyles.iconButton)
        {
            alignment = TextAnchor.MiddleCenter,
            fixedHeight = 18,
            fixedWidth = 18,
        });


        public static GUIStyle disabledTextField => Get(nameof(disabledTextField), new GUIStyle()
        {
            border = new RectOffset(2, 2, 2, 2),
            margin = new RectOffset(2, 2, 2, 2),
            padding = new RectOffset(3, 3, 3, 3),
            normal =
            {
                background = EditorGUITextures.TextField(GUIColor.Gray),
                textColor = ExColor.charcoal,
            },
            fontSize = 12,
            fontStyle = FontStyle.Normal,
        });

        public static GUIStyle popupBody => Get(nameof(popupBody), new GUIStyle()
        {
            padding = new RectOffset(5, 5, 10, 5)
        });

        
        private static GUIStyle Border(GUIBorder direction, RectOffset padding)
        {
            string key = $"{direction}_{padding.left},{padding.right},{padding.top},{padding.bottom}";
            if (!_cache.TryGetValue(key, out GUIStyle style))
            {
                Texture2D boxTex = direction == GUIBorder.Top ? EditorGUITextures.BorderTop : EditorGUITextures.BorderBottom;
                style = new GUIStyle
                {
                    border = new RectOffset(5, 5, 5, 5),
                    margin = new RectOffset(0, 0, 0, 0),
                    padding = padding,
                    normal = { background = boxTex }
                };
                _cache.Add(key, style);
            }
            return style;
        }

        public static GUIStyle Border(GUIBorder direction)
        {
            RectOffset padding = new(
                      7,
                      7,
                      direction == GUIBorder.Top ? 7 : 10,
                      direction == GUIBorder.Top ? 7 : 4
                  );

            return Border(direction, padding);
        }

        public static GUIStyle Label(TextAnchor alignment, int fontSize, Color fontColor, bool bold = false)
        {
            string key = $"label_{alignment}_{fontSize}_{fontColor.ToHex()}_{(bold ? "b" : "r")}";
            if (!_cache.TryGetValue(key, out GUIStyle style))
            {
                style = new GUIStyle(bold ? EditorStyles.boldLabel : EditorStyles.label)
                {
                    alignment = alignment,
                    fontSize = fontSize
                };
                style.normal.textColor = fontColor;
                _cache[key] = style;
            }
            return style;
        }

        public static GUIStyle Label(TextAnchor alignment, int fontSize = 10, GUIColor color = GUIColor.None)
        {
            string key = $"label_{alignment}_{fontSize}_{color}";
            if (!_cache.TryGetValue(key, out GUIStyle style))
            {
                style = new GUIStyle(GUI.skin.label)
                {
                    alignment = alignment,
                    fontSize = fontSize,
                    normal =
                    {
                        textColor = EGUI.GetFontColor(color)
                    }
                };
                _cache[key] = style;
            }
            return style;
        }

        public static GUIStyle Button(TextAnchor alignment, int fontSize = 10, GUIColor color = GUIColor.None, bool isSelected = false)
        {
            string key = $"button_{alignment}_{fontSize}_{color}_{isSelected}";
            if (!_cache.TryGetValue(key, out GUIStyle style))
            {
                Texture2D tex = EditorGUITextures.Button(color, isSelected);

                style = new GUIStyle(EGUI.skin.button);
                style.normal.textColor = Color.black;
                style.hover.textColor = Color.black;
                style.active.textColor = Color.black;
                style.focused.textColor = Color.black;
                style.onNormal.textColor = Color.black;
                style.onHover.textColor = Color.black;
                style.onActive.textColor = Color.black;
                style.onFocused.textColor = Color.black;

                style.normal.background = tex;
                style.hover.background = tex;
                style.active.background = tex;
                style.focused.background = tex;
                style.onNormal.background = tex;
                style.onHover.background = tex;
                style.onActive.background = tex;
                style.onFocused.background = tex;
                style.wordWrap = true;

                style.alignment = alignment;
                style.fontSize = fontSize;
                _cache[key] = style;
            }

            return style;
        }
        public static GUIStyle Button(bool isSelected)
            => Button(TextAnchor.MiddleCenter, 10, GUIColor.None, isSelected);
        public static GUIStyle Button(GUIColor color, bool isSelected = false)
            => Button(TextAnchor.MiddleCenter, 10, color, isSelected);

        public static GUIStyle MiniButton(TextAnchor alignment)
        {
            string key = $"minibutton_{alignment}";
            if (!_cache.TryGetValue(key, out GUIStyle style))
            {
                style = new GUIStyle(GUI.skin.button);
                style.fontSize = 10;
                style.wordWrap = true;
                style.alignment = alignment;
                _cache[key] = style;
            }
            return style;
        }

        public static GUIStyle Box(TextAnchor alignment = TextAnchor.MiddleLeft, int fontSize = 10, GUIColor color = GUIColor.None, RectOffset margin = null, RectOffset padding = null)
        {
            margin ??= _defaultBoxMargin;
            padding ??= _defaultBoxPadding;
            string key = $"box_{alignment}_{fontSize}_{color}_{margin.left},{margin.right},{margin.top},{margin.bottom}_{padding.left},{padding.right},{padding.top},{padding.bottom}";
            if (!_cache.TryGetValue(key, out GUIStyle style))
            {
                style = new GUIStyle
                {
                    border = new RectOffset(5, 5, 5, 5),
                    margin = margin,
                    padding = padding,
                    fontSize = fontSize,
                    normal = { background = EditorGUITextures.Box(color) },
                    overflow = new RectOffset(0, 0, 0, 0),
                    alignment = alignment,
                    wordWrap = true
                };
                _cache[key] = style;
            }
            return style;
        }

        public static GUIStyle Box(GUIColor color, RectOffset margin = null, RectOffset padding = null)
            => Box(TextAnchor.UpperLeft, 12, color, margin, padding);

        public static GUIStyle Box(TextAnchor alignment, GUIColor color, RectOffset margin = null, RectOffset padding = null)
            => Box(alignment, 12, color, margin, padding);

        public static GUIStyle TableBox(int fontSize = 12, GUIColor color = GUIColor.None)
            => Box(TextAnchor.MiddleLeft, fontSize, color, new RectOffset(0, 0, 0, 0), new RectOffset(8, 8, 6, 6));


        public static GUIStyle TreeViewField(TextAnchor alignment, int fontSize = 10, GUIColor color = GUIColor.None)
        {
            string key = $"tree_{alignment}_{fontSize}_{color}";
            if (!_cache.TryGetValue(key, out GUIStyle style))
            {
                style = new GUIStyle
                {
                    border = new RectOffset(5, 5, 5, 5),
                    margin = new RectOffset(0, 0, 0, 0),
                    padding = new RectOffset(6, 6, 2, 2),
                    fontSize = fontSize,
                    normal = { background = EditorGUITextures.TextField(color) },
                    overflow = new RectOffset(4, 4, 0, 0),
                    alignment = alignment,
                    wordWrap = true
                };
                _cache[key] = style;
            }
            return style;
        }
        public static GUIStyle TreeViewField(TextAnchor alignment, GUIColor color)
            => TreeViewField(alignment, 12, color);

        public static GUIStyle TextField(GUIColor color)
        {
            string key = $"textfield_{color}";
            if (!_cache.TryGetValue(key, out GUIStyle style))
            {
                style = new GUIStyle(EditorStyles.textField)
                {
                    border = new RectOffset(5, 5, 5, 5),
                    margin = new RectOffset(0, 0, 0, 0),
                    padding = new RectOffset(6, 6, 6, 6),
                    overflow = new RectOffset(3, 3, 0, 0),
                    fontSize = 10,
                    normal = { background = EditorGUITextures.TextField(color) },
                    alignment = TextAnchor.UpperLeft,
                    wordWrap = true
                };

                _cache[key] = style;
            }
            return style;
        }

        public static GUIStyle ListItem(GUIColor color)
        {
            string key = $"listitem_{color}";
            if (!_cache.TryGetValue(key, out GUIStyle style))
            {
                style = new GUIStyle
                {
                    border = new RectOffset(4, 4, 4, 4),
                    margin = new RectOffset(4, 4, 2, 2),
                    padding = new RectOffset(9, 9, 5, 5),
                    fontSize = 11,
                    normal = { background = EditorGUITextures.Box(color) },
                    alignment = TextAnchor.UpperLeft,
                    fontStyle = FontStyle.Bold,
                    wordWrap = true
                };

                _cache[key] = style;
            }
            return style;
        }

    }
}