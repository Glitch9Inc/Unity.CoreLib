using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEngine;

namespace Glitch9.ExEditor
{
    public class EPrefs<T>
    {
        public static implicit operator T(EPrefs<T> prefs) => prefs == null ? default : prefs.Value;

        public T Value
        {
            get
            {
                _cache ??= Load();
                return _cache;
            }

            set
            {
                if (EqualityComparer<T>.Default.Equals(_cache, value)) return;
                _cache = value;
                Save();
            }
        }

        private readonly string _prefsKey;
        private T _cache;

        public EPrefs(string prefsKey, T defaultValue)
        {
            if (string.IsNullOrEmpty(prefsKey)) throw new ArgumentException("prefsKey cannot be null or empty");
            _prefsKey = prefsKey;

            if (!EditorPrefs.HasKey(_prefsKey))
            {
                _cache = defaultValue;
                Save();
                return;
            }

            try
            {
                T savedValue = Load();
                _cache = savedValue ?? defaultValue; // 로드된 값이 null이면 defaultValue 사용
                if (_cache == null || savedValue == null) // 로드된 값이 null이거나 첫 로드에서 값을 얻지 못한 경우
                {
                    _cache = defaultValue;
                    Save();
                }
            }
            catch (Exception e)
            {
                GNLog.Exception(e);
                _cache = defaultValue; // 예외 발생 시 defaultValue를 사용
                Save();
            }
        }

        private T Load()
        {
            if (typeof(T) == typeof(int))
            {
                return (T)Convert.ChangeType(EditorPrefs.GetInt(_prefsKey), typeof(T));
            }

            if (typeof(T) == typeof(float))
            {
                return (T)Convert.ChangeType(EditorPrefs.GetFloat(_prefsKey), typeof(T));
            }

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(EditorPrefs.GetString(_prefsKey), typeof(T));
            }

            if (typeof(T) == typeof(bool))
            {
                return (T)Convert.ChangeType(EditorPrefs.GetBool(_prefsKey), typeof(T));
            }

            if (typeof(T) == typeof(UnixTime))
            {
                UnixTime unixTime = new(EditorPrefs.GetInt(_prefsKey));
                return (T)(object)unixTime;
            }

            if (typeof(T) == typeof(Vector2))
            {
                float x = EditorPrefs.GetFloat(_prefsKey + ".x");
                float y = EditorPrefs.GetFloat(_prefsKey + ".y");
                return (T)(object)new Vector2(x, y);
            }

            if (typeof(T) == typeof(Vector3))
            {
                float x = EditorPrefs.GetFloat(_prefsKey + ".x");
                float y = EditorPrefs.GetFloat(_prefsKey + ".y");
                float z = EditorPrefs.GetFloat(_prefsKey + ".z");
                return (T)(object)new Vector3(x, y, z);
            }

            if (typeof(T) == typeof(Quaternion))
            {
                float x = EditorPrefs.GetFloat(_prefsKey + ".x");
                float y = EditorPrefs.GetFloat(_prefsKey + ".y");
                float z = EditorPrefs.GetFloat(_prefsKey + ".z");
                float w = EditorPrefs.GetFloat(_prefsKey + ".w");
                return (T)(object)new Quaternion(x, y, z, w);
            }

            if (typeof(T).IsEnum)
            {
                // Correct approach for loading enums
                int storedValue = EditorPrefs.GetInt(_prefsKey);
                return (T)Enum.ToObject(typeof(T), storedValue);
            }

            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
            {
                string json = EditorPrefs.GetString(_prefsKey, null);
                if (string.IsNullOrEmpty(json)) return Activator.CreateInstance<T>();

                try
                {
                    Type itemType = typeof(T).GetGenericArguments()[0];
                    Type listType = typeof(List<>).MakeGenericType(itemType);
                    return (T)JsonConvert.DeserializeObject(json, listType, JsonUtils.DefaultSettings);
                }
                catch (Exception e)
                {
                    HandleFailedDeserialization(e);
                    return default;
                }
            }

            try
            {
                string json = EditorPrefs.GetString(_prefsKey, "{}");
                return JsonConvert.DeserializeObject<T>(json, JsonUtils.DefaultSettings);
            }
            catch (Exception e)
            {
                HandleFailedDeserialization(e);
                return default;
            }
        }

        private void HandleFailedDeserialization(Exception e)
        {
            string json = EditorPrefs.GetString(_prefsKey, string.Empty);
            Debug.LogError($"Error occurred while deserializing {typeof(T).Name}: {e.Message}");
            Debug.LogError($"Failed JSON: {json}");

            if (!string.IsNullOrWhiteSpace(json))
            {
                // Create backup to a file
                string backupPath = Path.Combine(Application.persistentDataPath, $"EPrefs_{_prefsKey}.json");
                Debug.LogError($"Creating JSON backup at: {backupPath}");
                File.WriteAllText(backupPath, json);
            }
            
            EditorPrefs.DeleteKey(_prefsKey);
        }


        public void Save()
        {
            if (Value is int intValue)
            {
                EditorPrefs.SetInt(_prefsKey, intValue);
            }
            else if (Value is float floatValue)
            {
                EditorPrefs.SetFloat(_prefsKey, floatValue);
            }
            else if (Value is string stringValue)
            {
                EditorPrefs.SetString(_prefsKey, stringValue);
            }
            else if (Value is bool boolValue)
            {
                EditorPrefs.SetBool(_prefsKey, boolValue);
            }
            else if (Value is UnixTime unixTimeValue)
            {
                EditorPrefs.SetInt(_prefsKey, (int)unixTimeValue);
            }
            else if (Value is Vector2 vector2)
            {
                EditorPrefs.SetFloat(_prefsKey + ".x", vector2.x);
                EditorPrefs.SetFloat(_prefsKey + ".y", vector2.y);
            }
            else if (Value is Vector3 vector3)
            {
                EditorPrefs.SetFloat(_prefsKey + ".x", vector3.x);
                EditorPrefs.SetFloat(_prefsKey + ".y", vector3.y);
                EditorPrefs.SetFloat(_prefsKey + ".z", vector3.z);
            }
            else if (Value is Quaternion quaternion)
            {
                EditorPrefs.SetFloat(_prefsKey + ".x", quaternion.x);
                EditorPrefs.SetFloat(_prefsKey + ".y", quaternion.y);
                EditorPrefs.SetFloat(_prefsKey + ".z", quaternion.z);
                EditorPrefs.SetFloat(_prefsKey + ".w", quaternion.w);
            }
            else if (Value.GetType().IsEnum)
            {
                // Safely convert enum to its underlying int value
                int enumValue = Convert.ToInt32(Value);
                EditorPrefs.SetInt(_prefsKey, enumValue);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Value, JsonUtils.DefaultSettings);
                EditorPrefs.SetString(_prefsKey, json);
            }
        }
    }

    public static class EditorPrefsExtensions
    {
        public static bool HasValue<T>(this EPrefs<T> prefs)
        {
            return prefs != null && prefs.Value != null;
        }
    }
}