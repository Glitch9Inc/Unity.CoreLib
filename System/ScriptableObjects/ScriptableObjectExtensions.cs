using UnityEngine;

namespace Glitch9.ScriptableObjects
{
    public static class ScriptableObjectExtensions
    {
        public static void Save<T>(this T scriptableObject) where T : ScriptableObject
        {
#if UNITY_EDITOR
            if (scriptableObject == null) return;
            UnityEditor.EditorUtility.SetDirty(scriptableObject);
#endif
        }
    }
}