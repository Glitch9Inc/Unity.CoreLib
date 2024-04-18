using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExEditor
{
    public partial class ExGUILayout
    {
        public static bool EnumListToolbar<T>(List<T> selected, out List<T> updated, params GUILayoutOption[] options) where T : Enum
        {
            updated = selected;
            bool changed = false;

            // Get all values and names of the ApiEnumDE
            Array values = Enum.GetValues(typeof(T));
            string[] names = ExEnum.GetNames(typeof(T));

            EditorGUILayout.BeginHorizontal();

            for (int i = 0; i < values.Length; i++)
            {
                // Determine if the current flag is set
                bool flagSet = selected.Contains((T)values.GetValue(i));

                // Use toggle style buttons for the toolbar buttons
                bool newFlagSet = GUILayout.Toggle(flagSet, names[i], EditorStyles.toolbarButton, options);

                if (flagSet != newFlagSet)
                {
                    changed = true;
                    if (newFlagSet) updated.Add((T)values.GetValue(i));
                    else updated.Remove((T)values.GetValue(i));
                }
            }

            EditorGUILayout.EndHorizontal();

            return changed;
        }

        public static T FlagToolbar<T>(T enumValue, int startIndex = 1, params GUILayoutOption[] options) where T : Enum
        {
            // Get all values and names of the ApiEnumDE
            Array values = Enum.GetValues(typeof(T));
            string[] names = Enum.GetNames(typeof(T));

            // Convert the current selected value to int
            int currentIntValue = Convert.ToInt32(enumValue);
            int newIntValue = currentIntValue;

            EditorGUILayout.BeginHorizontal();

            if (values.Length > startIndex)
            {
                for (int i = startIndex; i < values.Length; i++)
                {
                    // Determine if the current flag is set
                    bool flagSet = (currentIntValue & (int)values.GetValue(i)) != 0;

                    if (i == startIndex)
                    {
                        flagSet = ToolBarLeft(names[i], flagSet, options: options);
                    }
                    else if (i == values.Length - 1)
                    {
                        flagSet = ToolBarRight(names[i], flagSet, options: options);
                    }
                    else
                    {
                        flagSet = ToolBarMid(names[i], flagSet, options: options);
                    }

                    // Update the int value based on toggle button state
                    if (flagSet)
                    {
                        newIntValue |= (int)values.GetValue(i);
                    }
                    else
                    {
                        newIntValue &= ~(int)values.GetValue(i);
                    }
                }
            }

            EditorGUILayout.EndHorizontal();

            // Convert the int value back to the enum type and return
            return (T)Enum.ToObject(typeof(T), newIntValue);
        }

        /// <summary>
        /// Enum변수가 변경되면 true를 리턴한다.
        /// </summary>
        public static bool EnumToolbar<T>(T enumValue, out T newEnum, GUIStyle toolbarStyle, params GUILayoutOption[] options) where T : Enum
        {

            string[] enumNames = ExEnum.GetNames(typeof(T));
            int currentIndex = Convert.ToInt32(enumValue);

            toolbarStyle ??= new(EditorStyles.toolbarButton);
            GUILayout.BeginHorizontal(EditorStyles.toolbar);

            int newIndex = GUILayout.Toolbar(currentIndex, enumNames, toolbarStyle, options);

            GUILayout.EndHorizontal();

            if (newIndex != currentIndex)
            {
                newEnum = (T)Enum.ToObject(typeof(T), newIndex);
                return true;
            }

            newEnum = enumValue;
            return false;
        }

        public static bool EnumToolbar<T>(T enumValue, out T newEnum, params GUILayoutOption[] options) where T : Enum
        {
            return EnumToolbar(enumValue, out newEnum, null, options);
        }
    }
}

