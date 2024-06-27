using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Glitch9.Collections
{
    [Serializable]
    public class ReferencedDictionary<TKeyValuePair, TKey, TValue> : IDictionary<TKey, TValue> 
        where TKeyValuePair : class, IKeyValuePair<TKey, TValue>, new() 
        where TKey : notnull
    {
        [SerializeReference] public List<TKeyValuePair> serializedList = new();
        private readonly Dictionary<TKey, TValue> _dictionary = new();

        public int Count => _dictionary.Count;
        public bool IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).IsReadOnly;
        
        public ReferencedDictionary()
        {
            Deserialize();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _dictionary.Add(item.Key, item.Value);
            Serialize();
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            foreach (KeyValuePair<TKey, TValue> item in items)
            {
                _dictionary.Add(item.Key, item.Value);
            }
            Serialize();
        }

        public void Clear()
        {
            _dictionary.Clear();
            Serialize();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.ContainsKey(item.Key) && EqualityComparer<TValue>.Default.Equals(_dictionary[item.Key], item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (_dictionary.ContainsKey(item.Key) && EqualityComparer<TValue>.Default.Equals(_dictionary[item.Key], item.Value))
            {
                bool removed = _dictionary.Remove(item.Key);
                Serialize();
                return removed;
            }

            return false;
        }

        public bool RemoveAll(Predicate<KeyValuePair<TKey, TValue>> match)
        {
            List<TKey> keysToRemove = new();
            foreach (KeyValuePair<TKey, TValue> pair in _dictionary)
            {
                if (match(pair))
                {
                    keysToRemove.Add(pair.Key);
                }
            }

            foreach (TKey key in keysToRemove)
            {
                _dictionary.Remove(key);
            }

            if (keysToRemove.Count > 0)
            {
                Serialize();
                return true;
            }

            return false;
        }

        // FindAll
        public List<TValue> FindAll(Predicate<TValue> match)
        {
            List<TValue> values = new();
            foreach (KeyValuePair<TKey, TValue> pair in _dictionary)
            {
                if (match(pair.Value))
                {
                    values.Add(pair.Value);
                }
            }

            return values;
        }

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
            Serialize();
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            bool removed = _dictionary.Remove(key);
            if (removed)
            {
                Serialize();
            }

            return removed;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set
            {
                _dictionary[key] = value;
                Serialize();
            }
        }

        public ICollection<TKey> Keys => _dictionary.Keys;

        public ICollection<TValue> Values => _dictionary.Values;

        private void Serialize()
        {
            serializedList.Clear();
            foreach (KeyValuePair<TKey, TValue> kvp in _dictionary)
            {
                serializedList.Add(new TKeyValuePair { Key = kvp.Key, Value = kvp.Value });
            }
        }

        private void Deserialize()
        {
            _dictionary.Clear();
            foreach (TKeyValuePair kvp in serializedList)
            {
                _dictionary[kvp.Key] = kvp.Value;
            }
        }
    }
}