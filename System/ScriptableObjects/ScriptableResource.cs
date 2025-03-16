using UnityEngine;

namespace Glitch9.ScriptableObjects
{
    public abstract class ScriptableResource<TSelf> : ScriptableObject
        where TSelf : ScriptableResource<TSelf>
    {

        private static TSelf _instance;
        public static TSelf Instance => _instance == null ? _instance = AssetUtils.LoadResource<TSelf>(true) : _instance;
    }
}