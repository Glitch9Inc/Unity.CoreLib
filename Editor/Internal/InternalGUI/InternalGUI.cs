using UnityEditor;
using UnityEngine;
using MessageType = UnityEditor.MessageType;

namespace Glitch9.ExtendedEditor
{
    public class InternalGUI
    {
        private const float BIG_BTN_HEIGHT = 30;

        public static void InfoVisitButton(string label, string url)
        {
            GUILayout.BeginHorizontal();

            EditorGUILayout.HelpBox(
                label
                , MessageType.Info);

            if (GUILayout.Button("Visit", new GUILayoutOption[] {
                GUILayout.Width(50),
                GUILayout.Height(38),
                }))
            {
                Application.OpenURL(url);
            }

            GUILayout.EndHorizontal();
        }

        public static void Title(string label)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(label, EGUIStyles.title);
            EGUIUtility.DrawTitleLine();
        }

        public static void ScriptableObjectNotInResourcesFolder<TScriptableObject>() where TScriptableObject : ScriptableObject
        {
            string objectName = typeof(TScriptableObject).Name;

            EditorGUILayout.HelpBox($"{objectName} is does not exist in this project. " +
                                    $"You need {objectName} in any of the 'Resources' folders to use this feature. " +
                                    $"Would you like to create one in the default path?", MessageType.Warning);

            if (GUILayout.Button($"Create {objectName}", GUILayout.Width(50), GUILayout.Height(38)))
            {
                AssetUtils.CreateResourcesAsset<TScriptableObject>(objectName);
            }
        }

        public static void TroubleShooting(string docUrl, string githubUrl)
        {
            GUILayout.Label("Troubleshooting", EditorStyles.boldLabel);

            string desc = "If you encounter any issues or have questions, please check the documentation or contact us for support.";
            GUILayout.Label(desc, EditorStyles.wordWrappedMiniLabel);

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Documentation", GUILayout.Height(BIG_BTN_HEIGHT), GUILayout.Width(200), GUILayout.ExpandWidth(true)))
                {
                    Application.OpenURL(docUrl);
                }

                if (GUILayout.Button("Report An Issue (Github)", GUILayout.Height(BIG_BTN_HEIGHT), GUILayout.Width(200), GUILayout.ExpandWidth(true)))
                {
                    Application.OpenURL(githubUrl);
                }

                if (GUILayout.Button("Contact Me", GUILayout.Height(BIG_BTN_HEIGHT), GUILayout.Width(200), GUILayout.ExpandWidth(true)))
                {
                    Application.OpenURL("mailto:munchkin@glitch9.dev");
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}

