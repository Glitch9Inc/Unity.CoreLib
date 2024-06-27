using System.IO;
using UnityEngine;

namespace Glitch9.ScriptableObjects
{
    public abstract class ScriptableResource<TSelf> : ScriptableObject
        where TSelf : ScriptableResource<TSelf>
    {
        protected static string AssetPath => $"Resources/{typeof(TSelf).Name}";

        private static TSelf _instance;
        public static TSelf Instance => _instance == null ? _instance = AssetUtils.LoadResource<TSelf>(true) : _instance;

        public static TSelf Create(TSelf obj)
        {
#if UNITY_EDITOR
            string path = AssetPath + ".asset";
            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
            UnityEditor.AssetDatabase.CreateAsset(obj, path);
            UnityEditor.EditorUtility.SetDirty(obj);
#endif
            return obj;
        }
    }
}