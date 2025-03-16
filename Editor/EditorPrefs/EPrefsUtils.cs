using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    internal class EPrefsUtils
    {
        internal static void HandleFailedDeserialization(string prefsKey, string paramName, Exception e)
        {
            string json = EditorPrefs.GetString(prefsKey, string.Empty);
            Debug.LogError($"Error occurred while deserializing {paramName}: {e.Message}");
            Debug.LogError($"Failed JSON: {json}");

            if (!string.IsNullOrWhiteSpace(json))
            {
                // Create backup to a file
                string backupPath = Path.Combine(Application.persistentDataPath, $"EPrefs_{prefsKey}.json");
                Debug.LogError($"Creating JSON backup at: {backupPath}");
                File.WriteAllText(backupPath, json);
            }

            PlayerPrefs.DeleteKey(prefsKey);
        }
    }
}