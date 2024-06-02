using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    public class EditorLoadingCircle
    {
        private readonly EditorWindow _editorWindow;
        private readonly Texture _texture;
        private float _angle = 0.0f;

        public EditorLoadingCircle(EditorWindow editorWindow)
        {
            _editorWindow = editorWindow;
            _texture = EditorIcons.Loading;
        }

        public void Draw(Rect rect)
        {
            Matrix4x4 oldMatrix = GUI.matrix;
            _angle = (_angle + 3) % 360; // 증가되는 각도
            GUIUtility.RotateAroundPivot(_angle, rect.center); // 회전
            GUI.DrawTexture(rect, _texture); // 텍스쳐 그리기
            GUI.matrix = oldMatrix; // GUI 상태 복구

            _editorWindow.Repaint();
        }
    }
}