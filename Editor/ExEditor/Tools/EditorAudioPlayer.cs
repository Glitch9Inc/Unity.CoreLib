using System;
using System.Collections;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExEditor
{
    public static class EditorAudioPlayer
    {
        private static bool _isPlaying = false;

        public static async void PlayAudio(AudioClip audioClip)
        {
            if (_isPlaying) StopAudio();
            Assembly unityAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "PlayPreviewClip",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new System.Type[] { typeof(AudioClip), typeof(Int32), typeof(Boolean) },
                null
            );
            method.Invoke(
                null,
                new object[] { audioClip, 0, false }
            );
            _isPlaying = true;
            // Delay for the length of the audio clip using UniTask
            await UniTask.Delay(TimeSpan.FromSeconds(audioClip.length));

            _isPlaying = false;
        }

        public static void StopAudio()
        {
            _isPlaying = false;
            Assembly unityAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "StopAllPreviewClips",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new System.Type[] { },
                null
            );
            method.Invoke(
                null,
                new object[] { }
            );
        }

    }
}