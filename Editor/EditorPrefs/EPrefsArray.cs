using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Glitch9.ExtendedEditor.Collections
{
    public class EPrefsArray<T> : IList<T>
    {
        public static implicit operator T[](EPrefsArray<T> prefsArray) => prefsArray._array;

        public int Count => _array.Length;
        public bool IsReadOnly => false;

        private readonly string _prefsKey;
        private readonly T[] _array;
        public string ListName
        {
            get
            {
                if (_listName == null)
                {
                    string itemName = typeof(T).Name;
                    _listName = $"EPrefsArray<{itemName}>";
                }
                return _listName;
            }
        }
        private string _listName;

        public EPrefsArray(string prefsKey)
        {
            if (string.IsNullOrEmpty(prefsKey)) throw new ArgumentException("prefsKey cannot be null or empty");
            _prefsKey = prefsKey;

            try
            {
                T[] savedValue = Load();
                if (savedValue != null) _array = savedValue;
            }
            catch (Exception e)
            {
                GNLog.Exception(e);
            }
        }

        public EPrefsArray(string prefsKey, T[] defaultValue)
        {
            if (string.IsNullOrEmpty(prefsKey)) throw new ArgumentException("prefsKey cannot be null or empty");
            _prefsKey = prefsKey;

            if (!EditorPrefs.HasKey(_prefsKey))
            {
                _array = defaultValue;
                Save();
                return;
            }

            try
            {
                T[] savedValue = Load();
                _array = savedValue ?? defaultValue;
                if (_array == null || savedValue == null)
                {
                    _array = defaultValue;
                    Save();
                }
            }
            catch (Exception e)
            {
                GNLog.Exception(e);
                _array = defaultValue;
                Save();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            throw new NotSupportedException("Add is not supported on EPrefsArray. Arrays have fixed size.");
        }

        public void Clear()
        {
            Array.Clear(_array, 0, _array.Length);
            Save();
        }

        public bool Contains(T item)
        {
            return Array.Exists(_array, element => EqualityComparer<T>.Default.Equals(element, item));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_array, 0, array, arrayIndex, _array.Length);
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException("Remove is not supported on EPrefsArray. Arrays have fixed size.");
        }

        public int IndexOf(T item)
        {
            return Array.IndexOf(_array, item);
        }

        public void Insert(int index, T item)
        {
            throw new NotSupportedException("Insert is not supported on EPrefsArray. Arrays have fixed size.");
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException("RemoveAt is not supported on EPrefsArray. Arrays have fixed size.");
        }

        public void Replace(T[] newArray)
        {
            if (newArray == null) throw new ArgumentNullException(nameof(newArray));
            if (newArray.Length != _array.Length) throw new ArgumentException("New array must be the same length as the existing array.");

            Array.Copy(newArray, _array, _array.Length);
            Save();
        }

        public T this[int index]
        {
            get => _array[index];
            set
            {
                _array[index] = value;
                Save();
            }
        }

        private T[] Load()
        {
            try
            {
                string json = EditorPrefs.GetString(_prefsKey, "[]");
                return JsonConvert.DeserializeObject<T[]>(json, JsonUtils.DefaultSettings);
            }
            catch (Exception e)
            {
                EPrefsUtils.HandleFailedDeserialization(_prefsKey, ListName, e);
                return default;
            }
        }

        private void Save()
        {
            string json = JsonConvert.SerializeObject(_array, JsonUtils.DefaultSettings);
            EditorPrefs.SetString(_prefsKey, json);
        }
    }
}
