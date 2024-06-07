#if UNITY_EDITOR
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
        private const int SPACE_BETWEEN_MODULES = 5;
        private const int ADD_SEPARATOR = 15;
        private const int NEXT_ROW = 1;

        public const string URL_SUPPORT_REPO = "https://github.com/Glitch9Inc/Glitch9-Support";

        public static class Utils
        {
            private const string PATH = ROOT_TOOL_PATH + "Tools/";

            public const string PATH_RELOAD_SKINS = PATH + "Reload EditorGUI Skins";
            public const string PATH_RENAME_UI_PREFAB_RESOURCES = PATH + "Rename UIPrefabResource Files";

            public const int PRIORITY_RELOAD_SKINS = -1000;
            public const int PRIORITY_RENAME_UI_PREFAB_RESOURCES = PRIORITY_RELOAD_SKINS - 1;
        }

        // AI Development Kit
        public static class OpenAI
        {
            private const string NAME = "OpenAI";
            private const string TOOLS_PATH = ROOT_TOOL_PATH + NAME + "/";
            private const string CREATE_PATH = ROOT_CREATE_PATH + NAME + "/";

            // Scriptable Objects
            public const string NAME_SETTINGS = "Settings";
            public const string NAME_LOGS = "Log Container";
            public const string NAME_AI_MODEL_METADATA = "AI Model Metadata";

            public const string NAME_CHAT_STREAMER = "Chat Streamer";
            public const string NAME_IMAGE_GENERATOR = "Image Generator";
            public const string NAME_VOICE_GENERATOR = "Voice Generator";
            public const string NAME_VOICE_TRANSCRIBER = "Voice Transcriber";

            public const string CREATE_SETTINGS = CREATE_PATH + NAME_SETTINGS;
            public const string CREATE_LOGS = CREATE_PATH + NAME_LOGS;
            public const string CREATE_AI_MODEL_METADATA = CREATE_PATH + NAME_AI_MODEL_METADATA;

            public const string CREATE_CHAT_STREAMER = CREATE_PATH + NAME_CHAT_STREAMER;
            public const string CREATE_IMAGE_GENERATOR = CREATE_PATH + NAME_IMAGE_GENERATOR;
            public const string CREATE_VOICE_GENERATOR = CREATE_PATH + NAME_VOICE_GENERATOR;
            public const string CREATE_VOICE_TRANSCRIBER = CREATE_PATH + NAME_VOICE_TRANSCRIBER;

            public const int ORDER_SETTINGS = STARTING_CREATE_MENU_ORDER;
            public const int ORDER_LOGS = ORDER_SETTINGS + NEXT_ROW;
            public const int ORDER_AI_MODEL_METADATA = ORDER_LOGS + NEXT_ROW;

            public const int ORDER_CHAT_STREAMER = ORDER_AI_MODEL_METADATA + ADD_SEPARATOR;
            public const int ORDER_IMAGE_GENERATOR = ORDER_CHAT_STREAMER + NEXT_ROW;
            public const int ORDER_VOICE_GENERATOR = ORDER_IMAGE_GENERATOR + NEXT_ROW;
            public const int ORDER_VOICE_TRANSCRIBER = ORDER_VOICE_GENERATOR + NEXT_ROW;

            // Menu
            public const string NAME_COMPLETION = "Editor Completion";
            public const string NAME_CHAT_GPT = "Editor ChatGPT";
            public const string NAME_DALLE = "Editor DALL·E";
            public const string NAME_SPEECH = "Editor Speech";
            
            public const string NAME_LOG_MANAGER = "Log Manager";
            public const string NAME_AI_MODEL_MANAGER = "AI Model Manager";
            public const string NAME_ASSISTANT_MANAGER = "Assistant Manager";
            

            public const string PATH_COMPLETION = TOOLS_PATH + NAME_COMPLETION;
            public const string PATH_CHAT_GPT = TOOLS_PATH + NAME_CHAT_GPT;
            public const string PATH_DALLE = TOOLS_PATH + NAME_DALLE;
            public const string PATH_SPEECH = TOOLS_PATH + NAME_SPEECH;
            
            public const string PATH_LOG_MANAGER = TOOLS_PATH + NAME_LOG_MANAGER;
            public const string PATH_AI_MODEL_MANAGER = TOOLS_PATH + NAME_AI_MODEL_MANAGER;
            public const string PATH_ASSISTANT_MANAGER = TOOLS_PATH + NAME_ASSISTANT_MANAGER;
            
            public const string PATH_SETTINGS = TOOLS_PATH + NAME_PREFERENCES;
            public const string PATH_DOCUMENTATION = TOOLS_PATH + NAME_DOCUMENTATION;
            public const string PATH_SUPPORT = TOOLS_PATH + NAME_REPORT_ISSUES;
            

            public const int PRIORITY_COMPLETION = STARTING_TOOLS_MENU_PRIORITY;
            public const int PRIORITY_CHAT_GPT = PRIORITY_COMPLETION + NEXT_ROW;
            public const int PRIORITY_DALLE = PRIORITY_CHAT_GPT + NEXT_ROW;
            public const int PRIORITY_SPEECH = PRIORITY_DALLE + NEXT_ROW;

            public const int PRIORITY_LOG_MANAGER = PRIORITY_SPEECH + ADD_SEPARATOR;
            public const int PRIORITY_AI_MODEL_MANAGER = PRIORITY_LOG_MANAGER + NEXT_ROW;
            public const int PRIORITY_ASSISTANT_MANAGER = PRIORITY_AI_MODEL_MANAGER + NEXT_ROW;

            public const int PRIORITY_SETTINGS = PRIORITY_ASSISTANT_MANAGER + ADD_SEPARATOR;
            public const int PRIORITY_DOCUMENTATION = PRIORITY_SETTINGS + NEXT_ROW;
            public const int PRIORITY_SUPPORT = PRIORITY_DOCUMENTATION + NEXT_ROW;

            public const string URL_DOCUMENTATION = "https://glitch9.gitbook.io/docs/ai-development-kit/getting-started";
            public const string URL_SUPPORT = "https://github.com/Glitch9Inc/AI-Development-Kit/issues";
            public const string PROVIDER_SETTINGS = ROOT_USER_PREFERENCE + NAME;
        }

        // Smart Localization
        public static class SmartLocalization
        {
            private const string NAME = "Smart Localization";
            private const string TOOLS_PATH = ROOT_TOOL_PATH + NAME + "/";

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
            public const string URL_SUPPORT = "https://github.com/Glitch9Inc/Smart-Localization/issues";
            public const string PROVIDER_SETTINGS = ROOT_USER_PREFERENCE + NAME;
        }

        // PlayHT
        public static class PlayHT
        {
            private const string NAME = "PlayHT";
            private const string TOOL_PATH = ROOT_TOOL_PATH + NAME + "/";


        }

        // Commit-Gen (Only has 1 tool)
        public static class CommitGen
        {
            public const int PRIORITY = STARTING_TOOLS_MENU_PRIORITY - ADD_SEPARATOR;
            public const string TOOL_PATH = ROOT_TOOL_PATH + "Commit Message Generator";
        }
    }
}
#endif