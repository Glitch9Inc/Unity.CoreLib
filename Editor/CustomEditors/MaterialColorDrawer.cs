using Glitch9.ExtendedEditor;
using UnityEditor;
using UnityEngine;

namespace Glitch9.UI.MaterialDesign
{
    [CustomPropertyDrawer(typeof(MaterialColor))]
    public class MaterialColorDrawer : PropertyDrawer
    {
        private SerializedProperty role;
        private SerializedProperty color;
        private SerializedProperty onColor;
        private SerializedProperty container;
        private SerializedProperty onContainer;
        private SerializedProperty dim;
        private SerializedProperty bright;
        private SerializedProperty variant;
        private SerializedProperty onVariant;

        private ColorRole colorRole => (ColorRole)role.enumValueIndex;
        private bool _isInitialized = false;
        private int _lineCount = 1;
        private float _totalHeight;

        private void Initialize(SerializedProperty property)
        {
            if (_isInitialized) return;
            _isInitialized = true;

            role = property.FindPropertyRelative(nameof(MaterialColor.role));

            // for primary, secondary and tertiary colors (and error colors)
            color = property.FindPropertyRelative(nameof(MaterialColor.color));
            onColor = property.FindPropertyRelative(nameof(MaterialColor.onColor));
            container = property.FindPropertyRelative(nameof(MaterialColor.container));
            onContainer = property.FindPropertyRelative(nameof(MaterialColor.onContainer));

            // for surface colors (below + 'color')
            dim = property.FindPropertyRelative(nameof(MaterialColor.dim));
            bright = property.FindPropertyRelative(nameof(MaterialColor.bright));
            variant = property.FindPropertyRelative(nameof(MaterialColor.variant));
            onVariant = property.FindPropertyRelative(nameof(MaterialColor.onVariant));

            // outline only has 'color' and 'variant'

            UpdateLineCount();
        }

        private void UpdateLineCount()
        {
            switch (colorRole)
            {
                case ColorRole.Primary:
                case ColorRole.Secondary:
                case ColorRole.Tertiary:
                case ColorRole.Error:
                    _lineCount = 4;
                    break;
                case ColorRole.Surface:
                    _lineCount = 7;
                    break;
                case ColorRole.Outline:
                    _lineCount = 2;
                    break;
            }

            int totalLineCount = _lineCount; // including the label
            float singleLineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            _totalHeight = totalLineCount * singleLineHeight;
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Initialize(property);

            // Draw the role field
            //Rect roleRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            //EditorGUI.PropertyField(roleRect, role);
            //EditorGUI.LabelField(roleRect, label);

            // Update the line count if role changes
            if (role.hasMultipleDifferentValues || role.enumValueIndex != (int)colorRole)
            {
                UpdateLineCount();
            }

            // Calculate the position for the next fields
            //position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Draw the properties based on the selected role
            //EditorGUI.indentLevel++;
            switch (colorRole)
            {
                case ColorRole.Primary:
                case ColorRole.Secondary:
                case ColorRole.Tertiary:
                case ColorRole.Error:
                    position = DrawPropertyField(position, color, colorRole);
                    position = DrawPropertyField(position, onColor, colorRole);
                    position = DrawPropertyField(position, container, colorRole);
                    DrawPropertyField(position, onContainer, colorRole);
                    break;
                case ColorRole.Surface:
                    position = DrawPropertyField(position, color, colorRole);
                    position = DrawPropertyField(position, onColor, colorRole);
                    position = DrawPropertyField(position, container, colorRole);
                    position = DrawPropertyField(position, onContainer, colorRole);
                    position = DrawPropertyField(position, dim, colorRole);
                    position = DrawPropertyField(position, bright, colorRole);
                    DrawPropertyField(position, onVariant, colorRole);
                    break;
                case ColorRole.Outline:
                    position = DrawPropertyField(position, color, colorRole);
                    DrawPropertyField(position, variant, colorRole);
                    break;
            }
            //EditorGUI.indentLevel--;

            EditorGUI.EndProperty();
        }

        private Rect DrawPropertyField(Rect position, SerializedProperty property, ColorRole colorRole)
        {
            string displayName = property.displayName;
            
            if (displayName == "Color") displayName = $"{colorRole}";
            else if (displayName == "On Color") displayName = $"On {colorRole}";
            else if (displayName == "Container") displayName = $"{colorRole} Container";
            else if (displayName == "On Container") displayName = $"On {colorRole} Container";
            else if (displayName == "Dim") displayName = $"{colorRole} Dim";
            else if (displayName == "Bright") displayName = $"{colorRole} Bright";
            else if (displayName == "Variant") displayName = $"{colorRole} Variant";
            else if (displayName == "On Variant") displayName = $"On {colorRole} Variant";
            
            GUIContent label = new GUIContent(displayName);
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), property, label);
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            return position;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Initialize(property);
            return _totalHeight;
        }

    }
}