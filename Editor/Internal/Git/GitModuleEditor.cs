using Glitch9.ExtendedEditor;
using UnityEditor;
using UnityEngine;

namespace Glitch9.Internal.Git
{
    [CustomEditor(typeof(GitModule))]
    public class GitModuleEditor : Editor
    {
        private GitModule gitModule;
        private SerializedProperty gitUrl;
        private SerializedProperty gitBranch;
        private SerializedProperty localDir;
        private EditorGit _git;

        private void OnEnable()
        {
            gitModule = (GitModule)target;
            gitUrl = serializedObject.FindProperty(nameof(gitUrl));
            gitBranch = serializedObject.FindProperty(nameof(gitBranch));
            localDir = serializedObject.FindProperty(nameof(localDir));
            _git = new EditorGit();
            _git.InitializeAsync(gitUrl.stringValue, gitBranch.stringValue, localDir.stringValue, Repaint);
        }

        private void OnDestroy()
        {
            _git?.OnDestroy();;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EGUILayout.Title("Git Module");
            EditorGUILayout.PropertyField(gitUrl);

            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(localDir);
                if (GUILayout.Button("Find Directory", GUILayout.Width(100)))
                {
                    gitModule.FindLocalDir();
                }
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(gitBranch);

            serializedObject.ApplyModifiedProperties();

            GUILayout.Space(10);
            EGUILayout.Title("Git Window");

            if (_git == null || !_git.IsInitialized)
            {
                GUILayout.BeginVertical(EGUI.Box(3, 7));
                {
                    GUILayout.Label("Initializing Git...");
                }
                GUILayout.EndVertical();
                return;
            }

            _git.DrawGit();      
        }
    }
}