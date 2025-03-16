using Glitch9.ExtendedEditor;
using UnityEditor;
using UnityEngine;

namespace Glitch9
{
    public class CopyrightBox
    {
        private const string k_OnlineDocsLabel = "Online Documentation";
        private const float k_Space = 7f;
        private const float k_ButtonHeight = 24f;

        public static void Draw(int year, string productName, string url, string version = null)
        {
            version ??= Application.version;
            
            EditorGUILayout.BeginVertical(EGUI.Box(10, 10));
            {
                EGUILayout.Label(new GUIContent($"{productName} version: {version}"), TextAnchor.MiddleCenter, 14);
                EGUILayout.Label(new GUIContent($"{year} Glitch9 Inc. All Rights Reserved."), TextAnchor.MiddleCenter, 10);

                GUILayout.Space(k_Space);

                if (GUILayout.Button(k_OnlineDocsLabel, GUILayout.Height(k_ButtonHeight)))
                {
                    Application.OpenURL(url);
                }
            }
            EditorGUILayout.EndVertical();
        }
    }
}