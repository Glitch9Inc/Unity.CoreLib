using UnityEngine;

namespace Glitch9.ExEditor
{
    public static partial class EditorGUITextures
    {
        private static Texture2D BoxDefault => Get(DIR_BOX, "section-box.psd");
        private static Texture2D BoxGreen => Get(DIR_BOX, "section-box-green.psd");
        private static Texture2D BoxYellow => Get(DIR_BOX, "section-box-yellow.psd");
        private static Texture2D BoxOrange => Get(DIR_BOX, "section-box-orange.psd");
        private static Texture2D BoxPurple => Get(DIR_BOX, "section-box-purple.psd");
        private static Texture2D BoxBlue => Get(DIR_BOX, "section-box-blue.psd");
        
        private static Texture2D TextFieldDefault => Get(DIR_TEXTFIELD, "textfield-light.png");
        private static Texture2D TextFieldDefaultFocused => Get(DIR_TEXTFIELD, "textfield-light-focused.png");
        private static Texture2D TextFieldGreen => Get(DIR_TEXTFIELD, "textfield-green.png");
        private static Texture2D TextFieldGreenFocused => Get(DIR_TEXTFIELD, "textfield-green-focused.png");
        private static Texture2D TextFieldYellow => Get(DIR_TEXTFIELD, "textfield-yellow.png");
        private static Texture2D TextFieldYellowFocused => Get(DIR_TEXTFIELD, "textfield-yellow-focused.png");
        private static Texture2D TextFieldOrange => Get(DIR_TEXTFIELD, "textfield-orange.png");
        private static Texture2D TextFieldOrangeFocused => Get(DIR_TEXTFIELD, "textfield-orange-focused.png");
        private static Texture2D TextFieldPurple => Get(DIR_TEXTFIELD, "textfield-purple.png");
        private static Texture2D TextFieldPurpleFocused => Get(DIR_TEXTFIELD, "textfield-purple-focused.png");
        private static Texture2D TextFieldBlue => Get(DIR_TEXTFIELD, "textfield-blue.png");
        private static Texture2D TextFieldBlueFocused => Get(DIR_TEXTFIELD, "textfield-blue-focused.png");
        private static Texture2D TextFieldRed => Get(DIR_TEXTFIELD, "textfield-red.png");
        private static Texture2D TextFieldRedFocused => Get(DIR_TEXTFIELD, "textfield-red-focused.png");
        private static Texture2D TextFieldGray => Get(DIR_TEXTFIELD, "textfield-gray.png");
        private static Texture2D TextFieldGrayFocused => Get(DIR_TEXTFIELD, "textfield-gray-focused.png");

        private static Texture2D BtnDefault => Get(DIR_BUTTON, "btn-default.psd");
        private static Texture2D BtnGreen => Get(DIR_BUTTON, "btn-green.psd");
        private static Texture2D BtnPurple => Get(DIR_BUTTON, "btn-purple.psd");
        private static Texture2D BtnYellow => Get(DIR_BUTTON, "btn-yellow.psd");
        private static Texture2D BtnBlue => Get(DIR_BUTTON, "btn-blue.psd");
        private static Texture2D BtnOrange => Get(DIR_BUTTON, "btn-orange.psd");
        private static Texture2D BtnRed => Get(DIR_BUTTON, "btn-red.psd");

        private static Texture2D BtnDefaultSelected => Get(DIR_BUTTON, "btn-default-selected.psd");
        private static Texture2D BtnGreenSelected => Get(DIR_BUTTON, "btn-green-selected.psd");
        private static Texture2D BtnPurpleSelected => Get(DIR_BUTTON, "btn-purple-selected.psd");
        private static Texture2D BtnYellowSelected => Get(DIR_BUTTON, "btn-yellow-selected.psd");
        private static Texture2D BtnBlueSelected => Get(DIR_BUTTON, "btn-blue-selected.psd");
        private static Texture2D BtnOrangeSelected => Get(DIR_BUTTON, "btn-orange-selected.psd");
        private static Texture2D BtnRedSelected => Get(DIR_BUTTON, "btn-red-selected.psd");


        public static Texture2D ToolBarButtonOn => Get(DIR_TOOLBAR, "btn-mid-on.psd");
        public static Texture2D ToolBarButtonOff => Get(DIR_TOOLBAR, "btn-mid-off.psd");
        public static Texture2D BorderTop => Get(DIR_BORDER, "section-border-top.psd");
        public static Texture2D BorderBottom => Get(DIR_BORDER, "section-border-bottom.psd");
        public static Texture2D BorderBottomWithBlueLine => Get(DIR_BORDER, "section-border-bottom-blueline.psd");
        public static Texture2D Background => Get(DIR_BACKGROUND, "section-background.psd");


        // Config
        public static Texture2D ToggleDescriptionOn => Get(DIR_CONFIG, "toggle_description_on.psd");
        public static Texture2D ToggleDescriptionOff => Get(DIR_CONFIG, "toggle_description_off.psd");
        public static Texture2D ToggleIconOn => Get(DIR_CONFIG, "toggle_icon_on.psd");
        public static Texture2D ToggleIconOff => Get(DIR_CONFIG, "toggle_icon_off.psd");
        public static Texture2D ToggleLinebreakOn => Get(DIR_CONFIG, "toggle_linebreak_on.psd");
        public static Texture2D ToggleLinebreakOff => Get(DIR_CONFIG, "toggle_linebreak_off.psd");
        public static Texture2D ToggleNextlineOn => Get(DIR_CONFIG, "toggle_nextline_on.psd");
        public static Texture2D ToggleNextlineOff => Get(DIR_CONFIG, "toggle_nextline_off.psd");
    }
}