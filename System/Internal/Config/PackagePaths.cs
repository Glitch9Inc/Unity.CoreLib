#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

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

    public class PackagePaths
    {
        private static class Strings
        {
            internal const string ASSETS = "Assets";
        }
        
        private static readonly Dictionary<Glitch9Library, string> _paths = new()
        {
            { Glitch9Library.Auth, "Glitch9/Auth" },
            { Glitch9Library.CoreLib, "Glitch9/CoreLib" },
            { Glitch9Library.CoreLibIO, "Glitch9/CoreLib.IO" },
            { Glitch9Library.CoreLibUI, "Glitch9/CoreLib.UI" },
            { Glitch9Library.SerializationSaver, "Glitch9/Toolkits/SerializationSaver" },
            { Glitch9Library.OpenAI, "Glitch9/Apis/OpenAI" },
            { Glitch9Library.CommitGet, "Glitch9/Toolkits/CommitGet" },
            { Glitch9Library.SmartLocalization, "Glitch9/SmartLocalization" },
            { Glitch9Library.PlayHT, "Glitch9/Apis/PlayHT" },
        };

        public static string GetAssetsPath(Glitch9Library library)
        {
            return $"{Strings.ASSETS}/{_paths.GetValueOrDefault(library)}";
        }

        public static string GetFullPath(Glitch9Library library)
        {
            return $"{Application.dataPath}/{_paths.GetValueOrDefault(library)}";
        }
    }
}
#endif