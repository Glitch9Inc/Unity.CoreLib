using System;
using UnityEngine;

namespace Glitch9
{
    [Serializable]
    public class Version
    {
        [SerializeField] private int major;
        [SerializeField] private int minor;
        [SerializeField] private int patch;
        [SerializeField] private int build;
        [SerializeField] private long releaseDate;

        public int Major
        {
            get => major;
            set
            {
                major = value;
                build = CalcBuildNumber(this);
                releaseDate = UnixTime.Now;
            }
        }

        public int Minor
        {
            get => minor;
            set
            {
                minor = value;
                build = CalcBuildNumber(this);
                releaseDate = UnixTime.Now;
            }
        }

        public int Patch
        {
            get => patch;
            set
            {
                patch = value;
                build = CalcBuildNumber(this);
                releaseDate = UnixTime.Now;
            }
        }

        public int Build
        {
            get => build;
            set
            {
                build = value;
                major = build / 10000;
                minor = (build % 10000) / 100;
                patch = build % 100;
                releaseDate = UnixTime.Now;
            }
        }

        public UnixTime ReleaseDate => releaseDate;

        public Version(string version)
        {
            string[] parts = version.Split('.');
            if (parts.Length != 3) throw new ArgumentException("Invalid version format. Must be in the format 'major.minor.patch'.");

            Major = int.Parse(parts[0]);
            Minor = int.Parse(parts[1]);
            Patch = int.Parse(parts[2]);
        }


        // Utility methods
        public static int CalcBuildNumber(Version version)
        {
            if (version == null) return -1;
            return CalcBuildNumber(version.Major, version.Minor, version.Patch);
        }

        public static int CalcBuildNumber(int major, int minor, int patch)
        {
            return major * 10000 + minor * 100 + patch;
        }
    }
}