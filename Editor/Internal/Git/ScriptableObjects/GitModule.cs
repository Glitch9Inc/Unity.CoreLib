using UnityEngine;

namespace Glitch9.Internal.Git
{
    [CreateAssetMenu(fileName = nameof(GitModule), menuName = UnityMenu.Utility.CREATE_GIT_MODULE, order = UnityMenu.Utility.ORDER_GIT_MODULE)]
    public class GitModule : ScriptableObject
    {
        public string gitUrl;
        public string gitBranch = "main";
        public string localDir;

#if UNITY_EDITOR
        public void FindLocalDir()
        {
            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
            string directoryPath = System.IO.Path.GetDirectoryName(assetPath);
            localDir = directoryPath;
            Debug.Log("Local Directory: " + localDir);
        }
#endif
    }
}