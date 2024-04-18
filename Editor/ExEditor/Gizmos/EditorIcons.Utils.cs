using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExEditor
{
    public static partial class EditorIcons
    {
        private static readonly Dictionary<string, Texture> _lightIcons = new();
        private static readonly Dictionary<string, Texture> _darkIcons = new();

        // Root Dirs
        private const string ICON_DIR_LIGHT = "CoreLib/Editor/Gizmos/Icons/Light";
        private const string ICON_DIR_DARK = "CoreLib/Editor/Gizmos/Icons/Dark";

        private static Texture Get(string iconName)
        {
            if (ExGUI.IsDarkMode)
            {
                Texture tex = GetIconFromDictionary(ICON_DIR_DARK, iconName, _darkIcons);
                if (tex != null) return tex;
            }

            return GetIconFromDictionary(ICON_DIR_LIGHT, iconName, _lightIcons);
        }

        private static Texture GetIconFromDictionary(string rootDir, string iconName, IDictionary<string, Texture> dictionary)
        {
            if (!dictionary.TryGetValue(iconName, out Texture texture))
            {
                string path = ExGUIUtility.GetTexturePath(rootDir, iconName);
                if (path == null) return null;
                texture = AssetDatabase.LoadAssetAtPath<Texture>(path);
                dictionary.Add(iconName, texture);
            }
            return texture;
        }
    }
}