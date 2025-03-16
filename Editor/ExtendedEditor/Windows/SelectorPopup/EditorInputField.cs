using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    /// <summary>
    /// Provides an input field for editing a string
    /// </summary>
    public class EditorInputField : EditorSelectorPopup<EditorInputField, string>
    {
        const float WIDTH = 340f;
        const float HEIGHT = 100f;
        protected override void Initialize()
        {
            minSize = new Vector2(WIDTH, HEIGHT);
            maxSize = new Vector2(WIDTH, HEIGHT);
        }

        protected override string DrawContent(string value)
        {
            string result = EditorGUILayout.TextArea(value, GUILayout.ExpandHeight(true));
            GUILayout.Space(10);
            return result;
        }
    }
}