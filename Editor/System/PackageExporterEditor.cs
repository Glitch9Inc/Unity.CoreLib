using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Glitch9
{
    [CustomEditor(typeof(PackageExporter))]
    public class PackageExporterEditor : Editor
    {
        private const string FILE_NAME_FORMAT = "{0}_{1}.unitypackage"; // 0 is the package name, 1 is the version number
        private const string ASSET_PATHS_HELP = "Asset Paths must start with 'Assets/' and can contain wildcards (*).";

        private SerializedProperty packageName;
        private SerializedProperty buildPath;
        private SerializedProperty versionNumber;
        private SerializedProperty assetPaths;

        private void OnEnable()
        {
            packageName = serializedObject.FindProperty(nameof(packageName));
            buildPath = serializedObject.FindProperty(nameof(buildPath));
            versionNumber = serializedObject.FindProperty(nameof(versionNumber));
            assetPaths = serializedObject.FindProperty(nameof(assetPaths));
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

            EditorGUILayout.PropertyField(versionNumber);

            EditorGUILayout.PropertyField(assetPaths, true);
            EditorGUILayout.HelpBox(ASSET_PATHS_HELP, MessageType.Info);

            EditorGUILayout.Space();

            if (GUILayout.Button("Export Package", GUILayout.Height(30)))
            {
                Export();
            }

            if (GUILayout.Button("Export Package (+1 Version)", GUILayout.Height(30)))
            {
                versionNumber.intValue++;
                Export();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void Export()
        {
            List<string> list = new();

            for (int i = 0; i < this.assetPaths.arraySize; i++)
            {
                list.Add(this.assetPaths.GetArrayElementAtIndex(i).stringValue);
            }

            int newVersionNumber = ExportPackage(packageName.stringValue, versionNumber.intValue, buildPath.stringValue, list);
            if (newVersionNumber != -1) versionNumber.intValue = newVersionNumber;
        }


        private static int ExportPackage(string packageName, int versionNumber, string buildPath, List<string> assetPaths)
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
            System.Collections.Generic.List<string> assetList = new();
            foreach (string path in allAssetPaths)
            {
                if (Contains(path, assetPaths))
                {
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

        private static bool Contains(string path, List<string> assetPaths)
        {
            foreach (string assetPath in assetPaths)
            {
                if (path.StartsWith(assetPath))
                {
                    return true;
                }
            }

            return false;
        }
    }
}