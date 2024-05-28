using System;
using UnityEngine;
using Version = Glitch9.Version;

namespace Glitch9
{
    [CreateAssetMenu(fileName = "PackageExporter", menuName = "Glitch9/Package Exporter")]
    public class PackageExporter : ScriptableObject
    {
        [SerializeField] private string packageName;
        [SerializeField] private string buildPath;
        [SerializeField] private Version version;
        [SerializeField] private string[] assetPaths;

        public Version Version => version;
        public string PackageName => packageName;

        public void IncreasePatchVersion(int amount = 1)
        {
            version.Patch += amount;
        }

        public void SetBuildNumber(int buildNumber)
        {
            version.Build = buildNumber;
        }
    }
}