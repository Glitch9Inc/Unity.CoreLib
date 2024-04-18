using UnityEditor;
using UnityEngine;

namespace Glitch9.ExEditor
{
    /// <summary>
    /// Built-in Editor Icons
    /// </summary>
    public static partial class EditorIcons
    {
        public static Texture Folder => EditorGUIUtility.IconContent("Folder Icon").image;
        public static Texture Refresh => EditorGUIUtility.IconContent("Refresh").image;
        public static Texture PlayButton => EditorGUIUtility.IconContent("PlayButton").image;
        public static Texture PauseButton => EditorGUIUtility.IconContent("PauseButton").image;
        public static Texture Settings => EditorGUIUtility.IconContent("Settings@2x").image;
        public static Texture BuiltIn => Settings;
        public static Texture Error => EditorGUIUtility.IconContent("console.erroricon.sml").image;
        public static Texture Edit => EditorGUIUtility.IconContent("d_editicon.sml").image;
        public static Texture Clipboard => EditorGUIUtility.IconContent("Clipboard").image;
        public static Texture Copy => Clipboard;
        public static Texture TrashDetailed => EditorGUIUtility.IconContent("d_TreeEditor.Trash").image;
        public static Texture Plus => EditorGUIUtility.IconContent("Toolbar Plus@2x").image;
        public static Texture Minus => EditorGUIUtility.IconContent("Toolbar Minus@2x").image;
        public static Texture Neutral => EditorGUIUtility.IconContent("d_winbtn_mac_min_h").image;
        public static Texture ScriptableObjectGray => EditorGUIUtility.IconContent("d_ScriptableObject On Icon").image;
        public static Texture ScriptableObject => EditorGUIUtility.IconContent("d_ScriptableObject Icon").image;
        public static Texture Valid => EditorGUIUtility.IconContent("Valid@2x").image;
        public static Texture Invalid => EditorGUIUtility.IconContent("Invalid@2x").image;
        public static Texture Info => EditorGUIUtility.IconContent("d_console.infoicon.inactive.sml@2x").image;
        public static Texture Warning => EditorGUIUtility.IconContent("d_console.warnicon.inactive.sml@2x").image;
        public static Texture RedQuestionMark => EditorGUIUtility.IconContent("P4_Conflicted@2x").image;
        public static Texture CSScript => EditorGUIUtility.IconContent("cs Script Icon").image;
        public static Texture CGProgram => EditorGUIUtility.IconContent("TextScriptImporter Icon").image;
        public static Texture OrangeLight => EditorGUIUtility.IconContent("d_orangeLight").image;
        public static Texture GreenLight => EditorGUIUtility.IconContent("d_greenLight").image;
        public static Texture Font => EditorGUIUtility.IconContent("TrueTypeFontImporter Icon").image;
        public static Texture Search => EditorGUIUtility.IconContent("Search Icon").image;
        public static Texture Tools => EditorGUIUtility.IconContent("SceneViewTools@2x").image;
        public static Texture Resize => EditorGUIUtility.IconContent("PositionAsUV1 Icon").image;
        public static Texture Close => EditorGUIUtility.IconContent("winbtn_win_close").image;
        public static Texture Image => EditorGUIUtility.IconContent("Image Icon").image;
    }
}