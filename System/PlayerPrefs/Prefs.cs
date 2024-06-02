using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Glitch9
{
    public class Prefs<T>
    {
        public static implicit operator T(Prefs<T> prefs) => prefs.Value;

        private readonly bool _useEncryption = false;
        private readonly string _prefsKey;
        private T _cache;
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

        public Prefs(string prefsKey, bool useEncryption = false)
        {
            _prefsKey = prefsKey;
            _useEncryption = useEncryption;

            try
            {
                T savedValue = Load();
                if (savedValue != null)
                {
                    _cache = savedValue;
                }
            }
            catch (Exception e)
            {
                GNLog.Exception(e);
            }
        }

        public Prefs(string prefsKey, T defaultValue, bool useEncryption = false)
        {
            if (string.IsNullOrEmpty(prefsKey)) throw new ArgumentException("prefsKey cannot be null or empty");
            _prefsKey = prefsKey;
            _useEncryption = useEncryption;

            if (!PlayerPrefs.HasKey(_prefsKey))
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
                return (T)Convert.ChangeType(PlayerPrefs.GetInt(_prefsKey), typeof(T));
            }

            if (typeof(T) == typeof(float))
            {
                return (T)Convert.ChangeType(PlayerPrefs.GetFloat(_prefsKey), typeof(T));
            }

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(PlayerPrefs.GetString(_prefsKey), typeof(T));
            }

            if (typeof(T) == typeof(bool))
            {
                return (T)Convert.ChangeType(PlayerPrefs.GetInt(_prefsKey) == 1, typeof(T));
            }

            if (typeof(T) == typeof(UnixTime))
            {
                UnixTime unixTime = new(PlayerPrefs.GetInt(_prefsKey));
                return (T)(object)unixTime;
            }

            if (typeof(T) == typeof(Vector2))
            {
                float x = PlayerPrefs.GetFloat(_prefsKey + ".x");
                float y = PlayerPrefs.GetFloat(_prefsKey + ".y");
                return (T)(object)new Vector2(x, y);
            }

            if (typeof(T) == typeof(Vector3))
            {
                float x = PlayerPrefs.GetFloat(_prefsKey + ".x");
                float y = PlayerPrefs.GetFloat(_prefsKey + ".y");
                float z = PlayerPrefs.GetFloat(_prefsKey + ".z");
                return (T)(object)new Vector3(x, y, z);
            }

            if (typeof(T) == typeof(Quaternion))
            {
                float x = PlayerPrefs.GetFloat(_prefsKey + ".x");
                float y = PlayerPrefs.GetFloat(_prefsKey + ".y");
                float z = PlayerPrefs.GetFloat(_prefsKey + ".z");
                float w = PlayerPrefs.GetFloat(_prefsKey + ".w");
                return (T)(object)new Quaternion(x, y, z, w);
            }

            if (typeof(T).IsEnum)
            {
                // Correct approach for loading enums
                int storedValue = PlayerPrefs.GetInt(_prefsKey);
                return (T)Enum.ToObject(typeof(T), storedValue);
            }

            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
            {
                string json = PlayerPrefs.GetString(_prefsKey, null);
                if (string.IsNullOrEmpty(json)) return Activator.CreateInstance<T>();

                try
                {
                    Type itemType = typeof(T).GetGenericArguments()[0];
                    Type listType = typeof(List<>).MakeGenericType(itemType);
                    return (T)JsonConvert.DeserializeObject(json, listType, JsonUtils.DefaultSettings);
                }
                catch (Exception e)
                {
                    PrefsUtils.HandleFailedDeserialization(_prefsKey, typeof(T).Name, e);
                    return default;
                }
            }

            try
            {
                string json = PlayerPrefs.GetString(_prefsKey, "{}");
                return JsonConvert.DeserializeObject<T>(json, JsonUtils.DefaultSettings);
            }
            catch (Exception e)
            {
                PrefsUtils.HandleFailedDeserialization(_prefsKey, typeof(T).Name, e);
                return default;
            }
        }


        public void Save()
        {
            if (Value is int intValue)
            {
                PlayerPrefs.SetInt(_prefsKey, intValue);
            }
            else if (Value is float floatValue)
            {
                PlayerPrefs.SetFloat(_prefsKey, floatValue);
            }
            else if (Value is string stringValue)
            {
                PlayerPrefs.SetString(_prefsKey, stringValue);
            }
            else if (Value is bool boolValue)
            {
                PlayerPrefs.SetInt(_prefsKey, boolValue ? 1 : 0);
            }
            else if (Value is UnixTime unixTimeValue)
            {
                PlayerPrefs.SetInt(_prefsKey, (int)unixTimeValue);
            }
            else if (Value is Vector2 vector2)
            {
                PlayerPrefs.SetFloat(_prefsKey + ".x", vector2.x);
                PlayerPrefs.SetFloat(_prefsKey + ".y", vector2.y);
            }
            else if (Value is Vector3 vector3)
            {
                PlayerPrefs.SetFloat(_prefsKey + ".x", vector3.x);
                PlayerPrefs.SetFloat(_prefsKey + ".y", vector3.y);
                PlayerPrefs.SetFloat(_prefsKey + ".z", vector3.z);
            }
            else if (Value is Quaternion quaternion)
            {
                PlayerPrefs.SetFloat(_prefsKey + ".x", quaternion.x);
                PlayerPrefs.SetFloat(_prefsKey + ".y", quaternion.y);
                PlayerPrefs.SetFloat(_prefsKey + ".z", quaternion.z);
                PlayerPrefs.SetFloat(_prefsKey + ".w", quaternion.w);
            }
            else if (Value.GetType().IsEnum)
            {
                // Safely convert enum to its underlying int value
                int enumValue = Convert.ToInt32(Value);
                PlayerPrefs.SetInt(_prefsKey, enumValue);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Value, JsonUtils.DefaultSettings);
                PlayerPrefs.SetString(_prefsKey, json);
            }

            PlayerPrefs.Save();
        }

        public void Clear()
        {
            Value = default;
            PlayerPrefs.DeleteKey(_prefsKey);
        }
    }
}
