using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExEditor
{
    public static partial class EditorIcons
    {
        private static readonly Dictionary<string, Texture> _icons = new();

        // Root Dirs
        private const string ICON_DIR_LIGHT = "CoreLib/Editor/Gizmos/Icons/Light";
        private const string ICON_DIR_DARK = "CoreLib/Editor/Gizmos/Icons/Dark";

        private static Texture Get(string iconName)
        {
            if (!_icons.TryGetValue(iconName, out Texture texture))
            {
                string path = ExGUIUtility.GetTexturePath(GetRootDir(), iconName);
                if (path == null) return null;
                texture = AssetDatabase.LoadAssetAtPath<Texture>(path);
                _icons.Add(iconName, texture);
            }

            return texture;
        }

        private static string GetRootDir()
        {
            return ExGUI.IsDarkMode ? ICON_DIR_DARK : ICON_DIR_LIGHT;
        }

        // Util that gets light/dark icon based on current editor skin 
        private static Texture GetBuiltInIcon(string iconName)
        {
            if (!_icons.TryGetValue(iconName, out Texture texture))
            {
                if (ExGUI.IsDarkMode)
                {
                    string d_iconName = $"d_{iconName}";
                    texture = EditorGUIUtility.IconContent(d_iconName).image;
                }
                texture ??= EditorGUIUtility.IconContent(iconName).image;
                _icons.Add(iconName, texture);
            }

            return texture;
        }
    }
}