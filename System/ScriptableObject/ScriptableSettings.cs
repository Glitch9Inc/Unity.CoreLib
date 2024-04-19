using System.IO;
using UnityEngine;

namespace Glitch9
{
    public abstract class ScriptableSettings<TSelf> : ScriptableObject where TSelf : ScriptableSettings<TSelf>
    {
        protected static string AssetPath => $"Resources/Settings/{typeof(TSelf).Name}";

        private static TSelf _instance;

        public static TSelf Instance
        {
            get
            {
                if (_instance == null)
                {
                    TSelf res = Resources.Load<TSelf>($"Settings/{typeof(TSelf).Name}");

                    if (res == null)
                    {
                        Debug.LogWarning($"Settings/{typeof(TSelf).Name} is not found. Creating a new file...");
                        res = CreateInstance<TSelf>();
                        Create(res);
                    }

                    _instance = res;
                }

                return _instance;
            }
        }

        public static TSelf Create(TSelf obj)
        {
#if UNITY_EDITOR
            string path = AssetPath + ".asset";
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            UnityEditor.AssetDatabase.CreateAsset(obj, path);
            UnityEditor.EditorUtility.SetDirty(obj);
#endif
            return obj;
        }

        public static void Save()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(Instance);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }
    }
}