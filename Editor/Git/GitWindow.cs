using UnityEngine;

namespace Glitch9.IO.Git
{
    public abstract class GitWindow : EditorUtilityWindow
    {
        protected abstract string GIT_URL { get; }
        protected abstract string GIT_BRANCH { get; }
        protected abstract string WORKING_DIR { get; }

        private EditorGit _git;
   

        private void OnEnable()
        {
            if (string.IsNullOrEmpty(GIT_URL))
            {
                Debug.LogError("Git URL is null or empty: " + GIT_URL);
                return;
            }

            if (string.IsNullOrEmpty(WORKING_DIR))
            {
                Debug.LogError("Working Dir is null or empty: " + WORKING_DIR);
                return;
            }

            _git = new EditorGit();
            _git.InitializeAsync(GIT_URL, GIT_BRANCH, WORKING_DIR);
        }

        protected override void OnGUIUpdate()
        {
            if (!_git.IsInitialized)
            {
                GUILayout.Label("Initializing Git...");
                return;
            }
            
            _git.DrawGit(); 
            GUILayout.Space(10);
            _git.DrawDebugMenu();
        }
    
    }
}
