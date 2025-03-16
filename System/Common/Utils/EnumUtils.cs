using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Glitch9
{
    public static class EnumUtils
    {
        // cache the enum names
        private static readonly ConcurrentDictionary<Type, Dictionary<Enum, string>> _enumDisplayNames = new();

        public static string GetDisplayName(this Enum value)
        {
            Type type = value.GetType();
            if (!_enumDisplayNames.TryGetValue(type, out Dictionary<Enum, string> displayNames))
            {
                displayNames = new Dictionary<Enum, string>();
                _enumDisplayNames[type] = displayNames;
            }

            if (displayNames.TryGetValue(value, out string displayName))
            {
                return displayName;
            }

            string enumAsString = value.ToString();
            FieldInfo field = type.GetField(enumAsString);
            
            if (field != null)
            {
                DisplayNameAttribute nameAttribute = AttributeCache<DisplayNameAttribute>.Get(field);

                if (nameAttribute != null)
                {
                    displayNames[value] = nameAttribute.DisplayName;
                    return nameAttribute.DisplayName;
                }
            }

            displayNames[value] = enumAsString; // Cache the default name if custom name is not found.
            _enumDisplayNames[type] = displayNames;
            
            return enumAsString;
        }

        public static string[] GetDisplayNames(Type enumType)
        {
            string[] enumNames = Enum.GetNames(enumType);
            string[] displayedOptions = new string[enumNames.Length];
            for (int i = 0; i < enumNames.Length; i++)
            {
                Enum value = (Enum)Enum.Parse(enumType, enumNames[i]);
                displayedOptions[i] = value.GetDisplayName();
            }

            return displayedOptions;
        }

#if UNITY_EDITOR
        public static TEnum EnumPopup<TEnum>(string label, TEnum selected, params GUILayoutOption[] options) where TEnum : struct, Enum
        {
            string[] displayedOptions = GetDisplayNames(typeof(TEnum));
            TEnum[] enumValues = (TEnum[])Enum.GetValues(typeof(TEnum));

            // GetName을 사용하여 현재 선택된 Enum의 이름을 가져옵니다.
            string selectedName = selected.GetDisplayName();

            // displayedOptions에서 selectedName의 인덱스를 찾습니다.
            int selectedIndex = Array.IndexOf(displayedOptions, selectedName);

            // 사용자가 선택한 새로운 Enum 항목의 인덱스를 가져옵니다.
            selectedIndex = EditorGUILayout.Popup(label, selectedIndex, displayedOptions, options);

            if (selectedIndex >= 0 && selectedIndex < enumValues.Length)
            {
                // selectedIndex에 해당하는 Enum 값을 반환합니다.
                return enumValues[selectedIndex];
            }

            // 오류가 있는 경우, 기존의 선택된 값을 반환합니다.
            return selected;
        }
#endif

    }
}