using Glitch9.ExEditor;
using UnityEditor;
using UnityEngine;

namespace Glitch9.IO.Git
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
            _git.InitializeAsync(gitUrl.stringValue, gitBranch.stringValue, localDir.stringValue);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUILayout.BeginVertical(ExGUI.Box(3, 7));
            {
                GUILayout.Label("Git Module", EditorStyles.boldLabel);
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
            }
            GUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();

            if (!_git.IsInitialized)
            {
                GUILayout.BeginVertical(ExGUI.Box(3, 7));
                {
                    GUILayout.Label("Initializing Git...");
                }
                GUILayout.EndVertical();
                return;
            }

            _git.DrawGit();
            GUILayout.Space(10);
            _git.DrawRemoteMenu();
            _git.DrawDebugMenu();
        }
    }
}