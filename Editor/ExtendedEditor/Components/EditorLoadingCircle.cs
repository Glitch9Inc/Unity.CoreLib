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
            // Aspect ratio를 1:1로 유지
            float size = Mathf.Min(rect.width, rect.height);
            Rect squareRect = new Rect(
                rect.x + (rect.width - size) / 2,
                rect.y + (rect.height - size) / 2,
                size,
                size
            );

            Matrix4x4 oldMatrix = GUI.matrix;
            _angle = (_angle + 3) % 360; // 증가되는 각도
            GUIUtility.RotateAroundPivot(_angle, squareRect.center); // 회전
            GUI.DrawTexture(squareRect, _texture); // 텍스쳐 그리기
            GUI.matrix = oldMatrix; // GUI 상태 복구

            _editorWindow.Repaint();
        }
    }
}