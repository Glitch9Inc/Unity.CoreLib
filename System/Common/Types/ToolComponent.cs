using UnityEngine;

namespace Glitch9
{
    public class ToolComponent : MonoBehaviour
    {
        [SerializeField] private string toolName = "Unknown Tool";
        [SerializeField, SerializeReference] private ILogger logger;
        public string ToolName => toolName;
        public ILogger Logger
        {
            get => logger ??= new GNLogger(toolName);
            set => logger = value;
        }
    }
}