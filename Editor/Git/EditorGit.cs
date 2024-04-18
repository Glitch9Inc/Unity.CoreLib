using Glitch9.ExEditor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Glitch9.IO.Git
{
    internal class EditorGit
    {
        internal bool IsInitialized { get; set; } = false;

        private GitManager _git;
        private Vector2 _scrollPosition;
        private List<GitOutput> _gitOutputs;

        private string _gitUrl;
        private string _repoName;
        private string _branchName;

        private string _commandLine;
        private int _gitOutputUpdated = 0;
        private bool _isInitializing = false;

        private readonly Dictionary<GitOutputStatus, Color> _gitOutputColors = new()
        {
            { GitOutputStatus.Success, Color.blue },
            { GitOutputStatus.Warning, ExColor.firebrick },
            { GitOutputStatus.Hint, Color.magenta },
            { GitOutputStatus.Error, ExColor.charcoal },
            { GitOutputStatus.RealError, ExColor.orange },
            { GitOutputStatus.Fatal, Color.red },
        };

        private static class Texts
        {
            internal const string MISSING_URL_OR_DIR = "Git URL and Local Directory must be set.";
            internal const string NEW_VERSION_AVAILABLE = "New version available. Please download the latest version";
            internal const string UP_TO_DATE = "You are up to date with the latest version.";
        }

        internal async void InitializeAsync(string gitUrl, string gitBranch, string localDir, bool reinitialize = false)
        {
            if ((IsInitialized || _isInitializing) && !reinitialize) return;
            _isInitializing = true;

            if (string.IsNullOrEmpty(gitUrl) || string.IsNullOrEmpty(localDir))
            {
                Debug.LogError(Texts.MISSING_URL_OR_DIR);
                return;
            }

            _gitOutputs = new List<GitOutput>();

            _gitUrl = gitUrl;
            _repoName = gitUrl.Substring(gitUrl.LastIndexOf('/') + 1);
            _branchName = gitBranch;
            
            _git = new GitManager(_repoName, gitUrl, gitBranch, localDir);

            _git.OnGitOutput += (output) =>
            {
                _gitOutputs.Add(output);
                _gitOutputUpdated++;
                GoToBottom();
            };

            await _git.InitializeAsync();
            IsInitialized = true;
            _isInitializing = false;
        }

        internal void DrawGit()
        {
            GUILayout.BeginVertical(ExGUI.Box(3, 7));
            {
                GUILayout.Label($"{_repoName}-{_branchName} (Output {_gitOutputUpdated})", EditorStyles.boldLabel);
                GUILayout.Space(3);
                DrawVersionInfo();
                DrawGitPanel();
                DrawButtons();
            }
            GUILayout.EndVertical();
        }

        private void DrawVersionInfo()
        {
            GUILayout.BeginVertical(ExGUI.box);
            {
                GUILayout.Label($"Local: {_git.LocalVersion.CreateTagInfo()}");
                GUILayout.Label($"Remote: {_git.RemoteVersion.CreateTagInfo()}");

                if (_git.PullAvailable)
                {
                    GUILayout.Label(Texts.NEW_VERSION_AVAILABLE, ExEditorStyles.centeredRedMiniLabel);
                }
                else
                {
                    GUILayout.Label(Texts.UP_TO_DATE, ExEditorStyles.centeredBlueMiniLabel);
                }
            }
            GUILayout.EndVertical();
        }

        private void DrawGitPanel()
        {
            GUILayout.BeginVertical(ExGUI.box, GUILayout.MinHeight(500), GUILayout.ExpandHeight(true));
            {
                _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

                foreach (GitOutput gitOutput in _gitOutputs)
                {
                    DrawGitOutput(gitOutput);
                }

                GUILayout.EndScrollView();

                GUILayout.FlexibleSpace();
                DrawCommandLineInput();
            }
            GUILayout.EndVertical();
        }

        private void DrawButtons()
        {
            if (GUILayout.Button("Download (Git Pull)"))
            {
                Pull();
            }

            if (GUILayout.Button("Upload (Git Push)"))
            {
                if (ExGUI.Ask("Are you sure you want to upload to the git repository?"))
                {
                    string popupMessage = "Please select the version type.";
                    string popupDescription = "Version type is used to determine the version number. \n" +
                                              "Patch: 1.0.0 -> 1.0.1 \n" +
                                              "Minor: 1.0.0 -> 1.1.0 \n" +
                                              "Major: 1.0.0 -> 2.0.0 \n";

                    VersionTypeSelector.Show(popupMessage, popupDescription, VersionIncrement.Patch, Push);
                }
            }

            if (GUILayout.Button("Git Status"))
            {
                Status();
            }
        }

        internal void DrawRemoteMenu()
        {
            ExGUILayout.Foldout("Remote Menu", () =>
            {
                if (GUILayout.Button("Remote Add Origin"))
                {
                    EnterGitCommand($"remote add origin {_gitUrl}");
                }

                if (GUILayout.Button("Remote Set URL"))
                {
                    EnterGitCommand($"remote set-url origin {_gitUrl}");
                }

                if (GUILayout.Button("Remote Remove"))
                {
                    EnterGitCommand("remote remove origin");
                }

                if (GUILayout.Button("Remote Information"))
                {
                    EnterGitCommand("remote -v");
                }
            });
        }

        internal void DrawDebugMenu()
        {
            ExGUILayout.Foldout("Debug Menu",() => {
                if (GUILayout.Button("Commit"))
                {
                    Commit();
                }

                if (GUILayout.Button("Normalize Line Endings"))
                {
                    if (ExGUI.Ask("Are you sure you want to continue?"))
                    {
                        NormalizeLineEndings();
                    }
                }

                if (GUILayout.Button("Configure core.autocrlf Globally [true]"))
                {
                    if (ExGUI.Ask("Are you sure you want to continue?"))
                    {
                        ConfigureAutoCRLF(true);
                    }
                }

                if (GUILayout.Button("Configure core.autocrlf Globally [false]"))
                {
                    if (ExGUI.Ask("Are you sure you want to continue?"))
                    {
                        ConfigureAutoCRLF(false);
                    }
                }

                if (GUILayout.Button("Force Push"))
                {
                    if (ExGUI.Ask("Are you sure you want to upload to the git repository?"))
                    {
                        string popupMessage = "Are you sure you want to force push?";
                        string popupDescription = "Version type is used to determine the version number. \n" +
                                                  "Patch: 1.0.0 -> 1.0.1 \n" +
                                                  "Minor: 1.0.0 -> 1.1.0 \n" +
                                                  "Major: 1.0.0 -> 2.0.0 \n";

                        VersionTypeSelector.Show(popupMessage, popupDescription, VersionIncrement.Patch, ForcePush);
                    }
                }

                if (GUILayout.Button("Push Version Tag"))
                {
                    PushVersionTag();
                }

                if (GUILayout.Button("Pull Version Tag"))
                {
                    PullVersionTag();
                }
            });
        }

        private void GoToBottom()
        {
            _scrollPosition.y = int.MaxValue;
        }

        private void DrawGitOutput(GitOutput gitOutput)
        {
            if (_gitOutputColors.TryGetValue(gitOutput.Status, out Color color))
            {
                GUIStyle coloredStyle = new(EditorStyles.wordWrappedLabel)
                {
                    normal = { textColor = color }
                };
                GUILayout.Label(gitOutput.Value, coloredStyle);
            }
            else
            {
                GUILayout.Label(gitOutput.Value, EditorStyles.wordWrappedLabel);
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
            // if it starts with git, remove it
            if (_commandLine.StartsWith("git ")) _commandLine = _commandLine.Substring(4);
            if (string.IsNullOrEmpty(_commandLine))
            {
                _gitOutputs.Add(new GitOutput("Empty Command"));
                return;
            }
            await _git.RunGitCommandAsync(_commandLine);
            _commandLine = "";
        }

        private async void EnterGitCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                _gitOutputs.Add(new GitOutput("Empty Command"));
                return;
            }
            await _git.RunGitCommandAsync(command);
        }


        private async void Pull()
        {
            await _git.PullAsync();
        }

        private async void Push(VersionIncrement versionType)
        {
            await _git.PushAsync(versionType);
        }

        private async void ForcePush(VersionIncrement versionType)
        {
            await _git.PushAsync(versionType, true);
        }

        private async void Status()
        {
            await _git.StatusAsync();
        }

        private async void PushVersionTag()
        {
            await _git.PushVersionTagAsync();
        }

        private async void PullVersionTag()
        {
            await _git.PullVersionTagAsync();
        }

        private async void Commit()
        {
            await _git.CommitAsync();
        }

        private async void NormalizeLineEndings()
        {
            await _git.NormalizeLineEndingsAsync();
        }

        private async void ConfigureAutoCRLF(bool value)
        {
            await _git.ConfigureGlobalCoreAutoCRLFAsync(value);
        }
    }
}