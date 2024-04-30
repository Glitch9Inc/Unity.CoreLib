using System;
using UnityEditor;
using UnityEngine;
using MessageType = UnityEditor.MessageType;

namespace Glitch9.ExEditor
{
    public partial class ExGUILayout
    {
        public static void InfoVisitButton(string label, string url)
        {
            GUILayout.BeginHorizontal();

            EditorGUILayout.HelpBox(
                label
                , MessageType.Info);

            if (GUILayout.Button("Visit", new GUILayoutOption[] {
                GUILayout.Width(50),
                GUILayout.Height(38),
                }))
            {
                Application.OpenURL(url);
            }

            GUILayout.EndHorizontal();
        }

        public static void Foldout(string label, Action callback)
        {
            int space = 3;
            ExGUIUtility.DrawHorizontalLine(1);
            GUILayout.Space(space);
            bool b = EditorPrefs.GetBool(label, true);
            b = EditorGUILayout.Foldout(b, label, ExEditorStyles.foldout);
            EditorPrefs.SetBool(label, b);

            if (b)
            {
                GUILayout.Space(space);
                ExGUIUtility.DrawHorizontalLine(1);

                GUIStyle style = new();
                style.margin = new RectOffset(0, 0, 10, 10);

                GUILayout.BeginVertical(style);
                callback?.Invoke();
                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.Space(space);
                ExGUIUtility.DrawHorizontalLine(1);
            }

            GUILayout.Space(-space);
        }

        public static void Title(string label)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(label, ExEditorStyles.title);
            ExGUIUtility.DrawTitleLine();
        }

        public static void BoxedLayout(string label, Action callback, Texture texture = null)
        {
            Rect r = (Rect)EditorGUILayout.BeginVertical(ExGUI.skin.box);
            if (texture == null) texture = EditorIcons.NoImageHighRes;
            /* Header */
            GUI.DrawTexture(new Rect(r.position.x + 10, r.position.y + 5, 24, 24), texture);
            GUI.skin.label.fontSize = 14;
            GUI.Label(ExGUI.GetHeaderRect(r, indent: 40, width: r.width), label.ToUpper());
            GUI.skin.label.fontSize = 12;
            GUILayout.Space(30);
            callback?.Invoke();
            EditorGUILayout.EndVertical();
        }

        public static bool ReverseBoolField(string label, SerializedProperty p)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(p, GUIContent.none, GUILayout.Width(10));
            label ??= p.displayName;
            GUILayout.Label(new GUIContent(label), GUILayout.MinWidth(10));
            GUILayout.EndHorizontal();
            return p.boolValue;
        }

        public static bool ReverseBoolField(SerializedProperty p)
        {
            return ReverseBoolField(null, p);
        }

        public static void SpriteField(SerializedProperty p, int size, int topMargin)
        {
            GUILayout.BeginVertical();
            GUILayout.Space(topMargin);
            p.objectReferenceValue = EditorGUILayout.ObjectField(p.objectReferenceValue, typeof(Sprite), false, new GUILayoutOption[] {
                    GUILayout.Width(size),
                    GUILayout.Height(size)
                }); ;
            GUILayout.EndVertical();
        }
    }
}

