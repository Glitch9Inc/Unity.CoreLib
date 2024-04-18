using UnityEditor;
using UnityEngine;
// ReSharper disable All

namespace Glitch9.ExEditor
{
    public static class EditorSkin
    {
        private const string ASSET_PATH = "CoreLib/Editor/Gizmos/Skins/";
        private const string SKIN_NAME_LIGHT = "ExSkin_Light.guiskin";
        private const string SKIN_NAME_DARK = "ExSkin_Dark.guiskin";

        private static string ResolveSkinPath(string assetName) => ExGUIUtility.GetGlitch9Directory() + "/" + ASSET_PATH + assetName;

        private static GUISkin _skinLightDefault;
        private static GUISkin skinLightDefault
        {
            get
            {
                if (_skinLightDefault == null) _skinLightDefault = LoadSkin(ResolveSkinPath(SKIN_NAME_DARK));
                return _skinLightDefault;
            }
        }
        private static GUISkin _skinDarkDefault;
        private static GUISkin skinDarkDefault
        {
            get
            {
                if (_skinDarkDefault == null) _skinDarkDefault = LoadSkin(ResolveSkinPath(SKIN_NAME_DARK));
                return _skinDarkDefault;
            }
        }

        private static GUISkin LoadSkin(string path)
        {
            GUISkin skin = AssetDatabase.LoadAssetAtPath<GUISkin>(path);
            if (skin == null)
            {
                EditorUtility.DisplayDialog("Error", "Codeqo skin not found: " + path, "OK");
                return GUI.skin;
            }
            return skin;
        }


        public static GUISkin skin
        {
            get
            {
                if (EditorGUIUtility.isProSkin) return skinDarkDefault;
                else return skinLightDefault;
            }
        }

        [MenuItem("Tools/Glitch9/Setup/EditorGUI/Reload Skins")]
        public static void ReloadSkins()
        {
            if (EditorGUIUtility.isProSkin)
            {
                _skinDarkDefault = LoadSkin(ResolveSkinPath(SKIN_NAME_DARK));
            }
            else
            {
                _skinLightDefault = LoadSkin(ResolveSkinPath(SKIN_NAME_LIGHT));
            }
        }
    }
}
