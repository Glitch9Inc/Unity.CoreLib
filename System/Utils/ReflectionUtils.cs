using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Glitch9
{
    public static class ReflectionUtils
    {
        public static string TypeNameWithNamespace(Type type)
        {
            if (string.IsNullOrEmpty(type.Namespace))
                return type.Name;
            else
                return $"{type.Namespace}.{type.Name}";
        }

        public static IEnumerable<Type> GetSubclassTypes(Type parent, bool excludeAbstract = true, bool excludeInterface = true, params string[] excludes)
        {
            Type type = parent;
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p));
            if (excludeAbstract) types = types.Where(p => !p.IsAbstract);
            if (excludeInterface) types = types.Where(p => !p.IsInterface);
            if (excludes != null && excludes.Length > 0)
            {
                types = types.Where(p =>
                {
                    bool isExcluded = false;
                    foreach (string exclude in excludes)
                    {
                        if (p.Name.Search(exclude))
                        {
                            isExcluded = true;
                            break;
                        }
                    }
                    return !isExcluded;
                });
            }
            return types;
        }

        public static IEnumerable<Type> GetSubclassTypesWithAttribute<TAttribute>(Type parent)
        {
            Type type = parent;
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p.GetCustomAttributes(typeof(TAttribute), true).Length > 0);
            return types;
        }


        public static List<T> InstantiateSubclasses<T>(Action<T> onGet = null, params string[] excludes) where T : class
        {
            List<T> list = new();
            System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsAbstract || type.IsInterface) continue;
                    if (excludes != null && excludes.Length > 0)
                    {
                        bool isExcluded = false;
                        foreach (string exclude in excludes)
                        {
                            if (StringExtensions.Search(type.Name, exclude))
                            {
                                isExcluded = true;
                                break;
                            }
                        }
                        if (isExcluded) continue;
                    }
                    if (type.IsSubclassOf(typeof(T)))
                    {
                        T instance = (T)Activator.CreateInstance(type);
                        list.Add(instance);
                        onGet?.Invoke(instance);
                    }
                }
            }
            return list;
        }

        public static Type FindEnumType(this string enumName)
        {
            Type enumType = null;
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.Name == enumName)
                    {
                        enumType = type;
                        break;
                    }
                }
            }
            if (enumType == null)
            {
                Debug.LogError($"ApiEnumDE {enumName} not found");
#if UNITY_EDITOR
                EditorUtility.DisplayDialog("Error", $"ApiEnumDE {enumName} not found", "OK");
#endif
            }
            return enumType;
        }

        public static string[] FindEnumNames(this string enumName)
        {
            Type enumType = FindEnumType(enumName);
            if (enumType == null) return null;

            // find the enum values
            string[] enumValues = Enum.GetNames(enumType);
            if (enumValues.Length == 0)
            {
                Debug.LogError($"ApiEnumDE {enumName} has no values");
#if UNITY_EDITOR
                EditorUtility.DisplayDialog("Error", $"ApiEnumDE {enumName} has no values", "OK");
#endif
                return null;
            }
            return enumValues;
        }
    }
}
