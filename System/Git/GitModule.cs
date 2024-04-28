using UnityEngine;


namespace Glitch9
{
    [CreateAssetMenu(fileName = "GitModule", menuName = "Glitch9/GitModule", order = 0)]
    public class GitModule : ScriptableObject
    {
        [SerializeField] private string gitUrl;
        [SerializeField] private string gitBranch = "main";
        [SerializeField] private string localDir;

        public string GitUrl => gitUrl;
        public string LocalDir => localDir;

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