using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    /// <summary>
    /// Provides an input field for editing a string
    /// </summary>
    public class EditorInputField : EditorSelectorPopup<EditorInputField, string>
    {
        protected override string DrawContent(string value)
        {
            return EditorGUILayout.TextArea(value, GUILayout.ExpandHeight(true));
        }
    }
}