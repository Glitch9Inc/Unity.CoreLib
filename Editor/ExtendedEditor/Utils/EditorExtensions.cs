using System;
using System.Reflection;
using UnityEditor;

namespace Glitch9.ExtendedEditor
{
    public static class EditorExtensions
    {
        public static Type GetEnumType(this SerializedProperty property)
        {
            // Get the object that the property belongs to
            object targetObject = property.serializedObject.targetObject;

            // Use reflection to get the FieldInfo of the property
            Type targetType = targetObject.GetType();
            FieldInfo fieldInfo = targetType.GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            if (fieldInfo != null && fieldInfo.FieldType.IsEnum)
            {
                // Return the enum type
                return fieldInfo.FieldType;
            }

            return null;
        }
    }
}