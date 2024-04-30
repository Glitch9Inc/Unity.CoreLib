using UnityEngine;

namespace Glitch9
{
    [CreateAssetMenu(fileName = "PackageExporter", menuName = "Glitch9/Package Exporter")]
    public class PackageExporter : ScriptableObject
    {
        [SerializeField] private string packageName;
        [SerializeField] private string buildPath;
        [SerializeField] private int versionNumber;
        [SerializeField] private string[] assetPaths;
    }
}