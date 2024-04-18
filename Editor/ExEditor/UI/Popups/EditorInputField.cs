using UnityEditor;
using UnityEngine;

namespace Glitch9
{
    public class EditorInputField : EditorSelectorPopup<EditorInputField, string>
    {
        protected override string DrawContent(string value)
        {
            return EditorGUILayout.TextArea(value, GUILayout.ExpandHeight(true));
        }
    }
}