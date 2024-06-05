using System.Collections.Generic;

namespace Glitch9.Internal
{
    public enum Glitch9Library
    {
        Auth,
        CoreLib,
        CoreLibIO,
        CoreLibUI,
        SerializationSaver,
        OpenAI,
        CommitGet,
        SmartLocalization,
        PlayHT,
    }

    public class LibraryPaths
    {
        private static readonly Dictionary<Glitch9Library, string> _paths = new()
        {
            { Glitch9Library.Auth, "Assets/Glitch9/Auth" },
            { Glitch9Library.CoreLib, "Assets/Glitch9/CoreLib" },
            { Glitch9Library.CoreLibIO, "Assets/Glitch9/CoreLib.IO" },
            { Glitch9Library.CoreLibUI, "Assets/Glitch9/CoreLib.UI" },
            { Glitch9Library.SerializationSaver, "Assets/Glitch9/Toolkits/SerializationSaver" },
            { Glitch9Library.OpenAI, "Assets/Glitch9/Apis/OpenAI" },
            { Glitch9Library.CommitGet, "Assets/Glitch9/Toolkits/CommitGet" },
            { Glitch9Library.SmartLocalization, "Assets/Glitch9/Toolkits/SmartLocalization" },
            { Glitch9Library.PlayHT, "Assets/Glitch9/Apis/PlayHT" },
        };

        public static string GetPath(Glitch9Library library)
        {
            return _paths.GetValueOrDefault(library);
        }
    }
}