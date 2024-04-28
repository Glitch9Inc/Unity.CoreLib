using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Glitch9
{
    public static class ExEnum
    {
        // cache the enum names
        private static readonly Dictionary<Type, Dictionary<Enum, string>> _enumDisplayNames = new();

        public static string GetName(this Enum value)
        {
            Type type = value.GetType();
            if (!_enumDisplayNames.TryGetValue(type, out Dictionary<Enum, string> names))
            {
                names = new Dictionary<Enum, string>();
                _enumDisplayNames[type] = names;
            }

            if (names.TryGetValue(value, out string cachedName))
            {
                return cachedName;
            }

            string enumName = value.ToString();
            FieldInfo field = type.GetField(enumName);
            if (field != null)
            {
                // 모든 ExEnumAttribute 어트리뷰트를 배열로 가져오기
                DisplayNameAttribute[] nameAttributes = field.GetCustomAttributes<DisplayNameAttribute>().ToArray();

                if (nameAttributes.Length > 0)
                {
                    DisplayNameAttribute nameAttribute = nameAttributes[0];
                    if (nameAttribute != null)
                    {
                        names[value] = nameAttribute.DisplayName;
                        return nameAttribute.DisplayName;
                    }
                }
            }

            names[value] = enumName; // Cache the default name if custom name is not found.
            return enumName;
        }

        public static string[] GetNames(Type enumType)
        {
            string[] enumNames = Enum.GetNames(enumType);
            string[] displayedOptions = new string[enumNames.Length];
            for (int i = 0; i < enumNames.Length; i++)
            {
                Enum value = (Enum)Enum.Parse(enumType, enumNames[i]);
                displayedOptions[i] = value.GetName();
            }

            return displayedOptions;
        }

#if UNITY_EDITOR
        public static TEnum EnumPopup<TEnum>(string label, TEnum selected, params GUILayoutOption[] options) where TEnum : struct, Enum
        {
            string[] displayedOptions = GetNames(typeof(TEnum));
            TEnum[] enumValues = (TEnum[])Enum.GetValues(typeof(TEnum));

            // GetName을 사용하여 현재 선택된 Enum의 이름을 가져옵니다.
            string selectedName = selected.GetName();

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