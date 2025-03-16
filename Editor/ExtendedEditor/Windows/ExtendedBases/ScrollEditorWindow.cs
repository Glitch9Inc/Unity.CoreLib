using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    public abstract class ScrollEditorWindow<TWindowClass> : ExtendedEditorWindow<TWindowClass>
        where TWindowClass : EditorWindow
    {
        private Vector2 _scrollPosition;
        private GUIStyle _topBorderStyle;
        private GUIStyle _bottomBorderStyle;

        private bool _isInitialized = false;
        private bool _isInitializing = false;

        protected override void OnEnable()
        {
            base.OnEnable();
            InitializeScrollEditorWindow();
            _topBorderStyle = EGUIStyles.Border(GUIBorder.Top);
            _bottomBorderStyle = EGUIStyles.Border(GUIBorder.Bottom);
        }

        protected void InitializeScrollEditorWindow()
        {
            if (_isInitialized || _isInitializing) return;
            _isInitializing = true;
            _isInitialized = true;
            OnInitialize();
            _isInitializing = false;
        }

        protected abstract void OnInitialize();

        private void OnGUI()
        {
            if (!_isInitialized)
            {
                InitializeScrollEditorWindow();
                return;
            }

            if (!RequirementsMet())
            {
                OnRequirementsNotMet();
                return;
            }

            try
            {
                GUILayout.BeginVertical(_topBorderStyle);
                DrawTop();
            }
            catch
            {
                _isInitialized = false;
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
            catch
            {
                _isInitialized = false;
            }
            finally
            {
                GUILayout.EndVertical();
            }

            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();

            try
            {
                GUILayout.BeginVertical(_bottomBorderStyle);
                OnGUIBottom();
            }
            catch
            {
                _isInitialized = false;
            }
            finally
            {
                GUILayout.EndVertical();
            }
        }

        public virtual void GoToBottomScrollPosition()
        {
            _scrollPosition.y = int.MaxValue;
        }

        private void DrawTop()
        {
            EditorGUILayout.BeginHorizontal();
            {
                OnGUITop();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(EditorIcons.GUISkin, EGUIStyles.headerButton))
                {
                    OpenSettingsWindow();
                }
                DrawWindowLabelButtons();
            }
            EditorGUILayout.EndHorizontal();

            DrawWindowToolBar();
        }

        #region Override Methods
        protected abstract void OnGUITop();
        protected virtual void DrawWindowLabelButtons() { }
        protected virtual void DrawWindowToolBar() { }
        protected abstract void OnGUIMid();
        protected abstract void OnGUIBottom();
        protected virtual bool RequirementsMet() => true;
        protected virtual void OnRequirementsNotMet() { }
        #endregion
    }
}