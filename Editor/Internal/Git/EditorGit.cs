using Glitch9.ExtendedEditor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Glitch9.Internal.Git
{
    public class EditorGit
    {
        internal bool IsInitialized { get; set; } = false;

        private GitManager _git;
        private Vector2 _scrollPosition;
        private List<GitOutput> _gitOutputs;
        private Action _onRepaint;

        private string _gitUrl;
        private string _repoName;
        private string _gitBranch;
        private string _localDir;

        private string commitMessage;
        private EPrefs<string> _commitMessage;

        private bool saveCommitMessage;
        private EPrefs<bool> _saveCommitMessage;

        private string _commandLine;
        private int _gitOutputUpdated = 0;
        private bool _isInitializing = false;


        private readonly Dictionary<GitOutputStatus, Color> _gitOutputColors = new()
        {
            { GitOutputStatus.Success, Color.blue },
            { GitOutputStatus.Warning, ExColor.firebrick },
            { GitOutputStatus.Hint, Color.magenta },
            { GitOutputStatus.Error, Color.red },
            { GitOutputStatus.Fatal, Color.red },
            { GitOutputStatus.Completed, ExColor.teal },
        };

        private static class Strings
        {
            internal const string MISSING_URL_OR_DIR = "Git URL and Local Directory must be set.";
            internal const string NEW_VERSION_AVAILABLE = "New version available. Please download the latest version";
            internal const string UP_TO_DATE = "You are up to date with the latest version.";
        }

        private GUIStyle GetColoredStyle(Color color)
        {
            return new(EditorStyles.wordWrappedLabel)
            {
                normal = { textColor = color }
            };
        }

        internal async void InitializeAsync(string gitUrl, string gitBranch, string localDir, Action onRepaint)
        {
            if (IsInitialized || _isInitializing) return;
            _isInitializing = true;

            if (string.IsNullOrEmpty(gitUrl) || string.IsNullOrEmpty(localDir))
            {
                Debug.LogError(Strings.MISSING_URL_OR_DIR);
                return;
            }

            string commitMessageKey = $"{gitUrl}-{gitBranch}-commitMessage";
            _commitMessage = new EPrefs<string>(commitMessageKey, string.Empty);
            commitMessage = _commitMessage.Value;

            string saveCommitMessageKey = $"{gitUrl}-{gitBranch}-saveCommitMessage";
            _saveCommitMessage = new EPrefs<bool>(saveCommitMessageKey, false);
            saveCommitMessage = _saveCommitMessage.Value;

            _gitOutputs = new List<GitOutput>();
            _gitUrl = gitUrl;
            _repoName = gitUrl.Substring(gitUrl.LastIndexOf('/') + 1);
            _gitBranch = gitBranch;
            _onRepaint = onRepaint;
            _localDir = localDir;

            _git = new GitManager(_repoName, gitUrl, gitBranch, localDir, onRepaint);
            _git.OnGitOutput += OnGitOutput;

            await _git.InitializeAsync();
            IsInitialized = true;
            _isInitializing = false;
        }

        private void OnGitOutput(GitOutput output)
        {
            //if (_gitOutputs.Contains(output)) return;

            _gitOutputs.Add(output);
            _gitOutputUpdated++;
            GoToBottom();
            _onRepaint?.Invoke();
        }

        internal void OnDestroy()
        {
            if (saveCommitMessage)
            {
                _commitMessage.Value = commitMessage;
            }

            _saveCommitMessage.Value = saveCommitMessage;
        }

        internal void DrawGit()
        {
            GUILayout.BeginVertical(EGUI.Box(3, 7));
            {
                GUILayout.Label($"{_repoName}-{_gitBranch} (Output {_gitOutputUpdated})", EditorStyles.boldLabel);
                GUILayout.Space(3);
                DrawVersionInfo();
                DrawGitPanel();

                // Menu Buttons
                DrawPushAndPullMenu();
                DrawRemoteMenu();
                DrawBranchMenu();
                DrawDebugMenu();
            }
            GUILayout.EndVertical();
        }

        private void DrawVersionInfo()
        {
            if (_git == null || _git.LocalVersion == null || _git.RemoteVersion == null) return;

            GUILayout.BeginVertical(EGUI.box);
            {
                GUILayout.Label($"Local: {_git.LocalVersion.CreateTagInfo()}");
                GUILayout.Label($"Remote: {_git.RemoteVersion.CreateTagInfo()}");

                if (_git.PullAvailable)
                {
                    GUILayout.Label(Strings.NEW_VERSION_AVAILABLE, GetColoredStyle(Color.red));
                }
                else
                {
                    GUILayout.Label(Strings.UP_TO_DATE, GetColoredStyle(Color.blue));
                }
            }
            GUILayout.EndVertical();
        }

        private void DrawGitPanel()
        {
            GUILayout.BeginVertical(EGUI.box, GUILayout.MinHeight(500), GUILayout.ExpandHeight(true));
            {
                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

                foreach (GitOutput gitOutput in _gitOutputs)
                {
                    DrawGitOutput(gitOutput);
                }

                GUILayout.EndScrollView();

                GUILayout.FlexibleSpace();
                DrawMainMenu();
                DrawCommandLineInput();
            }
            GUILayout.EndVertical();
        }

        private void DrawCommitMessageTextField()
        {
            GUILayout.BeginVertical(EGUI.box);
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Commit Message");
                    GUILayout.FlexibleSpace();
                    saveCommitMessage = GUILayout.Toggle(saveCommitMessage, "Save");
                }
                GUILayout.EndHorizontal();

                commitMessage = EditorGUILayout.TextField(commitMessage);
            }
            GUILayout.EndVertical();
        }

        private void DrawMainMenu()
        {
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Git Status (status)"))
                {
                    RunGitCommandsAsync("status");
                }

                if (GUILayout.Button("Remote Status (remove -v)"))
                {
                    RunGitCommandsAsync("remote -v");
                }

                if (GUILayout.Button("Fix 'index.lock'"))
                {
                    RunGitCommandsAsync("update-index --refresh", "update-index --really-refresh");
                }

                if (GUILayout.Button("Open Repo"))
                {
                    Application.OpenURL(_gitUrl);
                }
            }
            GUILayout.EndHorizontal();
        }

        private void DrawPushAndPullMenu()
        {
            EditorGUILayout.LabelField("Push / Pull", EditorStyles.boldLabel);

            DrawCommitMessageTextField();

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Upload (commit and push)"))
                {
                    GitEditorUtils.ShowGitVersionSelector((ver) => Push(ver, true));
                }

                if (GUILayout.Button("Force Upload (commit and push -f)"))
                {
                    GitEditorUtils.ShowGitVersionSelector((ver) => Push(ver, false));
                }
            }
            GUILayout.EndHorizontal();


            if (GUILayout.Button("Download (pull)"))
            {
                if (_git.PullAvailable) // check if there is a new version available
                {
                    RunGitCommandsAsync("pull");
                }
                else
                {
                    Debug.Log(Strings.UP_TO_DATE);
                }
            }
        }

        private void DrawRemoteMenu()
        {
            EditorGUILayout.LabelField("Remote Settings", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Remote Add Origin"))
                {
                    // if the url doesn't end with .git, add it
                    if (!_gitUrl.EndsWith(".git")) _gitUrl += ".git";
                    RunGitCommandsAsync($"remote add origin {_gitUrl}");
                }

                if (GUILayout.Button("Remote Set URL"))
                {
                    // if the url doesn't end with .git, add it
                    if (!_gitUrl.EndsWith(".git")) _gitUrl += ".git";
                    RunGitCommandsAsync($"remote set-url origin {_gitUrl}");
                }

                if (GUILayout.Button("Remote Remove"))
                {
                    RunGitCommandsAsync("remote remove origin");
                }
            }
            GUILayout.EndHorizontal();
        }

        private void DrawBranchMenu()
        {
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Current Branch"))
                {
                    RunGitCommandsAsync("rev-parse --abbrev-ref HEAD");
                }

                if (GUILayout.Button("List Branches"))
                {
                    RunGitCommandsAsync("branch -a");
                }

                if (GUILayout.Button("Branch Checkout"))
                {
                    RunGitCommandsAsync("checkout branch-name");
                }

                if (GUILayout.Button("Master => Main"))
                {
                    RunGitCommandsAsync("branch -m master main");
                }
            }
            GUILayout.EndHorizontal();
        }

        private void DrawDebugMenu()
        {
            EditorGUILayout.LabelField("Debug Settings", EditorStyles.boldLabel);

            EditorGitGUI.DrawTrueOrFalseButton("core.autocrlf", _git.ConfigureLocalCoreAutoCRLFAsync);
            EditorGitGUI.DrawTrueOrFalseButton("core.autocrlf (global)", _git.ConfigureGlobalCoreAutoCRLFAsync);
            EditorGitGUI.DrawSetOrUnsetButton("Upstream to origin", SetUpstreamToOrigin);

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Other Commands", GUILayout.Width(240));

                if (GUILayout.Button("Normalize Line Endings"))
                {
                    RunGitCommandsAsync("add --renormalize .");
                }

                if (GUILayout.Button("Push tag"))
                {
                    _git.PushVersionTagAsync();
                }
            }
            GUILayout.EndHorizontal();
        }

        private void SetUpstreamToOrigin(bool isSet)
        {
            if (isSet)
            {
                RunGitCommandsAsync($"branch --set-upstream-to=origin/{_gitBranch}");
            }
            else
            {
                RunGitCommandsAsync($"branch --unset-upstream");
            }
        }

        private void GoToBottom()
        {
            _scrollPosition.y = int.MaxValue;
        }

        private void DrawGitOutput(GitOutput gitOutput)
        {
            if (_gitOutputColors.TryGetValue(gitOutput.Status, out Color color))
            {
                GUILayout.Label(gitOutput.Message, GetColoredStyle(color));
            }
            else
            {
                GUILayout.Label(gitOutput.Message, EditorStyles.wordWrappedLabel);
            }
        }

        private void DrawCommandLineInput()
        {
            GUILayout.BeginHorizontal();
            {
                _commandLine = GUILayout.TextField(_commandLine);

                if (GUILayout.Button(EditorIcons.Enter, GUILayout.Height(18f), GUILayout.Width(30f)))
                {
                    EnterGitCommand();
                    GoToBottom();
                }
            }
            GUILayout.EndHorizontal();
        }

        private async void EnterGitCommand()
        {
            _commandLine = _commandLine.Trim();

            if (_commandLine.StartsWith("git ")) _commandLine = _commandLine.Substring(4); // if it starts with git, remove it
            if (string.IsNullOrEmpty(_commandLine))
            {
                _gitOutputs.Add(new GitOutput("Empty Command"));
                return;
            }
            await _git.RunGitCommandAsync(_commandLine, true, true);
            _commandLine = "";
        }

        private async void RunGitCommandsAsync(params string[] commands)
        {
            if (commands == null || commands.Length == 0)
            {
                _gitOutputs.Add(new GitOutput("Empty Command"));
                return;
            }

            foreach (string command in commands)
            {
                if (string.IsNullOrEmpty(command))
                {
                    _gitOutputs.Add(new GitOutput("Empty Command"));
                    continue;
                }

                IResult iResult = await _git.RunGitCommandAsync(command, true, true);
                if (iResult.IsSuccess) return;
            }
        }

        private async void Push(VersionIncrement versionType, bool force)
        {
            IResult iResult = await _git.PushAsync(commitMessage, versionType, force);
            if (iResult.IsSuccess && !saveCommitMessage) commitMessage = "";
        }
    }
}