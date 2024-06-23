using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    public class EditorEGUILayout
    {
        public static void SpriteField(SerializedProperty p, int size, int topMargin)
        {
            GUILayout.BeginVertical();
            GUILayout.Space(topMargin);
            p.objectReferenceValue = EditorGUILayout.ObjectField(p.objectReferenceValue, typeof(Sprite), false, new GUILayoutOption[] {
                GUILayout.Width(size),
                GUILayout.Height(size)
            }); ;
            GUILayout.EndVertical();
        }
    }
}