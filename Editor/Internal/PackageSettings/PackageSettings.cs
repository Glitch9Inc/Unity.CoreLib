using UnityEngine;

namespace Glitch9.Internal
{
    [CreateAssetMenu(fileName = nameof(PackageSettings), menuName = UnityMenu.Utility.CREATE_PACKAGE_SETTINGS, order = UnityMenu.Utility.ORDER_PACKAGE_SETTINGS)]
    public class PackageSettings : ScriptableObject
    {
        [SerializeField] private string packageName;
        [SerializeField] private string buildPath;
        [SerializeField] private Version version;
        [SerializeField] private bool linkPackage;
        [SerializeField] private PackageSettings linkedPackage;
        [SerializeField] private Glitch9Library[] libraries;
        [SerializeField] private bool define = false;
        [SerializeField] private string defineSymbol;

        public Version Version => version;
        public string PackageName => packageName;
        public string DefineSymbol => defineSymbol;
        public bool Define => define;


        public void IncreasePatchVersion(int amount = 1)
        {
            if (!linkPackage || linkedPackage == null)
            {
                version.Patch += amount;
            }
            else
            {
                // find a bigger version
                if (linkedPackage.Version >= version)
                {
                    linkedPackage.IncreasePatchVersion(amount);
                    version = linkedPackage.Version;
                }
                else
                {
                    version.Patch += amount;
                    linkedPackage.SetVersion(version);
                }
            }         
        }

        public void SetVersion(Version version)
        {
            this.version = version;
        }

        public void SetBuildNumber(int buildNumber)
        {
            version.Build = buildNumber;
        }
    }
}