using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Glitch9.Internal
{

    [InitializeOnLoad] // Unity가 로드될 때마다 이 클래스를 초기화합니다.
    public class DefineSymbolManager
    {
        static DefineSymbolManager()
        {
            EditorApplication.update += OnEditorUpdate;
        }

        internal const string SYMBOL_PREFIX = "GLITCH9_";

        private static void OnEditorUpdate()
        {
            EditorApplication.update -= OnEditorUpdate;
            UpdateDefineSymbols();
        }

        private static List<string> GetAllDefines(BuildTargetGroup buildTargetGroup)
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            return definesString.Split(';').ToList();
        }
        private static List<string> GetAllGlitch9Symbols(List<string> allDefines)
        {

            List<string> glitch9Defines = allDefines.Where(define => define.StartsWith(SYMBOL_PREFIX)).ToList();
            return glitch9Defines;
        }
    
        private static void UpdateDefineSymbols()
        {
            PackageSettings[] packageSettings = AssetDatabase.FindAssets($"t:{nameof(PackageSettings)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<PackageSettings>)
                .ToArray();

            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            List<string> allDefines = GetAllDefines(buildTargetGroup);
            List<string> currentGlitch9Symbols = GetAllGlitch9Symbols(allDefines);
            HashSet<string> symbolsToAdd = new();

            foreach (PackageSettings packageSetting in packageSettings)
            {
                if (packageSetting == null) continue;
                if (!packageSetting.Define) continue;
                
                string symbol = packageSetting.DefineSymbol;
                if (!symbol.StartsWith(SYMBOL_PREFIX))
                {
                    UnityEngine.Debug.LogWarning("This symbol does not start with the prefix 'GLITCH9_'.");
                    continue;
                }

                symbolsToAdd.Add(symbol);
            }

            // 설정에 따라 심볼을 추가 또는 제거
            foreach (string symbol in currentGlitch9Symbols)
            {
                if (!symbolsToAdd.Contains(symbol))
                {
                    UpdateDefineSymbol(buildTargetGroup, allDefines, symbol, false); // 존재하지만 추가되지 않은 심볼은 제거
                }
            }

            foreach (string symbol in symbolsToAdd)
            {
                if (!currentGlitch9Symbols.Contains(symbol))
                {
                    UpdateDefineSymbol(buildTargetGroup, allDefines, symbol, true); // 설정 파일에 있는 심볼은 추가
                }
            }
        }

        private static void UpdateDefineSymbol(BuildTargetGroup buildTargetGroup, List<string> allDefines, string defineSymbol, bool addSymbol)
        {
            bool symbolExists = allDefines.Contains(defineSymbol);

            if (addSymbol && !symbolExists)
            {
                allDefines.Add(defineSymbol);
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, string.Join(";", allDefines));
            }
            else if (!addSymbol && symbolExists)
            {
                allDefines.Remove(defineSymbol);
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, string.Join(";", allDefines));
            }
        }
    }
}