#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Glitch9.Internal
{
    public class UnityMenu
    {
        private const string ROOT_TOOL_PATH = "Tools/Glitch9/";
        private const string ROOT_CREATE_PATH = "Glitch9/";
        private const string ROOT_USER_PREFERENCE = "Preferences/Glitch9/";

        private const string NAME_PREFERENCES = "Preferences";
        private const string NAME_DOCUMENTATION = "Documentation";
        private const string NAME_REPORT_ISSUES = "Report An Issue";

        private const int STARTING_CREATE_MENU_ORDER = 0;
        private const int STARTING_TOOLS_MENU_PRIORITY = -5000;
        private const int STARTING_SUPPORT_MENU_PRIORITY = -2000;

        private const int SPACE_BETWEEN_MODULES = 5;
        private const int ADD_SEPARATOR = 15;
        private const int NEXT_ROW = 1;

        public const string URL_SUPPORT_REPO = "https://github.com/Glitch9Inc/Glitch9-Support";


        public static class NativeMediaPlayer
        {
            private const string NAME = "Native Media Player";
            private const string TOOLS_PATH = ROOT_TOOL_PATH + NAME + "/";
            private const string CREATE_PATH = ROOT_CREATE_PATH + NAME + "/";

            // Menus
            public const string NAME_ANDROID12_PREVIEW = "Android 12 Preview";
            public const string NAME_IOS13_PREVIEW = "iOS 13 Preview";

            public const string PATH_ANDROID12_PREVIEW = TOOLS_PATH + NAME_ANDROID12_PREVIEW;
            public const string PATH_IOS13_PREVIEW = TOOLS_PATH + NAME_IOS13_PREVIEW;
            public const string PATH_PREFERENCES = TOOLS_PATH + NAME_PREFERENCES;
            public const string PATH_DOCUMENTATION = TOOLS_PATH + NAME_DOCUMENTATION;

            // Priorities
            public const int PRIORITY_ANDROID12_PREVIEW = STARTING_TOOLS_MENU_PRIORITY;
            public const int PRIORITY_IOS13_PREVIEW = PRIORITY_ANDROID12_PREVIEW + NEXT_ROW;

            public const int PRIORITY_PREFERENCES = PRIORITY_IOS13_PREVIEW + ADD_SEPARATOR;
            public const int PRIORITY_DOCUMENTATION = PRIORITY_PREFERENCES + NEXT_ROW;  

            public const string URL_DOCUMENTATION = "https://glitch9.gitbook.io/native-media-player";
            public const string PROVIDER_SETTINGS = ROOT_USER_PREFERENCE + NAME;
        }

        public static class OpenAI // AI Development Kit
        {
            private const string NAME = "OpenAI";
            private const string TOOLS_PATH = ROOT_TOOL_PATH + NAME + "/";
            private const string CREATE_PATH = ROOT_CREATE_PATH + NAME + "/";

            // Scriptable Objects
            public const string NAME_SETTINGS = "Settings";
            public const string NAME_LOGS = "Log Container";
            public const string NAME_FILES = "File Container";
            public const string NAME_AI_MODEL_METADATA = "AI Model Metadata";

            public const string NAME_CHAT_STREAMER = "Chat Streamer";
            public const string NAME_IMAGE_GENERATOR = "Image Generator";
            public const string NAME_VOICE_GENERATOR = "Voice Generator";
            public const string NAME_VOICE_TRANSCRIBER = "Voice Transcriber";

            public const string CREATE_SETTINGS = CREATE_PATH + NAME_SETTINGS;
            public const string CREATE_LOGS = CREATE_PATH + NAME_LOGS;
            public const string CREATE_FILES = CREATE_PATH + NAME_FILES;
            public const string CREATE_AI_MODEL_METADATA = CREATE_PATH + NAME_AI_MODEL_METADATA;

            public const string CREATE_CHAT_STREAMER = CREATE_PATH + NAME_CHAT_STREAMER;
            public const string CREATE_IMAGE_GENERATOR = CREATE_PATH + NAME_IMAGE_GENERATOR;
            public const string CREATE_VOICE_GENERATOR = CREATE_PATH + NAME_VOICE_GENERATOR;
            public const string CREATE_VOICE_TRANSCRIBER = CREATE_PATH + NAME_VOICE_TRANSCRIBER;

            public const int ORDER_SETTINGS = STARTING_CREATE_MENU_ORDER;
            public const int ORDER_LOGS = ORDER_SETTINGS + NEXT_ROW;
            public const int ORDER_FILES = ORDER_LOGS + NEXT_ROW;
            public const int ORDER_AI_MODEL_METADATA = ORDER_FILES + NEXT_ROW;

            public const int ORDER_CHAT_STREAMER = ORDER_AI_MODEL_METADATA + ADD_SEPARATOR;
            public const int ORDER_IMAGE_GENERATOR = ORDER_CHAT_STREAMER + NEXT_ROW;
            public const int ORDER_VOICE_GENERATOR = ORDER_IMAGE_GENERATOR + NEXT_ROW;
            public const int ORDER_VOICE_TRANSCRIBER = ORDER_VOICE_GENERATOR + NEXT_ROW;

            // Menus
            public const string NAME_COMPLETION = "Editor Completion";
            public const string NAME_CHAT_GPT = "Editor ChatGPT";
            public const string NAME_DALLE = "Editor DALL·E";
            public const string NAME_SPEECH = "Editor Speech";

            public const string NAME_LOG_MANAGER = "Request Log Manager";
            public const string NAME_AI_MODEL_MANAGER = "AI Model Manager";
            public const string NAME_ASSISTANT_MANAGER = "Assistant Manager";
            public const string NAME_FILE_MANAGER = "File Manager";

            public const string PATH_COMPLETION = TOOLS_PATH + NAME_COMPLETION;
            public const string PATH_CHAT_GPT = TOOLS_PATH + NAME_CHAT_GPT;
            public const string PATH_DALLE = TOOLS_PATH + NAME_DALLE;
            public const string PATH_SPEECH = TOOLS_PATH + NAME_SPEECH;

            public const string PATH_LOG_MANAGER = TOOLS_PATH + NAME_LOG_MANAGER;
            public const string PATH_AI_MODEL_MANAGER = TOOLS_PATH + NAME_AI_MODEL_MANAGER;
            public const string PATH_ASSISTANT_MANAGER = TOOLS_PATH + NAME_ASSISTANT_MANAGER;
            public const string PATH_FILE_MANAGER = TOOLS_PATH + NAME_FILE_MANAGER;

            public const string PATH_PREFERENCES = TOOLS_PATH + NAME_PREFERENCES;
            public const string PATH_DOCUMENTATION = TOOLS_PATH + NAME_DOCUMENTATION;
            public const string PATH_SUPPORT = TOOLS_PATH + NAME_REPORT_ISSUES;


            public const int PRIORITY_COMPLETION = STARTING_TOOLS_MENU_PRIORITY;
            public const int PRIORITY_CHAT_GPT = PRIORITY_COMPLETION + NEXT_ROW;
            public const int PRIORITY_DALLE = PRIORITY_CHAT_GPT + NEXT_ROW;
            public const int PRIORITY_SPEECH = PRIORITY_DALLE + NEXT_ROW;

            public const int PRIORITY_LOG_MANAGER = PRIORITY_SPEECH + ADD_SEPARATOR;
            public const int PRIORITY_AI_MODEL_MANAGER = PRIORITY_LOG_MANAGER + NEXT_ROW;
            public const int PRIORITY_ASSISTANT_MANAGER = PRIORITY_AI_MODEL_MANAGER + NEXT_ROW;
            public const int PRIORITY_FILE_MANAGER = PRIORITY_ASSISTANT_MANAGER + NEXT_ROW;

            public const int PRIORITY_SETTINGS = PRIORITY_ASSISTANT_MANAGER + ADD_SEPARATOR;
            public const int PRIORITY_DOCUMENTATION = PRIORITY_SETTINGS + NEXT_ROW;

            public const string URL_DOCUMENTATION = "https://glitch9.gitbook.io/ai-development-kit/";
            public const string PROVIDER_SETTINGS = ROOT_USER_PREFERENCE + NAME;
        }

        public static class SmartLocalization
        {
            private const string NAME = "Smart Localization";
            private const string TOOLS_PATH = ROOT_TOOL_PATH + NAME + "/";
            private const string CREATE_PATH = ROOT_CREATE_PATH + NAME + "/";

            // Scriptable Objects
            public const string NAME_LOCALIZATION_SETTINGS = "Localization Settings";
            public const string CREATE_LOCALIZATION_SETTINGS = CREATE_PATH + NAME_LOCALIZATION_SETTINGS;
            public const int ORDER_LOCALIZATION_SETTINGS = STARTING_CREATE_MENU_ORDER;

            // Menus
            public const string NAME_LOCALIZATION_MANAGER = "Localization Manager";
            public const string NAME_TABLE_MANAGER = "Localization Tables";
            public const string NAME_SUFFIX_MANAGER = "Localization Suffixes";

            public const string PATH_LOCALIZATION_MANAGER = TOOLS_PATH + NAME_LOCALIZATION_MANAGER;
            public const string PATH_TABLE_MANAGER = TOOLS_PATH + NAME_TABLE_MANAGER;
            public const string PATH_SUFFIX_MANAGER = TOOLS_PATH + NAME_SUFFIX_MANAGER;

            public const string PATH_SETTINGS = TOOLS_PATH + NAME_PREFERENCES;
            public const string PATH_DOCUMENTATION = TOOLS_PATH + NAME_DOCUMENTATION;
            public const string PATH_SUPPORT = TOOLS_PATH + NAME_REPORT_ISSUES;

            public const int PRIORITY_LOCALIZATION_MANAGER = STARTING_TOOLS_MENU_PRIORITY;
            public const int PRIORITY_TABLE_MANAGER = PRIORITY_LOCALIZATION_MANAGER + NEXT_ROW;
            public const int PRIORITY_SUFFIX_MANAGER = PRIORITY_TABLE_MANAGER + NEXT_ROW;

            public const int PRIORITY_SETTINGS = PRIORITY_SUFFIX_MANAGER + ADD_SEPARATOR;
            public const int PRIORITY_DOCUMENTATION = PRIORITY_SETTINGS + NEXT_ROW;
            public const int PRIORITY_SUPPORT = PRIORITY_DOCUMENTATION + NEXT_ROW;

            public const string URL_DOCUMENTATION = "https://glitch9.gitbook.io/docs/unity-toolkits/smart-localization";
            //public const string URL_SUPPORT = "https://github.com/Glitch9Inc/Smart-Localization/issues";
            public const string PROVIDER_SETTINGS = ROOT_USER_PREFERENCE + NAME;
        }
        
        public static class CommitGen // Commit-Gen (Only has 1 tool)
        {
            public const int PRIORITY = STARTING_TOOLS_MENU_PRIORITY - ADD_SEPARATOR;
            public const string TOOL_PATH = ROOT_TOOL_PATH + "Commit Message Generator";
        }



        
        #region Extra Menu

        public static class Support
        {
            private const string PATH = ROOT_TOOL_PATH + "Support/";

            public const string PATH_REPORT_AN_ISSUE = PATH + NAME_REPORT_ISSUES;
            public const string PATH_RELOAD_SKINS = PATH + "Reload EditorGUI Skins";
            public const string PATH_RENAME_UI_PREFAB_RESOURCES = PATH + "Rename UIPrefabResource Files";
            public const string PATH_OPEN_PERSISTENT_DATA_PATH = PATH + "Open Persistent Data Path";

            public const int PRIORITY_REPORT_AN_ISSUE = STARTING_SUPPORT_MENU_PRIORITY;
            
            public const int PRIORITY_RELOAD_SKINS = PRIORITY_REPORT_AN_ISSUE + ADD_SEPARATOR;
            public const int PRIORITY_RENAME_UI_PREFAB_RESOURCES = PRIORITY_RELOAD_SKINS - 1;
            public const int PRIORITY_OPEN_PERSISTENT_DATA_PATH = PRIORITY_RENAME_UI_PREFAB_RESOURCES - 1;

            [MenuItem(PATH_REPORT_AN_ISSUE, priority = PRIORITY_REPORT_AN_ISSUE)]
            public static void OpenSupportURL()
            {
                Application.OpenURL(URL_SUPPORT_REPO);
            }

            [MenuItem(PATH_OPEN_PERSISTENT_DATA_PATH, priority = PRIORITY_OPEN_PERSISTENT_DATA_PATH)]
            public static void OpenPersistentDataPath()
            {
                EditorUtility.RevealInFinder(Application.persistentDataPath);
            }
        }

        public static class Utility
        {
            private const string NAME = "Utility";
            private const string TOOLS_PATH = ROOT_TOOL_PATH + NAME + "/";
            private const string CREATE_PATH = ROOT_CREATE_PATH + NAME + "/";

            // Scriptable Objects
            public const string PACKAGE_EXPORTER = "Package Exporter";
            public const string GIT_MODULE = "Git Module";

            public const string CREATE_PACKAGE_EXPORTER = CREATE_PATH + PACKAGE_EXPORTER;
            public const string CREATE_GIT_MODULE = CREATE_PATH + GIT_MODULE;

            public const int ORDER_PACKAGE_EXPORTER = STARTING_CREATE_MENU_ORDER + 2000;
            public const int ORDER_GIT_MODULE = ORDER_PACKAGE_EXPORTER + NEXT_ROW;
        }

        #endregion
        
        #region Not Available to Public yet

        public static class Game
        {
            private const string NAME = "Game";
            private const string TOOLS_PATH = ROOT_TOOL_PATH + NAME + "/";
            private const string CREATE_PATH = ROOT_CREATE_PATH + NAME + "/";

            public const string CREATE_GAME_SETTINGS = CREATE_PATH + "Game Settings";
            public const string CREATE_ITEM_SETTINGS = CREATE_PATH + "Item Settings";
            public const string CREATE_SEASON_PASS_SETTINGS = CREATE_PATH + "Season Pass Settings";

            public const int ORDER_CREATE_GAME_SETTINGS = STARTING_CREATE_MENU_ORDER;
            public const int ORDER_CREATE_ITEM_SETTINGS = ORDER_CREATE_GAME_SETTINGS + NEXT_ROW;
            public const int ORDER_CREATE_SEASON_PASS_SETTINGS = ORDER_CREATE_ITEM_SETTINGS + NEXT_ROW;
        }

        public static class GoogleSheets
        {
            private const string NAME = "Google Sheets";
            private const string TOOL_PATH = ROOT_TOOL_PATH + NAME + "/";
            private const string CREATE_PATH = ROOT_CREATE_PATH + NAME + "/";

            public const string NAME_GOOGLE_SHEETS_SETTINGS = "Google Sheets Settings";
            public const string CREATE_GOOGLE_SHEETS_SETTINGS = CREATE_PATH + NAME_GOOGLE_SHEETS_SETTINGS;
            public const int ORDER_GOOGLE_SHEETS_SETTINGS = STARTING_CREATE_MENU_ORDER;

            public const string NAME_GOOGLE_SHEETS_MANAGER = "Google Sheets Manager";
            public const string PATH_GOOGLE_SHEETS_MANAGER = TOOL_PATH + NAME_GOOGLE_SHEETS_MANAGER;
            public const int PRIORITY_GOOGLE_SHEETS_MANAGER = STARTING_TOOLS_MENU_PRIORITY;
        }
        
        public static class PlayHT
        {
            private const string NAME = "PlayHT";
            private const string TOOL_PATH = ROOT_TOOL_PATH + NAME + "/";
        }

        public static class AIDialogGenerator
        {
            private const string NAME = "AI Dialog Generator";
            private const string TOOL_PATH = ROOT_TOOL_PATH + NAME + "/";
            private const string CREATE_PATH = ROOT_CREATE_PATH + NAME + "/";

            public const string NAME_AI_DIALOG_SETTINGS = "AI Dialog Generator Settings";
            public const string CREATE_AI_DIALOG_SETTINGS = CREATE_PATH + NAME_AI_DIALOG_SETTINGS;
            public const int ORDER_AI_DIALOG_SETTINGS = STARTING_CREATE_MENU_ORDER;

            public const string NAME_AI_DIALOG_GENERATOR = "AI Dialog Generator";
            public const string PATH_AI_DIALOG_GENERATOR = TOOL_PATH + NAME_AI_DIALOG_GENERATOR;
            public const int PRIORITY_AI_DIALOG_GENERATOR = STARTING_TOOLS_MENU_PRIORITY;
        }
        
        #endregion
    }
}
#endif