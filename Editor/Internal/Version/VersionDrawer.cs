using Glitch9.ExtendedEditor;
using UnityEditor;
using UnityEngine;

namespace Glitch9.Internal
{
    [CustomPropertyDrawer(typeof(Version), true)]
    public class VersionDrawer : PropertyDrawer
    {
        private SerializedProperty major;
        private SerializedProperty minor;
        private SerializedProperty patch;
        private SerializedProperty build;
        private SerializedProperty releaseDate;
        
        private Rect labelRect;
        private Rect buildLabelRect;
        private Rect releaseDateLabelRect;
        
        private Rect majorRect;
        private Rect majorBtnRect;
        private Rect minorRect;
        private Rect minorBtnRect;
        private Rect patchRect;
        private Rect patchBtnRect;

        private Rect buildRect;
        private Rect releaseDateRect;

        private UnixTime _releaseDate;

        private bool _isInitialized = false;

        private void Initialize(Rect position, SerializedProperty property)
        {
            major = property.FindPropertyRelative("major");
            minor = property.FindPropertyRelative("minor");
            patch = property.FindPropertyRelative("patch");
            build = property.FindPropertyRelative("build");
            releaseDate = property.FindPropertyRelative("releaseDate");
            
            Rect rect = position;
            rect.y -= EditorGUIUtility.singleLineHeight;

            // Layout (3 rows)
            // Label            Major[+]  Minor[+]  Patch[+]
            // Build            [Build Number]
            // Release Date     [Release Date]

            Rect row1 = rect.GetSingleLightHeightRow(1);
            Rect row2 = rect.GetSingleLightHeightRow(2);
            Rect row3 = rect.GetSingleLightHeightRow(3);

            // Row1: Label, Major, Minor, Patch
            labelRect = row1.GetLabelRect();
            Rect row1ValueRect = row1.GetValueRect();

            const float BUTTON_WIDTH = 20;
            
            majorRect = row1ValueRect;
            majorRect.width = row1ValueRect.width / 3 - BUTTON_WIDTH;
            majorBtnRect = majorRect;
            majorBtnRect.x += majorRect.width;
            majorBtnRect.width = BUTTON_WIDTH;

            minorRect = majorRect;
            minorRect.x += majorRect.width + BUTTON_WIDTH;
            minorBtnRect = minorRect;
            minorBtnRect.x += minorRect.width;
            minorBtnRect.width = BUTTON_WIDTH;

            patchRect = minorRect;
            patchRect.x += minorRect.width + BUTTON_WIDTH;
            patchBtnRect = patchRect;
            patchBtnRect.x += patchRect.width;
            patchBtnRect.width = BUTTON_WIDTH;

            // Row2: Build
            buildLabelRect = row2.GetLabelRect();
            buildRect = row2.GetValueRect();

            // Row3: Release Date
            releaseDateLabelRect = row3.GetLabelRect();
            releaseDateRect = row3.GetValueRect();

            if (_isInitialized) return;
            _isInitialized = true;

            _releaseDate = new UnixTime(releaseDate.longValue);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Initialize(position, property);

            // Row 1
            GUILayout.BeginHorizontal();
            {
                EditorGUI.BeginChangeCheck();
                {
                    EditorGUI.LabelField(labelRect, label);
                    EditorGUI.PropertyField(majorRect, major, GUIContent.none);
                    DrawVersionIncreaseButton(majorBtnRect, major);
                    EditorGUI.PropertyField(minorRect, minor, GUIContent.none);
                    DrawVersionIncreaseButton(minorBtnRect, minor);
                    EditorGUI.PropertyField(patchRect, patch, GUIContent.none);
                    DrawVersionIncreaseButton(patchBtnRect, patch);
                }
                if (EditorGUI.EndChangeCheck())
                {
                    build.intValue = Version.CalcBuildNumber(major.intValue, minor.intValue, patch.intValue);
                }
            }
            GUILayout.EndHorizontal();

            // Row 2
            GUILayout.BeginHorizontal();
            {
                EditorGUI.LabelField(buildLabelRect, "Build");
                EditorGUI.LabelField(buildRect, build.intValue.ToString());
            }
            GUILayout.EndHorizontal();

            // Row 3
            GUILayout.BeginHorizontal();
            {
                EditorGUI.LabelField(releaseDateLabelRect, "Release Date");
                EditorGUI.LabelField(releaseDateRect, _releaseDate.ToString("d"));
            }
            GUILayout.EndHorizontal();

            EditorGUI.EndProperty();
        }

        private void DrawVersionIncreaseButton(Rect rect, SerializedProperty property)
        {
            if (GUI.Button(rect, "+"))
            {
                property.intValue++;
                build.intValue = Version.CalcBuildNumber(major.intValue, minor.intValue, patch.intValue);
                releaseDate.longValue = UnixTime.Now;
                _releaseDate = new UnixTime(releaseDate.longValue);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }
    }
}