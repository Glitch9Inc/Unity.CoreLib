using System;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    public class EditorConfirmation : EditorWindow
    {
        private const string WINDOW_NAME = "Confirmation";
        private const string CONFIRM_BUTTON = "Confirm";

        public static void Show(string instruction, string textToConfirm, Action<bool> onConfirm)
        {
            EditorConfirmation popup = GetWindow<EditorConfirmation>(true, WINDOW_NAME, true);
            popup._instruction = instruction;
            popup._textToConfirm = textToConfirm;
            popup._onConfirm = onConfirm;

            // fixed window size
            popup.minSize = new Vector2(400, 600);
            popup.maxSize = new Vector2(400, 600);
        }

        private string _instruction;
        private string _textToConfirm;
        private Action<bool> _onConfirm;
        private Vector2 _scrollPosition;

        private void OnGUI()
        {
            GUILayout.BeginVertical(EGUIStyles.popupBody);

            EditorGUILayout.LabelField(_instruction, EditorStyles.wordWrappedLabel);
            GUILayout.Space(5f);

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.ExpandHeight(true));
            EditorGUILayout.TextArea(_textToConfirm, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            EditorGUILayout.EndScrollView();

            GUILayout.Space(10f);

            GUILayout.BeginHorizontal();
            //Texts.UI.BtnConfirm
            if (GUILayout.Button(CONFIRM_BUTTON))
            {
                _onConfirm?.Invoke(true);
                _onConfirm = null;
                Close();
            }
      
            GUILayout.EndHorizontal();
        }

        
        private void OnDestroy()
        {
            _onConfirm?.Invoke(false);
        }
    }
}