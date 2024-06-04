using System;
using System.Text.RegularExpressions;


namespace Glitch9.Internal.Git
{
    public class GitVersion : Version
    {
        /*
             Version.txt looks like below
             Version: 1.4.3 (major, minor, patch)
             Build: 456
             Release: 2023-11-01
        */

        public static GitVersion CreateCurrentVersion(string projectName)
        {
            string envPrefix = CreateGithubEnvPrefix(projectName);
            string versionTag = Environment.GetEnvironmentVariable(envPrefix) ?? null;
            if (versionTag == null) return new GitVersion();
            return new GitVersion(projectName, versionTag);
        }

        public static string CreateGithubEnvPrefix(string projectName)
        {
            return string.Format(ENV_GITHUB_PROJECT_VERSION_PREFIX, projectName);
        }

        private const string VERSION_TAG_PATTERN = @"v(\d+)\.(\d+)\.(\d+)-build-(\d+)-(\d{4}-\d{2}-\d{2})";
        private const string VERSION_TAG_FORMAT = "v{0}.{1}.{2}-build-{3}-{4}";
        private const string VERSION_TAG_INFO_FORMAT = "Version {0}.{1}.{2} Build {3} Date {4}";
        private const string ENV_GITHUB_PROJECT_VERSION_PREFIX = "GITHUB_{0}";

        public bool IsValid { get; private set; }
        private readonly string _projectName;

        public GitVersion()
        {
        }

        public GitVersion(string projectName, string tag)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentException("Project name cannot be null or empty.", nameof(projectName));
            }

            _projectName = projectName.ToUpper();

            Match match = Regex.Match(tag, VERSION_TAG_PATTERN);
            if (match.Success && match.Groups.Count == 6)
            {
                IsValid = true;
                Major = int.Parse(match.Groups[1].Value);
                Minor = int.Parse(match.Groups[2].Value);
                Patch = int.Parse(match.Groups[3].Value);
                Build = int.Parse(match.Groups[4].Value);
                ReleaseDate = new UnixTime(match.Groups[5].Value);

                string envPrefix = CreateGithubEnvPrefix(_projectName);
                Environment.SetEnvironmentVariable(envPrefix, tag);
            }
            else
            {
                throw new FormatException("The provided tag does not match the expected pattern.");
            }
        }

        public string CreateTag()
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            return CreateTag(currentDate);
        }

        public string CreateTag(string dateString)
        {
            return string.Format(VERSION_TAG_FORMAT, Major, Minor, Patch, Build, dateString);
        }

        public string CreateTagInfo(string commitMessage = null)
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string cm = string.Format(VERSION_TAG_INFO_FORMAT, Major, Minor, Patch, Build, currentDate);
            if (!string.IsNullOrEmpty(commitMessage)) cm += $": {commitMessage}";
            return cm;
        }

        public string CreateUpdatedTag(VersionIncrement increment)
        {
            Increase(increment);

            string newTag = CreateTag();
            string envPrefix = CreateGithubEnvPrefix(_projectName);
            Environment.SetEnvironmentVariable(envPrefix, newTag);

            return newTag;
        }
    }
}