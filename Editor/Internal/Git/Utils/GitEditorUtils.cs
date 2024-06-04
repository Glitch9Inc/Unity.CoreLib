using System;

namespace Glitch9.Internal.Git
{
    internal class GitEditorUtils
    {
        internal static void ShowGitVersionSelector(Action<VersionIncrement> callback)
        {
            string popupMessage = "Are you sure you want to force push?";
            string popupDescription = "Version type is used to determine the version number. \n" +
                                      "Patch: 1.0.0 -> 1.0.1 \n" +
                                      "Minor: 1.0.0 -> 1.1.0 \n" +
                                      "Major: 1.0.0 -> 2.0.0 \n";

            GitVersionSelector.Show(popupMessage, popupDescription, VersionIncrement.Patch, callback);
        }

        internal static GitOutputStatus ParseStatus(string output)
        {
            if (output.StartsWith(GitEvents.HINT))
            {
                return GitOutputStatus.Hint;
            }

            if (output.StartsWith(GitEvents.WARNING))
            {
                return GitOutputStatus.Warning;
            }

            if (output.StartsWith(GitEvents.ERROR))
            {
                return GitOutputStatus.Error;
            }

            if (output.StartsWith(GitEvents.FATAL))
            {
                return GitOutputStatus.Fatal;
            }

            return GitOutputStatus.Success;
        }
    }
}