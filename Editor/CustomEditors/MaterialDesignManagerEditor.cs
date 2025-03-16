using UnityEditor;
using UnityEngine;

namespace Glitch9.UI.MaterialDesign
{
    [CustomEditor(typeof(MaterialDesignManager))]
    public class MaterialDesignManagerEditor : Editor
    {
        private SerializedProperty dontDestroyOnLoad;
        private SerializedProperty theme;
        private SerializedProperty lightScheme;
        private SerializedProperty darkScheme;
        private SerializedProperty editPrefabs;

        private MaterialDesignManager _target;


        private void OnEnable()
        {
            dontDestroyOnLoad = serializedObject.FindProperty(nameof(dontDestroyOnLoad));
            theme = serializedObject.FindProperty(nameof(theme));
            lightScheme = serializedObject.FindProperty(nameof(lightScheme));
            darkScheme = serializedObject.FindProperty(nameof(darkScheme));
            editPrefabs = serializedObject.FindProperty(nameof(editPrefabs));

            _target = (MaterialDesignManager)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(dontDestroyOnLoad);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(theme);
            EditorGUILayout.PropertyField(editPrefabs);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Color Schemes", EditorStyles.boldLabel);
            
            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(lightScheme);
                if (GUILayout.Button("Apply", GUILayout.Width(50)))
                {
                    _target.ApplyColors(MaterialTheme.Light);
                    SceneView.RepaintAll();
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(darkScheme);

                if (GUILayout.Button("Apply", GUILayout.Width(50)))
                {
                    _target.ApplyColors(MaterialTheme.Dark);
                    SceneView.RepaintAll();
                }
            }
            GUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }
}