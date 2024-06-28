using System;
using Glitch9.ExtendedEditor;
using UnityEditor;
using UnityEngine;

namespace Glitch9.EditorTools
{
    [CustomPropertyDrawer(typeof(UnixTime))]
    public class UnixTimeDrawer : PropertyDrawer
    {
        private DateTime? _dateTime;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (_dateTime == null)
            {
                SerializedProperty unixTimeAsLong = property.FindPropertyRelative("_value");
                UnixTime unixTime = new UnixTime(unixTimeAsLong.longValue);
                _dateTime = unixTime.ToDateTime();
            }

            float singleLineHeight = EditorGUIUtility.singleLineHeight;

            GUILayout.Space(-singleLineHeight);
            position.height = singleLineHeight;

            // Indent label
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            DateTime newDateTime = EGUILayout.DateTimeField(label, _dateTime.Value, true, true, true, true, true, true);

            if (newDateTime != _dateTime)
            {
                _dateTime = newDateTime;
                UnixTime unixTime = new UnixTime(_dateTime.Value);
                SerializedProperty unixTimeAsLong = property.FindPropertyRelative("_value");
                unixTimeAsLong.longValue = unixTime.Value;
            }

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}