using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    public enum StatusColor
    {
        Gray,
        Green,
        Yellow,
        Red,
    }

    /// <summary>
    /// Built-in Editor Icons
    /// </summary>
    public static partial class EditorIcons
    {
        public static Texture Folder => GetBuiltInIcon("Folder Icon");
        public static Texture Refresh => GetBuiltInIcon("Refresh");
        public static Texture Reset => Refresh;
        public static Texture PlayButton => GetBuiltInIcon("PlayButton");
        public static Texture PauseButton => GetBuiltInIcon("PauseButton");
        public static Texture Settings => GetBuiltInIcon("Settings");
        public static Texture BuiltIn => Settings;
        public static Texture Edit => GetBuiltInIcon("d_editicon.sml");
        public static Texture Clipboard => GetBuiltInIcon("Clipboard");
        public static Texture Copy => Clipboard;
        public static Texture TrashDetailed => GetBuiltInIcon("d_TreeEditor.Trash");
        public static Texture Plus => GetBuiltInIcon("Toolbar Plus@2x");
        public static Texture Minus => GetBuiltInIcon("Toolbar Minus@2x");
        public static Texture Neutral => GetBuiltInIcon("d_winbtn_mac_min_h");
        public static Texture ScriptableObjectGray => GetBuiltInIcon("d_ScriptableObject On Icon");
        public static Texture ScriptableObject => GetBuiltInIcon("d_ScriptableObject Icon");
        public static Texture Valid => GetBuiltInIcon("Valid@2x");
        public static Texture Invalid => GetBuiltInIcon("Invalid@2x");
        public static Texture Info => GetBuiltInIcon("d_console.infoicon.inactive.sml@2x");
        public static Texture Warning => GetBuiltInIcon("Warning");
        public static Texture Error => GetBuiltInIcon("Error");
        public static Texture RedQuestionMark => GetBuiltInIcon("P4_Conflicted@2x");
        public static Texture CSScript => GetBuiltInIcon("cs Script Icon");
        public static Texture CGProgram => GetBuiltInIcon("TextScriptImporter Icon");
        public static Texture OrangeLight => GetBuiltInIcon("d_orangeLight");
        public static Texture GreenLight => GetBuiltInIcon("d_greenLight");
        public static Texture Font => GetBuiltInIcon("TrueTypeFontImporter Icon");
        public static Texture Search => GetBuiltInIcon("Search Icon");
        public static Texture Tools => GetBuiltInIcon("SceneViewTools@2x");
        public static Texture Resize => GetBuiltInIcon("PositionAsUV1 Icon");
        public static Texture Close => GetBuiltInIcon("winbtn_win_close");
        public static Texture Image => GetBuiltInIcon("Image Icon");
        public static Texture InputField => GetBuiltInIcon("InputField Icon");
        public static Texture Loading => GetBuiltInIcon("Loading");
        public static Texture Loading2x => GetBuiltInIcon("Loading@2x");
        public static Texture BooScript => GetBuiltInIcon("boo Script Icon");
        public static Texture ToolBarPlusMore => GetBuiltInIcon("Toolbar Plus More");
        public static Texture ToolBarPlusMore2x => GetBuiltInIcon("Toolbar Plus More@2x");
        public static Texture ToolBarPlus => GetBuiltInIcon("Toolbar Plus");
        public static Texture ToolBarPlus2x => GetBuiltInIcon("Toolbar Plus@2x");
        public static Texture ToolBarMinus => GetBuiltInIcon("Toolbar Minus");
        public static Texture ToolBarMinus2x => GetBuiltInIcon("Toolbar Minus@2x");
        public static Texture Menu => GetBuiltInIcon("_Menu");
        public static Texture Menu2x => GetBuiltInIcon("_Menu@2x");

        /// <summary>
        /// Icon looks like branching
        /// </summary>
        public static Texture AnimatorIcon => GetBuiltInIcon("Animator Icon");



        public static Texture StatusLight(StatusColor color)
        {
            return color switch
            {
                StatusColor.Green => GetBuiltInIcon("greenLight"),
                StatusColor.Yellow => GetBuiltInIcon("orangeLight"),
                StatusColor.Red => GetBuiltInIcon("redLight"),
                _ => GetBuiltInIcon("lightOff")
            };
        }
    }
}