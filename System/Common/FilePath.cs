using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Glitch9
{
    /// <summary>
    /// A wrapper for file paths that can be serialized to JSON.
    /// </summary>
    [Serializable]
    public class FilePath
    {
        [SerializeField] private string id;
        [SerializeField] private FilePathType type;
        [SerializeField] private string uri;
        [SerializeField] private bool selected;

        [JsonIgnore] public string Id => id;
        [JsonIgnore] public FilePathType Type => type;
        [JsonIgnore] public string Uri => uri;
        [JsonIgnore] public bool Selected {
            get => selected;
            set => selected = value;
        }
        

        [JsonConstructor]
        public FilePath() { }
        public FilePath(FilePathType type, string uri)
        {
            this.type = type;
            this.uri = uri;
        }
    }

    public enum FilePathType
    {
        Unset,
        /// <summary>
        /// The path is relative to the project's Assets folder.
        /// </summary>
        Asset,
        /// <summary>
        /// The path is relative to the project's Resources folder.
        /// </summary>
        Resources,
        /// <summary>
        /// The path is relative to the project's StreamingAssets folder.
        /// </summary>
        StreamingAsset,
        /// <summary>
        /// The path is relative to the project's PersistentDataPath.
        /// </summary>
        PersistentData,
        /// <summary>
        /// The path is a URL to a resource on the internet.
        /// </summary>
        URL,
    }
}