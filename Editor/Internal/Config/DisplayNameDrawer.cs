using System;
using UnityEditor;
using UnityEngine;

namespace Glitch9.Internal
{
    [CustomPropertyDrawer(typeof(DisplayNameAttribute))]
    public class DisplayNameDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Enum)
            {
                EditorGUI.BeginProperty(position, label, property);

                System.Type enumType = fieldInfo.FieldType;
                string[] enumNames = EnumUtils.GetNames(enumType);
                int[] enumValues = Enum.GetValues(enumType) as int[];

                string[] displayedOptions = new string[enumNames.Length];
                for (int i = 0; i < enumNames.Length; i++)
                {
                    System.Reflection.FieldInfo field = enumType.GetField(enumNames[i]);
                    DisplayNameAttribute attr = Attribute.GetCustomAttribute(field, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                    displayedOptions[i] = attr != null ? attr.DisplayName : enumNames[i];
                }

                int selectedIndex = EditorGUI.Popup(position, property.enumValueIndex, displayedOptions);
                if (selectedIndex >= 0)
                {
                    property.enumValueIndex = enumValues[selectedIndex];
                }

                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}