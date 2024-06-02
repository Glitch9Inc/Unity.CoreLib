namespace Glitch9.Internal
{
    public class ToolMenu
    {
        private const string ROOT_PATH = "Tools/Glitch9/";

        public static class Utils
        {
            private const string PATH = ROOT_PATH + "Utils/";

            public const string PATH_RELOAD_SKINS = PATH + "Reload EditorGUI Skins";
            public const int PRIORITY_RELOAD_SKINS = -1000;
        }

        // AI Development Kit
        public static class OpenAI
        {
            //public const string NAME = "OpenAI";
            private const string PATH = ROOT_PATH + "OpenAI/";

            public const string NAME_COMPLETION = "Editor Completion";
            public const string NAME_CHAT_GPT = "Editor ChatGPT";
            public const string NAME_DALLE = "Editor DALL·E";
            public const string NAME_SPEECH = "Editor Speech";
            public const string NAME_LOG_MANAGER = "Log Manager";
            public const string NAME_AI_MODEL_MANAGER = "AI Model Manager";
            public const string NAME_ASSISTANT_MANAGER = "Assistant Manager";

            public const string PATH_COMPLETION = PATH + NAME_COMPLETION;
            public const string PATH_CHAT_GPT = PATH + NAME_CHAT_GPT;
            public const string PATH_DALLE = PATH + NAME_DALLE;
            public const string PATH_SPEECH = PATH + NAME_SPEECH;
            public const string PATH_LOG_MANAGER = PATH + NAME_LOG_MANAGER;
            public const string PATH_AI_MODEL_MANAGER = PATH + NAME_AI_MODEL_MANAGER;
            public const string PATH_ASSISTANT_MANAGER = PATH + NAME_ASSISTANT_MANAGER;

            public const int PRIORITY_COMPLETION = -5000;
            public const int PRIORITY_CHAT_GPT = -4999;
            public const int PRIORITY_DALLE = -4998;
            public const int PRIORITY_SPEECH = -4997;

            public const int PRIORITY_LOG_MANAGER = -4900;
            public const int PRIORITY_AI_MODEL_MANAGER = -4899;
            public const int PRIORITY_ASSISTANT_MANAGER = -4898;
        }

        // Smart Localization
        public static class SmartLocalization
        {
            private const string PATH = ROOT_PATH + "Smart Localization/";

            public const string NAME_EDITOR = "Smart Localization";
            public const string NAME_TABLE_MANAGER = "Localization Table Manager";
            public const string NAME_SUFFIX_MANAGER = "Suffix Manager";

            public const string PATH_EDITOR = PATH + NAME_EDITOR;
            public const string PATH_TABLE_MANAGER = PATH + NAME_TABLE_MANAGER;
            public const string PATH_SUFFIX_MANAGER = PATH + NAME_SUFFIX_MANAGER;

            public const int PRIORITY_EDITOR = -4500;
            public const int PRIORITY_TABLE_MANAGER = -4499;
            public const int PRIORITY_SUFFIX_MANAGER = -4498;
        }

        // Commit-Gen (Only has 1 tool)
        public static class CommitGen
        {
            public const string PATH = ROOT_PATH + "Commit Message Generator";
            public const int PRIORITY = -4000;
        }
    }
}