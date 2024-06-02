using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Glitch9.Internal
{
    public class VersionUtils
    {
        private static readonly Dictionary<string, Version> _packageVersions = new();
        public static Version GetPackageVersion(string packageName)
        {

            if (_packageVersions.TryGetValue(packageName, out Version version))
            {
                return version;
            }

            // Get all PackageExporter assets and find the one with the matching package name
            string[] packageExporters = AssetDatabase.FindAssets("t:PackageExporter");
            foreach (string packageExporterGuid in packageExporters)
            {
                PackageExporter packageExporter = AssetDatabase.LoadAssetAtPath<PackageExporter>(AssetDatabase.GUIDToAssetPath(packageExporterGuid));
                if (packageExporter.PackageName == packageName)
                {
                    version = packageExporter.Version;
                    break;
                }
            }

            if (version == null)
            {
                Debug.LogError($"Could not find PackageExporter for package {packageName}");
                return null;
            }
            _packageVersions.Add(packageName, version);
            return version;
        }
    }
}