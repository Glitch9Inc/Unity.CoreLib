using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    /// <summary>
    /// Mostly used for smaller editor windows that don't need a lot of space
    /// </summary>
    public abstract class PaddedEditorWindow : EditorWindow
    {
        const int PADDING_LEFT = 8;
        const int PADDING_TOP = 6;
        const int PADDING_RIGHT = 8;
        const int PADDING_BOTTOM = 10;

        private void OnGUI()
        {
            GUILayout.Space(PADDING_TOP); // Top padding

            GUILayout.BeginHorizontal(); // Start horizontal group for left padding
            {
                GUILayout.Space(PADDING_LEFT);

                GUILayout.BeginVertical();
                {
                    // Your GUI content goes here
                    OnGUIUpdate();
                }
                GUILayout.EndVertical();
                
                
                GUILayout.Space(PADDING_RIGHT);
            }
            GUILayout.EndHorizontal(); // End horizontal group for right padding
           
            GUILayout.Space(PADDING_BOTTOM);  // Bottom padding
        }

        protected abstract void OnGUIUpdate();
    }
}
