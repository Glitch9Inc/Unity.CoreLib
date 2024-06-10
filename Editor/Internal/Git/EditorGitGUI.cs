using System;
using UnityEngine;

namespace Glitch9.Internal.Git
{
    public class EditorGitGUI
    {
        internal static void DrawTrueOrFalseButton(string label, Action<bool> action)
        {
            DrawTwoOptionButton(label, action, "True", "False");
        }

        internal static void DrawSetOrUnsetButton(string label, Action<bool> action)
        {
            DrawTwoOptionButton(label, action, "Set", "Unset");
        }

        internal static void DrawTwoOptionButton(string label, Action<bool> action, string yes, string no)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(label, GUILayout.Width(240));
                if (GUILayout.Button(yes))
                {
                    action(true);
                }
                if (GUILayout.Button(no))
                {
                    action(false);
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}