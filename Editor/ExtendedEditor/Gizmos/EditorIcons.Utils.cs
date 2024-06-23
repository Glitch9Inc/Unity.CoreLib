using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
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
                if (EGUI.IsDarkMode)
                {
                    texture = LoadDarkTexture(iconName);
                    if (texture == null) texture = LoadLightTexture(iconName);
                }
                else
                {
                    texture = LoadLightTexture(iconName);
                }

                _icons.Add(iconName, texture);
            }

            return texture;
        }


        private static Texture LoadLightTexture(string iconName)
        {
            string path = EGUIUtility.GetTexturePath(ICON_DIR_LIGHT, iconName);
            if (path == null) return null;
            return AssetDatabase.LoadAssetAtPath<Texture>(path);
        }
        
        private static Texture LoadDarkTexture(string iconName)
        {
            string path = EGUIUtility.GetTexturePath(ICON_DIR_DARK, iconName);
            if (path == null) return null;
            return AssetDatabase.LoadAssetAtPath<Texture>(path);
        }

        // Util that gets light/dark icon based on current editor skin 
        private static Texture GetBuiltInIcon(string iconName)
        {
            if (EGUI.IsDarkMode) iconName = $"d_{iconName}";

            if (!_icons.TryGetValue(iconName, out Texture texture))
            {
                texture = EditorGUIUtility.IconContent(iconName).image;
                _icons.Add(iconName, texture);
            }

            return texture;
        }
    }
}