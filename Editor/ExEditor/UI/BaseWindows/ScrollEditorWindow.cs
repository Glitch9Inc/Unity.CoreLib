using UnityEditor;
using UnityEngine;

namespace Glitch9.ExEditor
{
    public abstract class ScrollEditorWindow<TWindowClass> : ExEditorWindow<TWindowClass>
        where TWindowClass : EditorWindow
    {
        private Vector2 _scrollPosition;
        private GUIStyle _topBorderStyle;
        private GUIStyle _bottomBorderStyle;

        protected override void OnEnable()
        {
            base.OnEnable();
            _topBorderStyle = ExEditorStyles.Border(GUIBorder.Top);
            _bottomBorderStyle = ExEditorStyles.Border(GUIBorder.Bottom);
        }

        private void OnGUI()
        {
            try
            {
                GUILayout.BeginVertical(_topBorderStyle);
                DrawTop();
            }
            finally
            {
                GUILayout.EndVertical();
            }

            GUILayout.BeginVertical();
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            GUILayout.Space(5);

            try
            {
                GUILayout.BeginVertical();
                OnGUIMid();
                GUILayout.Space(5);
            }
            finally
            {
                GUILayout.EndVertical();
            }

            EditorGUILayout.EndScrollView();

            OnGUIMidEnd();
            GUILayout.EndVertical();

            try
            {
                GUILayout.BeginVertical(_bottomBorderStyle);
                OnGUIBottom();
            }
            finally
            {
                GUILayout.EndVertical();
            }
        }

        public void GoToBottomScrollPosition()
        {
            _scrollPosition.y = int.MaxValue;
        }

        private void DrawTop()
        {
            ExGUILayout.HorizontalLayout(() =>
            {
                OnGUITop();
                GUILayout.FlexibleSpace();
                DrawWindowLabelButtons();
                if (GUILayout.Button(EditorIcons.Settings, ExEditorStyles.miniButton))
                {
                    IsShowingSettings = !IsShowingSettings;
                    return;
                }
            });

            if (IsShowingSettings) OpenSettings();
            else DrawWindowToolBar();
        }

        #region Override Methods
        protected abstract void OnGUITop();
        protected virtual void DrawWindowLabelButtons() { }
        protected virtual void DrawWindowToolBar() { }
        protected abstract void OnGUIMid();
        protected virtual void OnGUIMidEnd() { }
        protected abstract void OnGUIBottom();
        #endregion
    }
}