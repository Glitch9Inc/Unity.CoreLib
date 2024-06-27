#if UNITY_EDITOR && UNITY_ADDRESSABLES
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Glitch9
{
    public static class AddressableUtils
    {
        public static void CreateGroup(string groupName)
        {
            // Get the Addressable Asset Settings which is the entry point to the Addressable Asset System.
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

            // Check if the group already exists.
            if (settings.FindGroup(groupName) != null) return;

            // Create a new group in the Addressable Asset Settings.
            AddressableAssetGroup group = settings.CreateGroup(groupName, false, false, true, null);

            Debug.Log($"Addressable group '{groupName}' created.");
        }

        public static void AddAsset(string groupName, string labelName, string assetPath, bool createGroup = true)
        {
            // Get the Addressable Asset Settings which is the entry point to the Addressable Asset System.
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

            // Check if the group already exists.
            AddressableAssetGroup group = settings.FindGroup(groupName);
            if (group == null)
            {
                if (createGroup) CreateGroup(groupName);
                else return;
            }

            // Add assets to the group in the Addressable Asset Settings.
            string guid = AssetDatabase.AssetPathToGUID(assetPath);
            AddressableAssetEntry entry = settings.CreateOrMoveEntry(guid, group);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(assetPath);
            entry.address = fileName;
            entry.SetLabel(labelName, true, true, true);

            Debug.Log($"Asset '{assetPath}' added to addressable group '{groupName}'.");
        }
    }
}
#endif
