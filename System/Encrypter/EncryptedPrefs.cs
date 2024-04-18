using Newtonsoft.Json;
using UnityEngine;

namespace Glitch9
{
    public class EncryptedPrefs
    {
        public static void Set<T>(string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            string encrypted = Encrypter.EncryptString(json);
            PlayerPrefs.SetString(key, encrypted);
            PlayerPrefs.Save();
        }

        public static T Get<T>(string key, T defaultValue = default(T))
        {
            if (!PlayerPrefs.HasKey(key)) return defaultValue;
            string encrypted = PlayerPrefs.GetString(key);
            string json = Encrypter.DecryptString(encrypted);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
