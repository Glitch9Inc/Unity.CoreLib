namespace Glitch9.Internal
{
    public class UnityMenu
    {
        private const string ROOT_TOOL_PATH = "Tools/Glitch9/";
        private const string USER_PREFERENCE = "Preferences/Glitch9/";

        private const string NAME_SETTINGS = "Settings";
        private const string NAME_DOCUMENTATION = "Documentation";
        private const string NAME_SUPPORT = "Support";

        public static class Utils
        {
            private const string PATH = ROOT_TOOL_PATH + "Utils/";

            public const string PATH_RELOAD_SKINS = PATH + "Reload EditorGUI Skins";
            public const int PRIORITY_RELOAD_SKINS = -1000;
        }

        // AI Development Kit
        public static class OpenAI
        {
            private const string NAME = "OpenAI";
            private const string TOOL_PATH = ROOT_TOOL_PATH + NAME + "/";

            public const string NAME_COMPLETION = "Editor Completion";
            public const string NAME_CHAT_GPT = "Editor ChatGPT";
            public const string NAME_DALLE = "Editor DALL·E";
            public const string NAME_SPEECH = "Editor Speech";
            public const string NAME_LOG_MANAGER = "Log Manager";
            public const string NAME_AI_MODEL_MANAGER = "AI Model Manager";
            public const string NAME_ASSISTANT_MANAGER = "Assistant Manager";

            public const string PATH_COMPLETION = TOOL_PATH + NAME_COMPLETION;
            public const string PATH_CHAT_GPT = TOOL_PATH + NAME_CHAT_GPT;
            public const string PATH_DALLE = TOOL_PATH + NAME_DALLE;
            public const string PATH_SPEECH = TOOL_PATH + NAME_SPEECH;
            public const string PATH_LOG_MANAGER = TOOL_PATH + NAME_LOG_MANAGER;
            public const string PATH_AI_MODEL_MANAGER = TOOL_PATH + NAME_AI_MODEL_MANAGER;
            public const string PATH_ASSISTANT_MANAGER = TOOL_PATH + NAME_ASSISTANT_MANAGER;
            public const string PATH_SETTINGS = TOOL_PATH + NAME_SETTINGS;
            public const string PATH_DOCUMENTATION = TOOL_PATH + NAME_DOCUMENTATION;
            public const string PATH_SUPPORT = TOOL_PATH + NAME_SUPPORT;

            public const int PRIORITY_COMPLETION = -5000;
            public const int PRIORITY_CHAT_GPT = -4999;
            public const int PRIORITY_DALLE = -4998;
            public const int PRIORITY_SPEECH = -4997;

            public const int PRIORITY_LOG_MANAGER = -4900;
            public const int PRIORITY_AI_MODEL_MANAGER = -4899;
            public const int PRIORITY_ASSISTANT_MANAGER = -4898;

            public const int PRIORITY_SETTINGS = -4800;
            public const int PRIORITY_DOCUMENTATION = -4799;
            public const int PRIORITY_SUPPORT = -4798;

            public const string URL_DOCUMENTATION = "https://glitch9.gitbook.io/docs/ai-development-kit/getting-started";
            public const string URL_SUPPORT = "https://github.com/Glitch9Inc/AI-Development-Kit/issues";
            public const string PROVIDER_SETTINGS = USER_PREFERENCE + NAME;
        }

        // Smart Localization
        public static class SmartLocalization
        {
            private const string NAME = "Smart Localization";
            private const string TOOL_PATH = ROOT_TOOL_PATH + NAME + "/";

            public const string NAME_EDITOR = "Smart Localization";
            public const string NAME_TABLE_MANAGER = "Localization Table Manager";
            public const string NAME_SUFFIX_MANAGER = "Suffix Manager";

            public const string PATH_EDITOR = TOOL_PATH + NAME_EDITOR;
            public const string PATH_TABLE_MANAGER = TOOL_PATH + NAME_TABLE_MANAGER;
            public const string PATH_SUFFIX_MANAGER = TOOL_PATH + NAME_SUFFIX_MANAGER;
            public const string PATH_SETTINGS = TOOL_PATH + NAME_SETTINGS;
            public const string PATH_DOCUMENTATION = TOOL_PATH + NAME_DOCUMENTATION;
            public const string PATH_SUPPORT = TOOL_PATH + NAME_SUPPORT;

            public const int PRIORITY_EDITOR = -4500;
            public const int PRIORITY_TABLE_MANAGER = -4499;
            public const int PRIORITY_SUFFIX_MANAGER = -4498;

            public const int PRIORITY_SETTINGS = -4400;
            public const int PRIORITY_DOCUMENTATION = -4399;
            public const int PRIORITY_SUPPORT = -4398;

            public const string URL_DOCUMENTATION = "https://glitch9.gitbook.io/docs/unity-toolkits/smart-localization";
            public const string URL_SUPPORT = "https://github.com/Glitch9Inc/Smart-Localization/issues";
            public const string PROVIDER_SETTINGS = USER_PREFERENCE + NAME;
        }

        // Commit-Gen (Only has 1 tool)
        public static class CommitGen
        {
            public const string TOOL_PATH = ROOT_TOOL_PATH + "Commit Message Generator";
            public const int PRIORITY = -4000;
        }
    }
}