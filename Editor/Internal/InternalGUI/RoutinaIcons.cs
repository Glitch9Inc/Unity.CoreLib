using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    public class RoutinaIcons
    {
        private static readonly Dictionary<string, Texture> _icons = new();

        private const string ICON_DIR = "CoreLib/Editor/Gizmos/Routina/";

        private static Texture Get(string iconName)
        {
            if (!_icons.TryGetValue(iconName, out Texture texture))
            {
                string path = EGUIUtility.GetTexturePath(ICON_DIR, iconName);
                if (path == null) return null;
                texture = AssetDatabase.LoadAssetAtPath<Texture>(path);
                _icons.Add(iconName, texture);
            }

            return texture;
        }

        public static Texture Tuto => Get("ic_circle_tuto.png");
        public static Texture Aimi => Get("ic_circle_aimi.png");
    }
}