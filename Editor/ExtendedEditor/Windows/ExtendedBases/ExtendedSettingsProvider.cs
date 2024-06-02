using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    /// <summary>
    /// Base class for creating settings providers
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <typeparam name="TSettings"></typeparam>
    public abstract class ExtendedSettingsProvider<TSelf, TSettings> : SettingsProvider
        where TSelf : ExtendedSettingsProvider<TSelf, TSettings>
        where TSettings : ScriptableObject
    {
        protected ExtendedSettingsProvider(string path, SettingsScope scope = SettingsScope.User) : base(path, scope)
        {
            _providerStyle = new GUIStyle
            {
                richText = true,
                padding = new RectOffset(10, 10, 10, 10)
            };
        }

        private readonly GUIStyle _providerStyle;
        protected SerializedObject SettingsSerializedObject;
        public override void OnGUI(string searchContext)
        {
            GUILayout.BeginVertical(_providerStyle);
            {
                InitializeSettings();
                
                SettingsSerializedObject.Update();  // Update the serialized object

                DrawSettings();
                
                SettingsSerializedObject.ApplyModifiedProperties();
            }
            GUILayout.EndVertical();
        }
        protected abstract void DrawSettings();

        private void InitializeSettings()
        {
            if (SettingsSerializedObject == null)
            {
                string className = typeof(TSettings).Name;
                TSettings settingsInstance = Resources.Load<TSettings>(className);

                if (settingsInstance == null)
                {
                    EditorGUILayout.HelpBox($"Failed to load {className} asset. Please make sure the asset exists in the Resources folder.", MessageType.Error);
                    return;
                }

                Editor settingsEditor = Editor.CreateEditor(settingsInstance);

                if (settingsEditor == null)
                {
                    EditorGUILayout.HelpBox("Failed to create editor for LocalizationSettings. Please report this issue.", MessageType.Error);
                    return;
                }

                SettingsSerializedObject = settingsEditor.serializedObject;
            }

            if (SettingsSerializedObject == null) return;
        }
    }
}