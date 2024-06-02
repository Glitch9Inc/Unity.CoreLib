using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    public static partial class EditorGUITextures
    {
        private static readonly Dictionary<string, Texture2D> _lightTextures = new();
        private static readonly Dictionary<string, Texture2D> _darkTextures = new();

        // Root Dirs
        private const string TEXTURE_DIR = "CoreLib/Editor/Gizmos/EditorGUI/";

        // Dark Mode Format
        private const string DARK_TEX_PATH = "d_{0}";

        // Sub Dirs
        private const string DIR_ANDROID12 = "Android12/";
        private const string DIR_BACKGROUND = "Background/";
        private const string DIR_BORDER = "Border/";
        private const string DIR_BOX = "Box/";
        private const string DIR_CONFIG = "Config/";
        private const string DIR_SLIDER = "Slider/";
        private const string DIR_MEDIA = "Media/";
        private const string DIR_TEXTFIELD = "TextField/";
        private const string DIR_TOOLBAR = "ToolBar/";
        private const string DIR_EXTRA = "Extra/";
        private const string DIR_BUTTON = "Button/";

        private static Texture2D Get(string subDir, string textureName)
        {
            if (EGUI.IsDarkMode)
            {
                string darkTextureName = string.Format(DARK_TEX_PATH, textureName);
                Texture2D tex = GetTextureFromDictionary(subDir, darkTextureName, _darkTextures);
                if (tex != null) return tex;
            }

            return GetTextureFromDictionary(subDir, textureName, _lightTextures);
        }

        private static Texture2D GetTextureFromDictionary(string subDir, string textureName, IDictionary<string, Texture2D> dictionary)
        {
            if (!dictionary.TryGetValue(textureName, out Texture2D texture))
            {
                string dirCombined = TEXTURE_DIR + "/" + subDir;
                string path = EGUIUtility.GetTexturePath(dirCombined, textureName);
                if (path == null) return null;
                texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                dictionary.Add(textureName, texture);
            }
            return texture;
        }

        public static Texture2D Box(GUIColor color = 0)
        {
            return color switch
            {
                GUIColor.Green => BoxGreen,
                GUIColor.Yellow => BoxYellow,
                GUIColor.Orange => BoxOrange,
                GUIColor.Purple => BoxPurple,
                GUIColor.Blue => BoxBlue,
                GUIColor.White => EditorGUIUtility.whiteTexture,
                _ => BoxDefault,
            };
        }

        public static Texture2D TextField(GUIColor color = 0, bool isFocused = false)
        {
            return color switch
            {
                GUIColor.Green => isFocused ? TextFieldGreenFocused : TextFieldGreen,
                GUIColor.Yellow => isFocused ? TextFieldYellowFocused : TextFieldYellow,
                GUIColor.Orange => isFocused ? TextFieldOrangeFocused : TextFieldOrange,
                GUIColor.Purple => isFocused ? TextFieldPurpleFocused : TextFieldPurple,
                GUIColor.Blue => isFocused ? TextFieldBlueFocused : TextFieldBlue,
                GUIColor.Red => isFocused ? TextFieldRedFocused : TextFieldRed,
                GUIColor.Gray => isFocused ? TextFieldGrayFocused : TextFieldGray,
                _ => isFocused ? TextFieldDefaultFocused : TextFieldDefault,
            };
        }

        public static Texture2D Button(GUIColor color, bool isSelected)
        {
            return (color, selected: isSelected) switch
            {
                (GUIColor.Green, true) => BtnGreenSelected,
                (GUIColor.Purple, true) => BtnPurpleSelected,
                (GUIColor.Yellow, true) => BtnYellowSelected,
                (GUIColor.Blue, true) => BtnBlueSelected,
                (GUIColor.Orange, true) => BtnOrangeSelected,
                (GUIColor.Red, true) => BtnRedSelected,

                (GUIColor.Green, false) => BtnGreen,
                (GUIColor.Purple, false) => BtnPurple,
                (GUIColor.Yellow, false) => BtnYellow,
                (GUIColor.Blue, false) => BtnBlue,
                (GUIColor.Orange, false) => BtnOrange,
                (GUIColor.Red, false) => BtnRed,

                (GUIColor.None, true) => BtnDefaultSelected,
                _ => BtnDefault,
            };
        }
    }
}