using UnityEngine;

namespace Glitch9.Internal
{
    [CreateAssetMenu(fileName = nameof(PackageExporter), menuName = "Glitch9/Package Exporter")]
    public class PackageExporter : ScriptableObject
    {
        [SerializeField] private string packageName;
        [SerializeField] private string buildPath;
        [SerializeField] private Version version;
        [SerializeField] private Glitch9Library[] libraries;

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