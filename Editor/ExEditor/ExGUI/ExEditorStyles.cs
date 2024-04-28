using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
// ReSharper disable All

namespace Glitch9.ExEditor
{
    public static class ExEditorStyles
    {
        private static readonly Dictionary<string, GUIStyle> _cache = new();
        // private static readonly RectOffset _defaultBoxPadding = new(6, 6, 2, 2);
        private static readonly RectOffset _defaultBoxMargin = new(2, 2, 2, 2);
        private static readonly RectOffset _defaultBoxPadding = new(2, 2, 2, 2);


        public static GUIStyle centeredRedMiniLabel
        {
            get
            {
                string key = "centeredRedMiniLabel";
                if (!_cache.TryGetValue(key, out GUIStyle _centeredRedMiniLabel))
                {
                    _centeredRedMiniLabel = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
                    {
                        alignment = TextAnchor.MiddleCenter,
                        normal = { textColor = Color.red }
                    };
                    _cache.Add(key, _centeredRedMiniLabel);
                }
                return _centeredRedMiniLabel;
            }
        }

        public static GUIStyle centeredBlueMiniLabel
        {
            get
            {
                string key = "centeredBlueMiniLabel";
                if (!_cache.TryGetValue(key, out GUIStyle _centeredBlueMiniLabel))
                {
                    _centeredBlueMiniLabel = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
                    {
                        alignment = TextAnchor.MiddleCenter,
                        normal = { textColor = Color.blue }
                    };
                    _cache.Add(key, _centeredBlueMiniLabel);
                }
                return _centeredBlueMiniLabel;
            }
        }


        public static GUIStyle background
        {
            get
            {
                string key = "background";
                if (!_cache.ContainsKey(key))
                {
                    _cache.Add(key, new GUIStyle
                    {
                        padding = new RectOffset(7, 7, 7, 7),
                        normal = { background = ExGUIUtility.grayTexture }
                    });
                }
                return _cache[key];
            }
        }

        public static GUIStyle foldout
        {
            get
            {
                string key = "foldout";
                if (!_cache.TryGetValue(key, out GUIStyle style))
                {
                    style = new(EditorStyles.foldout);
                    style.fontStyle = FontStyle.Bold;
                    _cache.Add(key, style);
                }
                return style;
            }
        }

        public static GUIStyle title
        {
            get
            {
                string key = "title";
                if (!_cache.TryGetValue(key, out GUIStyle style))
                {
                    style = new GUIStyle(EditorStyles.boldLabel);
                    style.fontSize = 14;
                    style.margin = new RectOffset(0, 0, 10, 10);
                    _cache.Add(key, style);
                }
                return style;
            }
        }

        public static GUIStyle array
        {
            get
            {
                string key = "array";
                if (!_cache.TryGetValue(key, out GUIStyle style))
                {
                    style = new GUIStyle(EditorStyles.helpBox);
                    style.padding = new RectOffset(5, 5, 5, 5);
                    style.margin = new RectOffset(0, 0, 3, 5);
                    _cache.Add(key, style);
                }
                return style;
            }
        }

        public static GUIStyle centeredIconButton
        {
            get
            {
                string key = "centeredIconButton";
                if (!_cache.TryGetValue(key, out GUIStyle style))
                {
                    style = new GUIStyle(EditorStyles.iconButton)
                    {
                        alignment = TextAnchor.MiddleCenter,
                        fixedHeight = EditorGUIUtility.singleLineHeight + 2,
                    };
                    _cache.Add(key, style);
                }
                return style;
            }
        }

        public static GUIStyle miniButton
        {
            get
            {
                string key = "miniButton";
                if (!_cache.TryGetValue(key, out GUIStyle style))
                {
                    style = new GUIStyle(EditorStyles.miniButton)
                    {
                        padding = new RectOffset(1, 1, 1, 1),
                        margin = new RectOffset(2, 2, 2, 2),
                        fixedHeight = 20,
                        fixedWidth = 20,
                    };
                    _cache.Add(key, style);
                }
                return style;
            }
        }

        public static GUIStyle iconButton
        {
            get
            {
                string key = "iconButton";
                if (!_cache.TryGetValue(key, out GUIStyle style))
                {
                    style = new GUIStyle(EditorStyles.iconButton)
                    {
                        alignment = TextAnchor.MiddleCenter,
                        fixedHeight = 18,
                        fixedWidth = 18,
                    };
                    _cache.Add(key, style);
                }
                return style;
            }
        }


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
                      direction == GUIBorder.Top ? 10 : 4
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
                        textColor = ExGUI.GetFontColor(color)
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

                style = new GUIStyle(ExGUI.skin.button);
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