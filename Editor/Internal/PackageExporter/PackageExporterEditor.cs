using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Glitch9.Internal
{
    [CustomEditor(typeof(PackageExporter))]
    public class PackageExporterEditor : Editor
    {
        private const string FILE_NAME_FORMAT = "{0}_{1}.unitypackage"; // 0 is the package name, 1 is the version number
        private const string ASSET_PATHS_HELP = "Asset Paths must start with 'Assets/' and can contain wildcards (*).";
        private static readonly string[] _forbiddenWords = new string[]
        {
            "Sirenix",
            "HttpUtility",
        };

        private PackageExporter _target;
        private SerializedProperty packageName;
        private SerializedProperty buildPath;
        private SerializedProperty version;
        private SerializedProperty libraries;


        private void OnEnable()
        {
            _target = (PackageExporter)target;
            packageName = serializedObject.FindProperty(nameof(packageName));
            buildPath = serializedObject.FindProperty(nameof(buildPath));
            version = serializedObject.FindProperty(nameof(version));
            libraries = serializedObject.FindProperty(nameof(libraries));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(packageName);

            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(buildPath);
                if (GUILayout.Button("...", GUILayout.Width(30)))
                {
                    string newBuildPath = EditorUtility.OpenFolderPanel("Select Build Path", buildPath.stringValue, "");
                    if (!string.IsNullOrEmpty(newBuildPath)) buildPath.stringValue = newBuildPath;
                }
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(version);
            EditorGUILayout.PropertyField(libraries, true);
            EditorGUILayout.HelpBox(ASSET_PATHS_HELP, MessageType.Info);

            EditorGUILayout.Space();

            if (GUILayout.Button("Export Package", GUILayout.Height(30)))
            {
                ExportPackage();
            }

            if (GUILayout.Button("Export Package (Version +)", GUILayout.Height(30)))
            {
                _target.IncreasePatchVersion();
                ExportPackage();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void ExportPackage()
        {
            List<string> list = new();

            for (int i = 0; i < this.libraries.arraySize; i++)
            {
                int enumIndex = libraries.GetArrayElementAtIndex(i).enumValueIndex;
                string path = PackagePaths.GetAssetsPath((Glitch9Library)enumIndex);
                if (string.IsNullOrEmpty(path)) continue;
                list.Add(path);
            }

            int newBuildNum = BuildPackage(packageName.stringValue, _target.Version.Build, buildPath.stringValue, list);
            if (newBuildNum != -1) _target.SetBuildNumber(newBuildNum);
        }


        private static int BuildPackage(string packageName, int versionNumber, string buildPath, List<string> assetPaths)
        {
            if (string.IsNullOrEmpty(packageName))
            {
                Debug.LogError("No package name specified.");
                return -1;
            }

            if (string.IsNullOrEmpty(buildPath))
            {
                Debug.LogError("No path specified for saving the package.");
                return -1;
            }

            if (assetPaths == null || assetPaths.Count == 0)
            {
                Debug.LogError("No asset paths specified for export.");
                return -1;
            }

            // 폴더 내의 모든 에셋 가져오기
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
            List<string> assetList = new();
            foreach (string path in allAssetPaths)
            {
                if (IsSamePath(path, assetPaths))
                {
                    if (ValidateDirectory(path))
                    {
                        Debug.LogError($"Cannot export script: {path}");
                        return -1;
                    }
                    assetList.Add(path);
                }
            }

            if (assetList.Count == 0)
            {
                Debug.LogError("No assets found in the specified folder.");
                return -1;
            }

            // 패키지로 만들 에셋 배열
            string[] assetsToPackage = assetList.ToArray();

            packageName = packageName.Replace(" ", "_").ToLower();

            // 에셋 패키지 저장 경로 및 파일 이름 지정
            string fileName = string.Format(FILE_NAME_FORMAT, packageName, versionNumber);
            // string filePath = EditorUtility.SaveFilePanel("Save Unity Package", "", fileName, "unitypackage");
            // don't do EditorUtility.SaveFilePanel it's tedious.

            string filePath = Path.Combine(buildPath, fileName);

            if (string.IsNullOrEmpty(filePath))
            {
                Debug.LogError("No path selected for saving the package.");
                return -1;
            }

            // check if the file already exists, if it does, increase the version number
            while (File.Exists(filePath))
            {
                versionNumber++;
                fileName = string.Format(FILE_NAME_FORMAT, packageName, versionNumber);
                filePath = Path.Combine(buildPath, fileName);
            }

            // 에셋 패키지 만들기
            AssetDatabase.ExportPackage(assetsToPackage, filePath, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);
            Debug.Log("Package exported successfully to " + filePath);
            return versionNumber;
        }


        private static bool ValidateDirectory(string scriptPath)
        {
            // check if the script has "Sirenix" in it
            scriptPath = scriptPath.Replace("Assets/", "");
            string dir = Path.Combine(Application.dataPath, scriptPath);

            // get all script files from the directory
            string[] files = Directory.GetFiles(dir, "*.cs", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                if (file.Contains(nameof(PackageExporterEditor))) continue;
                string scriptText = File.ReadAllText(file);

                foreach (string forbiddenWord in _forbiddenWords)
                {
                    if (scriptText.Contains(forbiddenWord))
                    {
                        Debug.LogError($"<color=blue>{file}</color> contains forbidden word: {forbiddenWord}. Cannot export.");
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IsSamePath(string path, List<string> assetPaths)
        {
            foreach (string assetPath in assetPaths)
            {
                if (path.Trim() == assetPath.Trim())
                {
                    return true;
                }
            }

            return false;
        }
    }
}