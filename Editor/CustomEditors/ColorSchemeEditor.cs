using Glitch9.ExtendedEditor;
using UnityEditor;
using UnityEngine;

namespace Glitch9.UI.MaterialDesign
{
    [CustomEditor(typeof(ColorScheme))]
    public class ColorSchemeEditor : Editor
    {
        private SerializedProperty primary;
        private SerializedProperty secondary;
        private SerializedProperty tertiary;
        private SerializedProperty surface;
        private SerializedProperty outline;
        private SerializedProperty error;

        // test
        private SerializedProperty containerAdjustment;


        private void OnEnable()
        {
            primary = serializedObject.FindProperty("primary");
            secondary = serializedObject.FindProperty("secondary");
            tertiary = serializedObject.FindProperty("tertiary");
            surface = serializedObject.FindProperty("surface");
            outline = serializedObject.FindProperty("outline");
            error = serializedObject.FindProperty("error");

            containerAdjustment = serializedObject.FindProperty("containerAdjustment");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space();
            
            serializedObject.Update();

            DrawMaterialColor("Primary Colors", primary);
            DrawMaterialColor("Secondary Colors", secondary);
            DrawMaterialColor("Tertiary Colors", tertiary);
            DrawMaterialColor("Surface Colors", surface);
            DrawMaterialColor("Outline Colors", outline);
            DrawMaterialColor("Error Colors", error);


            // Adjustments
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Adjustments", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EGUIStyles.helpBox);
            {
                EditorGUILayout.PropertyField(containerAdjustment, new GUIContent("Container Adjustment"));
            }
            EditorGUILayout.EndVertical();


            serializedObject.ApplyModifiedProperties();
        }

        private void DrawMaterialColor(string label, SerializedProperty colorProperty)
        {
            ColorRole role = (ColorRole)colorProperty.FindPropertyRelative("role").enumValueIndex;
            
            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Auto Generate"))
                {
                    AutoGenerate(colorProperty, role);
                }

                if (role == ColorRole.Error)
                {
                    if (GUILayout.Button("Default Error Colors"))
                    {
                        GetDefaultErrorColors(colorProperty);
                    }
                }
            }
            GUILayout.EndHorizontal();
            
            EditorGUILayout.BeginVertical(EGUIStyles.helpBox);
            {
                EditorGUILayout.PropertyField(colorProperty);
            }
            EditorGUILayout.EndVertical();
        }
        
        private static void GetDefaultErrorColors(SerializedProperty colorProperty)
        {
            // base: ba1a1a
            // on: ffffff
            // container : ffdad6
            // onContainer: 410002

            SerializedProperty baseColor = colorProperty.FindPropertyRelative("color");
            SerializedProperty onColor = colorProperty.FindPropertyRelative("onColor");
            SerializedProperty container = colorProperty.FindPropertyRelative("container");
            SerializedProperty onContainer = colorProperty.FindPropertyRelative("onContainer");

            baseColor.colorValue = new Color(0.7294118f, 0.1019608f, 0.1019608f);
            onColor.colorValue = Color.white;
            container.colorValue = new Color(1f, 0.854902f, 0.8392157f);
            onContainer.colorValue = new Color(0.254902f, 0f, 0.007843138f);

            colorProperty.serializedObject.ApplyModifiedProperties();
        }

        private void AutoGenerate(SerializedProperty colorProperty, ColorRole role)
        {
            SerializedProperty baseColor = colorProperty.FindPropertyRelative("color");
            SerializedProperty onColor = colorProperty.FindPropertyRelative("onColor");
            SerializedProperty container = colorProperty.FindPropertyRelative("container");
            SerializedProperty onContainer = colorProperty.FindPropertyRelative("onContainer");
            SerializedProperty dim = colorProperty.FindPropertyRelative("dim");
            SerializedProperty bright = colorProperty.FindPropertyRelative("bright");
            SerializedProperty variant = colorProperty.FindPropertyRelative("variant");
            SerializedProperty onVariant = colorProperty.FindPropertyRelative("onVariant");

            Color baseColorValue = baseColor.colorValue;
            Color containerColor, onContainerColor, dimColor, brightColor, variantColor, onVariantColor;

            switch (role)
            {
                case ColorRole.Primary:
                case ColorRole.Secondary:
                case ColorRole.Tertiary:
                case ColorRole.Error:
                    containerColor = baseColorValue.AdjustBrightness(containerAdjustment.floatValue);
                    onContainerColor = containerColor.GetContrastingColor();
                    dimColor = baseColorValue.AdjustBrightness(0.8f);
                    brightColor = baseColorValue.AdjustBrightness(1.2f);
                    variantColor = baseColorValue.AdjustBrightness(1.1f);
                    onVariantColor = variantColor.GetContrastingColor();
                    break;

                case ColorRole.Surface:
                    containerColor = baseColorValue.AdjustBrightness(1.0f);
                    onContainerColor = containerColor.GetContrastingColor();
                    dimColor = baseColorValue.AdjustBrightness(0.9f);
                    brightColor = baseColorValue.AdjustBrightness(1.1f);
                    variantColor = baseColorValue.AdjustBrightness(1.2f);
                    onVariantColor = variantColor.GetContrastingColor();
                    break;

                case ColorRole.Outline:
                    containerColor = baseColorValue.AdjustBrightness(1.0f);
                    onContainerColor = containerColor.GetContrastingColor();
                    dimColor = baseColorValue.AdjustBrightness(0.9f);
                    brightColor = baseColorValue.AdjustBrightness(1.1f);
                    variantColor = baseColorValue.AdjustBrightness(1.2f);
                    onVariantColor = variantColor.GetContrastingColor();
                    break;

                default:
                    containerColor = baseColorValue;
                    onContainerColor = containerColor.GetContrastingColor();
                    dimColor = baseColorValue.AdjustBrightness(0.8f);
                    brightColor = baseColorValue.AdjustBrightness(1.2f);
                    variantColor = baseColorValue.AdjustBrightness(1.1f);
                    onVariantColor = variantColor.GetContrastingColor();
                    break;
            }

            // Assign colors
            baseColor.colorValue = baseColorValue;
            onColor.colorValue = baseColorValue.GetMaterialOnColor();
            container.colorValue = containerColor;
            onContainer.colorValue = containerColor.GetMaterialOnColor();
            dim.colorValue = dimColor;
            bright.colorValue = brightColor;
            variant.colorValue = variantColor;
            onVariant.colorValue = onVariantColor;
            Debug.Log("Auto Generated Colors for " + role);

            colorProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}