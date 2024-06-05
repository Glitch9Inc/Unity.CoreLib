using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Debug = UnityEngine.Debug;

namespace Glitch9.Internal.Git
{
    public class GitManager
    {
        #region Status Checks

        public bool PullAvailable => _remoteVersion > _localVersion && _remoteVersion.Build != 0 && _localVersion.Build != 0;
        public bool PushAvailable => _remoteVersion < _localVersion && _remoteVersion.Build != 0 && _localVersion.Build != 0;

        #endregion


        public event Action<GitOutput> OnGitOutput;

        private readonly string _gitBranch;
        private readonly string _localDir;
        private readonly string _gitUrl; // should look like https://github.com/amaiichigopurin/CodeqoShared.git
        private readonly string _repoName;

        private readonly Action _onRepaint;


        private GitVersion _localVersion;
        private GitVersion _remoteVersion;
        public GitVersion LocalVersion => _localVersion;
        public GitVersion RemoteVersion => _remoteVersion;

        public GitManager(string repoName, string gitUrl, string gitBranch, string localDir, Action onRepaint)
        {
            if (string.IsNullOrEmpty(repoName))
            {
                throw new ArgumentException("Project name cannot be null or empty.", nameof(repoName));
            }

            if (string.IsNullOrEmpty(localDir))
            {
                throw new ArgumentException("Working directory cannot be null or empty.", nameof(localDir));
            }

            if (string.IsNullOrEmpty(gitUrl))
            {
                throw new ArgumentException("Git URL cannot be null or empty.", nameof(gitUrl));
            }

            if (string.IsNullOrEmpty(gitBranch))
            {
                throw new ArgumentException("Git branch cannot be null or empty.", nameof(gitBranch));
            }

            _localDir = localDir;
            _gitUrl = gitUrl;
            _gitBranch = gitBranch;
            _repoName = repoName;
            _onRepaint = onRepaint;
        }

        public async Task InitializeAsync()
        {
            _localVersion = GitVersion.CreateCurrentVersion(_repoName);

            try
            {
                IResult iResult = await InitGitRepositoryAsync();
                if (iResult.IsFailure) return;
                iResult = await PullVersionTagAsync();
                if (iResult.IsFailure) return;
                await ValidateBranch();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                _onRepaint?.Invoke();
            }
        }

        private async Task<IResult> InitGitRepositoryAsync()
        {
            string gitDirectory = Path.Combine(_localDir, ".git");
            if (Directory.Exists(gitDirectory))
            {
                Debug.Log("Git repository already initialized.");
                return Result.Success();
            }

            try
            {
                IResult iResult = await RunGitCommandAsync("init", false);
                if (iResult.IsFailure) return iResult;
                if (Directory.Exists(gitDirectory)) return Result.Success();
                return Result.Fail("Failed to initialize Git repository.");
            }
            catch (Exception ex)
            {
                string error = $"Failed to initialize Git repository:  {ex.Message}";
                Debug.LogError(error);
                OnGitOutput?.Invoke(new GitOutput(error, GitOutputStatus.Error));
            }

            return Result.Fail("Failed to initialize Git repository.");
        }

        public async Task<IResult> PullVersionTagAsync()
        {
            Debug.Log("Getting version info...");

            // Set the current branch to track the remote branch
            IResult iResult = await RunGitCommandAsync($"branch --set-upstream-to=origin/{_gitBranch}", false);
            if (iResult.IsFailure) return iResult;

            // Fetch the tags from the remote repository
            iResult = await RunGitCommandAsync("fetch --tags", false);
            if (iResult.IsFailure) return iResult; ;

            // Get the latest tag from the fetched tags
            iResult = await RunGitCommandAsync("describe --tags --abbrev=0", false);
            if (iResult.IsFailure || iResult is not Result result) return iResult; ;
            string latestTag = result.Message.Trim();

            _remoteVersion ??= new GitVersion();

            // Check if the latestTag is not null or empty after trimming
            if (!string.IsNullOrEmpty(latestTag))
            {
                _remoteVersion = new GitVersion(_repoName, latestTag);
                Debug.Log($"Latest version tag pulled: {_remoteVersion}");
            }
            else
            {
                Debug.LogWarning("No valid version tag found.");
                _remoteVersion = new GitVersion();
            }

            return Result.Success();
        }

        private async Task<IResult> ValidateBranch()
        {
            IResult iResult = await RunGitCommandAsync("remote get-url origin", false);
            if (iResult.IsFailure || iResult is not Result result) return iResult;

            string currentBranch = result.Message;
            if (string.IsNullOrEmpty(currentBranch)) return Result.Fail("Failed to get current branch.");
            currentBranch = currentBranch.Trim();
            Debug.Log($"Current branch: {currentBranch}");

            if (currentBranch != _gitBranch)
            {
                Debug.LogWarning($"Current branch ({currentBranch}) does not match the expected branch ({_gitBranch}).");
                Debug.Log("Attempting to checkout branch...");
                return await RunGitCommandAsync($"checkout {_gitBranch}", false);
            }

            return Result.Success();
        }

        public async Task<IResult> PushAsync(string commitMessage, VersionIncrement versionInc, bool force = false)
        {
            string pushCommand = $"push origin {_gitBranch}";
            if (force) pushCommand += " --force";

            _remoteVersion ??= new GitVersion();
            string cm = _remoteVersion.CreateTagInfo(commitMessage);

            // Commit changes
            IResult iResult = await RunGitCommandAsync("add .", true);
            if (iResult.IsFailure) return iResult;

            iResult = await RunGitCommandAsync("commit -m \"" + cm + "\"", true);
            if (iResult.IsFailure) return iResult;

            // Push changes
            iResult = await RunGitCommandAsync(pushCommand, true);
            if (iResult.IsFailure) return iResult;

            iResult = await PushVersionTagAsync(versionInc);
            if (iResult.IsFailure) return iResult;
            
            _localVersion = _remoteVersion;
            return Result.Success();
        }

        /// <summary>
        /// git tag -a "tag" -m "tagInfo"
        /// git push origin--tags
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> PushVersionTagAsync(VersionIncrement versionInc = VersionIncrement.Patch)
        {
            _remoteVersion ??= new GitVersion();
            
            string tag = _remoteVersion.CreateUpdatedTag(versionInc);
            string tagInfo = _remoteVersion.CreateTagInfo();
            
            IResult iResult = await RunGitCommandAsync($"tag -a {tag} -m \"{tagInfo}\"", false);
            if (iResult.IsFailure) return iResult;

            iResult = await RunGitCommandAsync("push origin --tags", false);
            if (iResult.IsFailure) return iResult;
            
            _localVersion = _remoteVersion;
            return Result.Success();
        }

        public async void ConfigureLocalCoreAutoCRLFAsync(bool value) => await RunGitCommandAsync($"config core.autocrlf {(value ? "true" : "false")}", true);
        public async void ConfigureGlobalCoreAutoCRLFAsync(bool value) => await RunGitCommandAsync($"config --global core.autocrlf {(value ? "true" : "false")}", true);

        public async Task<IResult> RunGitCommandAsync(string command, bool logSuccessMessage)
        {
            Debug.Log($"Running Git command: {command}");
            OnGitOutput?.Invoke(new GitOutput(command));

            if (string.IsNullOrEmpty(_localDir) || string.IsNullOrEmpty(_gitUrl))
            {
                Debug.LogError("GitManager is not initialized properly.");
                return null;
            }

            ProcessStartInfo startInfo = new("git", command)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = _localDir
            };

            try
            {
                using Process process = new() { StartInfo = startInfo };
                StringBuilder outputBuilder = new();
                StringBuilder errorBuilder = new();

                process.OutputDataReceived += (sender, args) =>
                {
                    if (args.Data != null)
                    {
                        outputBuilder.AppendLine(args.Data);
                        HandleResult(command, args.Data, GitOutputStatus.Success, false);
                    }
                };

                process.ErrorDataReceived += (sender, args) =>
                {
                    if (args.Data != null)
                    {
                        errorBuilder.AppendLine(args.Data);
                        HandleResult(command, args.Data, GitOutputStatus.Error, false);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    string error = $"Git command Failed with exit code {process.ExitCode}";
                    return HandleResult(command, error, GitOutputStatus.Error);
                }

                string output = outputBuilder.ToString().Trim();
                return HandleResult(command, output, GitOutputStatus.Unknown, true, logSuccessMessage);
            }
            catch (Exception ex)
            {
                string error = $"Git command Failed: {ex.Message}";
                return HandleResult(command, error, GitOutputStatus.Error);
            }
        }

        private IResult HandleResult(string command, string output, GitOutputStatus status = GitOutputStatus.Unknown, bool triggerEventHandler = true, bool logSuccessMessage = false)
        {
            if (status == GitOutputStatus.Unknown)
            {
                status = GitEditorUtils.ParseStatus(output);
            }

            if (triggerEventHandler)
            {
                OnGitOutput?.Invoke(new GitOutput(output, status));
            }

            if (status == GitOutputStatus.Error || status == GitOutputStatus.Fatal)
            {
                return Result.Fail(output);
            }

            if (status == GitOutputStatus.Success && logSuccessMessage)
            {
                string successMessage = $"Git command \"{command}\" executed successfully.";
                OnGitOutput?.Invoke(new GitOutput(successMessage, GitOutputStatus.Completed));
            }

            return Result.Success(output);
        }
    }
}
