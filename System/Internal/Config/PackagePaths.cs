
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
        AIDevKit,
        AIDevKit_OpenAI,
        AIDevKit_PlayHT,
        AIDevKit_Pro,
        AIDevKit_CodeGen,
        AIDevKit_CommitGet,
        SmartLocalization,
    }

    public class PackagePaths
    {
        private static class Strings
        {
            internal const string ASSETS = "Assets";
        }

        private static readonly Dictionary<Glitch9Library, string[]> _dirs = new()
        {
            { Glitch9Library.Auth, new []{"Glitch9/Auth"}},
            { Glitch9Library.CoreLib, new []{"Glitch9/CoreLib" }},
            { Glitch9Library.CoreLibIO, new []{"Glitch9/CoreLib.IO"}},
            { Glitch9Library.CoreLibUI, new []{"Glitch9/CoreLib.UI"}},
            { Glitch9Library.SerializationSaver, new []{"Glitch9/SerializationSaver"}},
            { Glitch9Library.AIDevKit, new []
            {
                "Glitch9/AIDevKit/Core/Common",
                "Glitch9/AIDevKit/Core/OpenAI",
                "Glitch9/AIDevKit/Core/Google",
                "Glitch9/AIDevKit/Resources",
                "Glitch9/AIDevKit/Documentation",
            }},
            { Glitch9Library.AIDevKit_OpenAI, new []{"Glitch9/AIDevKit/Core/OpenAI"}},
            { Glitch9Library.AIDevKit_PlayHT,new []{ "Glitch9/AIDevKit/Core/PlayHT"}},
            { Glitch9Library.AIDevKit_Pro, new []
            {
                "Glitch9/AIDevKit/Pro",
                "Glitch9/AIDevKit/Demos",
            }},
            { Glitch9Library.AIDevKit_CodeGen, new []{"Glitch9/AIDevKit/Tools/CodeGen"}},
            { Glitch9Library.AIDevKit_CommitGet, new []{"Glitch9/AIDevKit/Tools/CommitGet"}},
            { Glitch9Library.SmartLocalization, new []{"Glitch9/SmartLocalization"}},
        };

        public static string[] GetAssetDirs(Glitch9Library library)
        {
            // return $"{Strings.ASSETS}/{_dirs.GetValueOrDefault(library)}";
            List<string> dirs = new();
            foreach (string dir in _dirs[library])
            {
                dirs.Add($"{Strings.ASSETS}/{dir}");
            }
            return dirs.ToArray();
        }
    }
}
