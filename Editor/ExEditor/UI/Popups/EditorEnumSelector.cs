using System;
using UnityEditor;

namespace Glitch9
{
    public class EditorEnumSelector<T> : EditorSelectorPopup<EditorEnumSelector<T>, T> where T : Enum
    {
        protected override T DrawContent(T value)
        {
            T newValue = (T)EditorGUILayout.EnumPopup(value);
            return newValue;
        }
    }
}