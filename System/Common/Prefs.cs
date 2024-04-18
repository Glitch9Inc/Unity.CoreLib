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


        public Prefs(string prefsKey, bool useEncryption)
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

        public Prefs(string prefsKey, T defaultValue = default, bool useEncryption = false)
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
                else if (defaultValue != null)
                {
                    _cache = defaultValue;
                    Save();
                }
            }
            catch (Exception e)
            {
                GNLog.Exception(e);
            }
        }

        private T Load()
        {
            // 타입을 체크해서 PlayerPrefs의 어떤 메소드를 사용할지 결정해야 함
            if (typeof(T) == typeof(int))
            {
                return (T)Convert.ChangeType(GetInt(), typeof(T));
            }
            else if (typeof(T) == typeof(float))
            {
                return (T)Convert.ChangeType(GetFloat(), typeof(T));
            }
            else if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(GetString(), typeof(T));
            }
            else if (typeof(T) == typeof(bool))
            {
                return (T)Convert.ChangeType(GetInt() == 1, typeof(T));
            }
            else if (typeof(T) == typeof(UnixTime))
            {
                UnixTime unixTime = new(GetInt());
                return (T)(object)unixTime;
            }
            else if (typeof(T).IsEnum)
            {
                // Correct approach for loading enums
                int storedValue = GetInt();
                return (T)Enum.ToObject(typeof(T), storedValue);
            }
            else
            {
                try
                {
                    string json = GetString();
                    return JsonConvert.DeserializeObject<T>(json, JsonUtils.DefaultSettings);
                }
                catch
                {
                    // remove the corrupted data
                    PlayerPrefs.DeleteKey(_prefsKey);
                    return default;
                }
            }
        }

        public void Save()
        {
            // 타입을 체크해서 EditorPrefs의 어떤 메소드를 사용할지 결정해야 함

            if (Value is int intValue)
            {
                SetInt(intValue);
            }
            else if (Value is float floatValue)
            {
                SetFloat(floatValue);
            }
            else if (Value is string stringValue)
            {
                SetString(stringValue);
            }
            else if (Value is bool boolValue)
            {
                SetInt(boolValue ? 1 : 0);
            }
            else if (Value is UnixTime unixTimeValue)
            {
                SetInt((int)unixTimeValue);
            }
            else if (Value.GetType().IsEnum)
            {
                // Safely convert enum to its underlying int value
                int enumValue = Convert.ToInt32(Value);
                SetInt(enumValue);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Value, JsonUtils.DefaultSettings);
                SetString(json);
            }
        }

        public string GetJsonString()
        {
            return GetString();
        }

        public void Clear()
        {
            Value = default;
            PlayerPrefs.DeleteKey(_prefsKey);
        }

        private string GetString(string defaultValue = "")
        {
            string value;
            if (_useEncryption) value = EncryptedPrefs.Get(_prefsKey, defaultValue);
            else value = PlayerPrefs.GetString(_prefsKey, defaultValue);
            return value;
        }

        private void SetString(string value)
        {
            if (_useEncryption) EncryptedPrefs.Set(_prefsKey, value);
            else PlayerPrefs.SetString(_prefsKey, value);
        }

        private int GetInt(int defaultValue = 0)
        {
            int value;
            if (_useEncryption) value = EncryptedPrefs.Get(_prefsKey, defaultValue);
            else value = PlayerPrefs.GetInt(_prefsKey, defaultValue);
            return value;
        }

        private void SetInt(int value)
        {
            if (_useEncryption) EncryptedPrefs.Set(_prefsKey, value);
            else PlayerPrefs.SetInt(_prefsKey, value);
        }

        private float GetFloat(float defaultValue = 0)
        {
            float value;
            if (_useEncryption) value = EncryptedPrefs.Get(_prefsKey, defaultValue);
            else value = PlayerPrefs.GetFloat(_prefsKey, defaultValue);
            return value;
        }

        private void SetFloat(float value)
        {
            if (_useEncryption) EncryptedPrefs.Set(_prefsKey, value);
            else PlayerPrefs.SetFloat(_prefsKey, value);
        }
    }
}
