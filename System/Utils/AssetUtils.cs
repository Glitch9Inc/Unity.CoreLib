#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Glitch9
{
    public static class AssetUtils
    {
        private static T Create<T>(string name, T obj) where T : ScriptableObject
        {
            string path = $"Assets/{name}.asset";
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir) && dir != null) Directory.CreateDirectory(dir);
            AssetDatabase.CreateAsset(obj, path);
            EditorUtility.SetDirty(obj);
            return obj;
        }

        public static T Load<T>(bool create = true) where T : ScriptableObject
        {
            string name = typeof(T).Name;
            string[] guids = AssetDatabase.FindAssets($"t:{name}");
            if (guids.Length == 0)
            {
                if (!create) return null;
                Debug.LogWarning($"{name} does not exist in the project. Creating a new one...");
                return Create(name, ScriptableObject.CreateInstance<T>());
            }
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<T>(path);
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

        public static void AssureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
            }
        }
    }
}
#endif