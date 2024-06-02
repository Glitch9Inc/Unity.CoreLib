using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

namespace Glitch9.Internal
{
    [InitializeOnLoad] // Unity가 로드될 때마다 이 클래스를 초기화합니다.
    public class DefineSymbolManager
    {
        // Package Names
        private const string PACKAGE_NAME_ADDRESSABLES = "com.unity.addressables";
        private const string PACKAGE_NAME_TMPRO = "com.unity.textmeshpro";

        // Asset Folder Names
        private const string FOLDER_NAME_SERIALIZATION_SAVER = "SerializationSaver";
        private const string FOLDER_NAME_GLITCH9_LOCALIZATION = "SmartLocalization";

        // Symbols
        private const string SYMBOL_ADDRESSABLES = "UNITY_ADDRESSABLES";
        private const string SYMBOL_TMPRO = "UNITY_TMPRO";
        private const string SYMBOL_SERIALIZATION_SAVER = "GLITCH9_SERIALIZATION_SAVER";
        private const string SYMBOL_SMART_LOCALIZATION = "GLITCH9_SMART_LOCALIZATION";

        private static readonly ListRequest _listRequest;

        static DefineSymbolManager()
        {
            _listRequest = Client.List(true); // Include dependencies in the list
            EditorApplication.update += CheckPackageListCompletion;
        }

        private static void CheckPackageListCompletion()
        {
            if (!_listRequest.IsCompleted) return;

            EditorApplication.update -= CheckPackageListCompletion;

            if (_listRequest.Status == StatusCode.Success)
            {
                UpdateDefineSymbols(_listRequest.Result);
            }
            else
            {
                Debug.LogError("Failed to list packages.");
            }
        }

        private static void UpdateDefineSymbols(PackageCollection packages)
        {
            (string packageName, string symbol)[] packageSymbols = new[]
            {
                (PACKAGE_NAME_ADDRESSABLES, SYMBOL_ADDRESSABLES),
                (PACKAGE_NAME_TMPRO, SYMBOL_TMPRO),
            };

            (string folderName, string symbol)[] folderSymbols = new[]
            {
                (FOLDER_NAME_SERIALIZATION_SAVER, SYMBOL_SERIALIZATION_SAVER),
                (FOLDER_NAME_GLITCH9_LOCALIZATION, SYMBOL_SMART_LOCALIZATION),
            };
            
            foreach ((string packageName, string symbol) packageSymbol in packageSymbols)
            {
                bool packageExists = packages.Any(p => p.name == packageSymbol.packageName);
                UpdateDefineSymbol(packageSymbol.symbol, packageExists);
            }

            foreach ((string folderName, string symbol) folderSymbol in folderSymbols)
            {
                bool folderExists = AssetDatabase.FindAssets($"t:Folder {folderSymbol.folderName}").Length > 0;
                UpdateDefineSymbol(folderSymbol.symbol, folderExists);
            }
        }

        private static void UpdateDefineSymbol(string defineSymbol, bool addSymbol)
        {
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            List<string> allDefines = definesString.Split(';').ToList();

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