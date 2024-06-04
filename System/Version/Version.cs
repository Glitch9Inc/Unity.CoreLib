using System;
using UnityEngine;

namespace Glitch9
{
    [Serializable]
    public class Version : IEquatable<Version>, IComparable<Version>
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

        public UnixTime ReleaseDate
        {
            get => releaseDate;
            set => releaseDate = value;
        }


        public Version()
        {
        }

        public Version(string version)
        {
            string[] parts = version.Split('.');
            if (parts.Length != 3) throw new ArgumentException("Invalid version format. Must be in the format 'major.minor.patch'.");

            Major = int.Parse(parts[0]);
            Minor = int.Parse(parts[1]);
            Patch = int.Parse(parts[2]);
        }

        public void Increase(VersionIncrement increment)
        {
            switch (increment)
            {
                case VersionIncrement.Major:
                    Major++;
                    Minor = 0;
                    Patch = 0;
                    break;
                case VersionIncrement.Minor:
                    Minor++;
                    Patch = 0;
                    break;
                case VersionIncrement.Patch:
                    Patch++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(increment), "Unsupported version increment specified.");
            }
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

        public bool Equals(Version other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return major == other.major && minor == other.minor && patch == other.patch && build == other.build && releaseDate == other.releaseDate;
        }

        public int CompareTo(Version other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            int majorComparison = major.CompareTo(other.major);
            if (majorComparison != 0) return majorComparison;
            int minorComparison = minor.CompareTo(other.minor);
            if (minorComparison != 0) return minorComparison;
            int patchComparison = patch.CompareTo(other.patch);
            if (patchComparison != 0) return patchComparison;
            int buildComparison = build.CompareTo(other.build);
            if (buildComparison != 0) return buildComparison;
            return releaseDate.CompareTo(other.releaseDate);
        }

        public static bool operator >(Version version1, Version version2)
        {
            return version1.CompareTo(version2) > 0;
        }

        public static bool operator <(Version version1, Version version2)
        {
            return version1.CompareTo(version2) < 0;
        }

        public static bool operator >=(Version version1, Version version2)
        {
            return version1.CompareTo(version2) >= 0;
        }

        public static bool operator <=(Version version1, Version version2)
        {
            return version1.CompareTo(version2) <= 0;
        }

        public static bool operator ==(Version version1, Version version2)
        {
            if (ReferenceEquals(version1, version2))
                return true;
            if (ReferenceEquals(version1, null))
                return false;
            if (ReferenceEquals(version2, null))
                return false;
            return version1.Equals(version2);
        }

        public static bool operator !=(Version version1, Version version2)
        {
            return !(version1 == version2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Version))
                return false;
            return Build == ((Version)obj).Build;
        }

        public override int GetHashCode()
        {
            return Build.GetHashCode();
        }
    }
}