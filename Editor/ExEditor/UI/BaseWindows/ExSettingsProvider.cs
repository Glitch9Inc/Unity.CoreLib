using UnityEditor;
using UnityEngine;

namespace Glitch9.ExEditor
{
    public abstract class ExSettingsProvider<TSelf> : SettingsProvider
        where TSelf : ExSettingsProvider<TSelf>
    {
        protected ExSettingsProvider(string path, SettingsScope scope = SettingsScope.User) : base(path, scope)
        {
            _providerStyle = new GUIStyle
            {
                richText = true,
                padding = new RectOffset(10, 10, 10, 10)
            };
        }

        private readonly GUIStyle _providerStyle;
        public override void OnGUI(string searchContext)
        {
            GUILayout.BeginVertical(_providerStyle);
            {
                DrawSettings();
            }
            GUILayout.EndVertical();
        }
        protected abstract void DrawSettings();
    }
}