using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ScriptableObjects
{
    public static class AssetUtils
    {
        private const string ASSETS_RESOURCES = "Assets/Resources";

#if UNITY_EDITOR
        private static string FixPathSlashes(string path)
        {
            return path.Replace('\\', '/');
        }

        private static T CreateAsset<T>(string fileName, T obj, string customPathWithFileName) where T : ScriptableObject
        {
            string path;

            if (string.IsNullOrEmpty(customPathWithFileName))
            {
                path = $"Assets/{fileName}.asset";
            }
            else
            {
                if (Path.HasExtension(customPathWithFileName)) // if path contains extension, remove it
                {
                    path = $"Assets/{customPathWithFileName}";
                }
                else
                {
                    path = $"Assets/{customPathWithFileName}/{fileName}.asset";
                }
            }

            path = path.Replace("Assets/Assets/", "Assets/");

            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir) && dir != null) Directory.CreateDirectory(dir);
            AssetDatabase.CreateAsset(obj, path);
            EditorUtility.SetDirty(obj);
            AssetDatabase.Refresh();
            return obj;
        }

        public static T LoadAsset<T>(string customPathWithFileName, bool create = true) where T : ScriptableObject
        {
            string name = typeof(T).Name;

            string[] guids = AssetDatabase.FindAssets($"t:{name}");
            if (guids.Length == 0)
            {
                if (!create) return null;
                Debug.LogWarning($"{name} does not exist in the project. Creating a new one...");
                return CreateAsset(name, ScriptableObject.CreateInstance<T>(), customPathWithFileName);
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            Debug.Log($"Loading {name} from {path}");
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        public static T[] LoadAssets<T>(string assetDirectoryPath) where T : ScriptableObject
        {
            // filename can be anything. load all.
            assetDirectoryPath = FixPathSlashes(assetDirectoryPath);
            if (!assetDirectoryPath.EndsWith("/")) assetDirectoryPath += "/";
            Debug.Log($"Loading assets of type {typeof(T).Name} from {assetDirectoryPath}");
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { assetDirectoryPath });
            if (guids.Length == 0)
            {
                Debug.LogWarning($"No assets of type {typeof(T).Name} found in {assetDirectoryPath}");
                return null;
            }
            else
            {
                Debug.Log($"Found {guids.Length} assets of type {typeof(T).Name} in {assetDirectoryPath}");
            }
            T[] assets = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                if (path == null)
                {
                    Debug.LogError($"Failed to load asset at {guids[i]}");
                    continue;
                }
                assets[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }
            return assets;
        }

        public static Texture2D LoadTexture(string assetName, ref Dictionary<string, Texture2D> cache)
        {
            if (cache.TryGetValue(assetName, out Texture2D icon)) return icon;
            string[] guids = AssetDatabase.FindAssets($"{assetName} t:texture2D", null);
            if (guids.Length == 0) return null;
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            if (path == null) return null;
            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            cache.Add(assetName, tex);
            return tex;
        }

        public static Texture2D LoadLightOrDarkTexture(string assetName, ref Dictionary<string, Texture2D> cache)
        {
            if (EditorGUIUtility.isProSkin) assetName = $"d_{assetName}";
            if (cache.TryGetValue(assetName, out Texture2D icon)) return icon;
            string[] guids = AssetDatabase.FindAssets($"{assetName} t:texture2D", null);
            if (guids.Length == 0) return null;
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            if (path == null) return null;
            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            cache.Add(assetName, tex);
            return tex;
        }

        public static void PingScriptFile(Type type)
        {
            // Find all MonoScript objects in the project
            string[] guids = AssetDatabase.FindAssets("t:MonoScript");
            Debug.Log($"Found {guids.Length} MonoScripts in the project.");

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);

                if (script != null && script.GetClass() == type)
                {
                    Debug.Log($"Found {type.Name} script at {assetPath}");
                    // Ping the script in the Unity Editor
                    EditorGUIUtility.PingObject(script);
                    Selection.activeObject = script;
                    break;
                }
            }
        }

        public static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
            }
        }

        public static TScriptableObject CreateScriptableObject<TScriptableObject>(string fileNameWithoutExt, string targetPath) where TScriptableObject : ScriptableObject
        {
            TScriptableObject res = Resources.Load(fileNameWithoutExt) as TScriptableObject;
            if (res != null)
            {
                Debug.LogError($"{typeof(TScriptableObject).Name} already exists. {res.name}");
                return res;
            }

            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            // check if the file already exists, if it does, it means the file is broken somehow.
            // create a backup by changing the filename to {filename}_{bk}.asset

            string filePath = $"{targetPath}/{fileNameWithoutExt}.asset";
            if (File.Exists(filePath))
            {
                string backupPath = $"{targetPath}/{fileNameWithoutExt}_bk.asset";
                File.Move(filePath, backupPath);
                Debug.LogError($"{typeof(TScriptableObject).Name} already exists, but there were issues loading the file. Backup created at {backupPath} and new file will be created.");
            }


            TScriptableObject obj = ScriptableObject.CreateInstance<TScriptableObject>();
            AssetDatabase.CreateAsset(obj, filePath);
            EditorUtility.SetDirty(obj);
            return obj;
        }

        public static TScriptableObject CreateResourcesAsset<TScriptableObject>(string objectName = null, string customPath = null) where TScriptableObject : ScriptableObject
        {
            if (string.IsNullOrEmpty(objectName)) objectName = typeof(TScriptableObject).Name;
            TScriptableObject asset = ScriptableObject.CreateInstance<TScriptableObject>();
            string resourcesPath = customPath ?? ASSETS_RESOURCES;
            AssetDatabase.CreateAsset(asset, $"{resourcesPath}/{objectName}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return asset;
        }
#endif

        public static T LoadResource<T>(bool create = false) where T : ScriptableObject
        {
            return LoadResource<T>(null, create);
        }


        public static T LoadResource<T>(string customPathWithFileName, bool create = false) where T : ScriptableObject
        {
            string assetPath = string.IsNullOrEmpty(customPathWithFileName) ? ASSETS_RESOURCES : customPathWithFileName;

#if UNITY_EDITOR
            return LoadAsset<T>(assetPath, create);
#else
            return LoadResourceInternal<T>(customPathWithFileName);
#endif
        }

        public static T[] LoadAllResources<T>(string customResourcesPath, string subDirectory) where T : ScriptableObject
        {
#if UNITY_EDITOR
            return LoadAssets<T>($"{customResourcesPath}/{subDirectory}");
#else
            return LoadResourcesInternal<T>(subDirectory);
#endif
        }

        private static T LoadResourceInternal<T>(string path) where T : ScriptableObject
        {
            if (string.IsNullOrEmpty(path))
            {
                return Resources.Load<T>(typeof(T).Name);
            }
            
            if (Path.HasExtension(path)) // if path contains extension, remove it
            {
                string fileName = Path.GetFileNameWithoutExtension(path);
                string dir = Path.GetDirectoryName(path);
                path = $"{dir}/{fileName}";
            }

            T asset = Resources.Load<T>(path);
            if (asset == null) Debug.LogWarning($"{path} does not exist in the Resources folder.");
            return asset;
        }

        private static T[] LoadResourcesInternal<T>(string subDirectory) where T : ScriptableObject
        {
            T[] assets = Resources.LoadAll<T>(subDirectory);
            if (assets.Length == 0) Debug.LogWarning($"{subDirectory} does not exist in the Resources folder.");
            return assets;
        }

    }
}
