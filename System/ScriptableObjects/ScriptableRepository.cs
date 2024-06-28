using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Glitch9.ScriptableObjects
{
    public abstract class ScriptableRepository<TRepo, TData, TSelf> : ScriptableResource<TSelf>
        where TRepo : Repository<TData>, new()
        where TData : class, IData<TData>, new()
        where TSelf : ScriptableRepository<TRepo, TData, TSelf>
    {
        [SerializeField, SerializeReference] private TRepo data = new();
        public static TRepo Data => Instance.data ??= new TRepo();
        public static int Count => Data.Count;

        public static TData Get(string id)
        {
            if (LogIfNull()) return null;
            return Data.TryGetValue(id, out TData data) ? data : null;
        }

        public static bool TryGetValue(string id, out TData data)
        {
            if (LogIfNull())
            {
                data = null;
                return false;
            }
            return Data.TryGetValue(id, out data);
        }

        public static void Add(TData data)
        {
            if (LogIfNull()) return;
            Data.Add(data.Id, data);
            Instance.Save();
        }

        public static void Remove(TData data)
        {
            if (LogIfNull()) return;
            Data.Remove(data.Id);
            Instance.Save();
        }
        
        public static void Remove(string id)
        {
            if (LogIfNull()) return;
            Data.Remove(id);
            Instance.Save();
        }

        public static void Clear()
        {
            if (LogIfNull()) return;
            Data.Clear();
            Instance.Save();
        }

        public static List<TData> ToList()
        {
            return Data.Values.ToList();
        }

        public static void RemoveInvalidEntries()
        {
            if (LogIfNull()) return;
            Data.RemoveAll(kvp => kvp.Value == null);
        }

        public static async void BackupToJsonFile(string path)
        {
            if (LogIfNull()) return;
            if (Data.Count == 0) return;
            string jsonString = JsonConvert.SerializeObject(Data, JsonUtils.DefaultSettings);
            if (string.IsNullOrEmpty(jsonString)) return;
            await System.IO.File.WriteAllTextAsync(path, jsonString);
        }

        public static async void RestoreFromJsonFile(string path)
        {
            if (LogIfNull()) return;
            string jsonString = await System.IO.File.ReadAllTextAsync(path);
            Repository<TData> data = JsonConvert.DeserializeObject<Repository<TData>>(jsonString, JsonUtils.DefaultSettings);
            // add logs to cache, don't replace
            if (!data.IsNullOrEmpty()) Data.AddRange(data);
        }
        
        protected static bool LogIfNull()
        {
            if (Data == null)
            {
                string dataName = typeof(TData).Name;
                string repoName = Instance.GetType().Name;
                Debug.LogError($"There was an error while trying to access {dataName} list in ScriptableObject - {repoName}. Check the ScriptableObject file for errors.");
                return true;
            }
            return false;
        }
    }
}