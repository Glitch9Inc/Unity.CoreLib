using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Glitch9.UI.MaterialDesign
{
    public class MaterialDesignManager : MonoSingleton<MaterialDesignManager>
    {
        private static readonly ConcurrentDictionary<Graphic, (ColorRole colorRole, Action<MaterialColor> apply)> _graphics = new();

        public static MaterialTheme Theme
        {
            get
            {
                if (Instance == null)
                {
                    Debug.LogWarning("ThemeManager is not present in the scene. Please add a ThemeManager to the scene.");
                    return MaterialTheme.Light;
                }
                return Instance.theme;
            }
            set
            {
                if (Instance.theme == value) return;
                Instance.theme = value;

                foreach (KeyValuePair<Graphic, (ColorRole colorRole, Action<MaterialColor> apply)> kvp in _graphics)
                {
                    kvp.Value.apply(GetMaterialColor(kvp.Value.colorRole));
                }
            }
        }

        public static ColorScheme Scheme
        {
            get
            {
                if (Instance == null)
                {
                    Debug.LogWarning("ThemeManager is not present in the scene. Please add a ThemeManager to the scene.");
                    return null;
                }

                return Theme == MaterialTheme.Light ? Instance.lightScheme : Instance.darkScheme;
            }
        }

        public static MaterialColor GetMaterialColor(ColorRole colorRole)
        {
            switch (colorRole)
            {
                case ColorRole.Primary:
                    return Scheme.Primary;

                case ColorRole.Secondary:
                    return Scheme.Secondary;

                case ColorRole.Tertiary:
                    return Scheme.Tertiary;

                case ColorRole.Error:
                    return Scheme.Error;

                case ColorRole.Surface:
                    return Scheme.Surface;

                case ColorRole.Outline:
                    return Scheme.Outline;

                default:
                    return Scheme.Primary;
            }
        }

        public static void Add(Graphic graphic, ColorRole colorRole, Action<MaterialColor> apply)
        {
            if (graphic == null)
            {
                Debug.LogWarning("Graphic is null. Please make sure the Graphic is not null.");
                return;
            }

            if (apply == null)
            {
                Debug.LogWarning("Apply is null. Please make sure the apply is not null.");
                return;
            }

            if (!_graphics.TryAdd(graphic, (colorRole, apply)))
            {
                _graphics[graphic] = (colorRole, apply);
            }
        }

        public static void Invoke(Graphic graphic)
        {
            if (graphic == null) return;
            if (_graphics.TryGetValue(graphic, out (ColorRole colorRole, Action<MaterialColor> apply) value))
            {
                value.apply(GetMaterialColor(value.colorRole));
            }
        }

        public static void Remove(Graphic graphic)
        {
            if (graphic == null) return;
            _graphics.TryRemove(graphic, out _);
        }


        [SerializeField] private MaterialTheme theme;
        [SerializeField] private ColorScheme lightScheme;
        [SerializeField] private ColorScheme darkScheme;
        [SerializeField] private bool editPrefabs;

        private void Start()
        {
            Theme = theme;
        }


        public void ApplyColors(MaterialTheme mode)
        {
            ColorScheme scheme = mode == MaterialTheme.Light ? lightScheme : darkScheme;

            foreach (KeyValuePair<object, MaterialColorAttribute> kvp in AttributeCache<MaterialColorAttribute>.GetDictionary())
            {
                // apply color to attribute
                object obj = kvp.Key;
                MaterialColorAttribute attribute = kvp.Value;
                if (obj == null || attribute == null) continue;
                if (obj is not Graphic graphic) continue;

                if (attribute.Role == ColorRole.Unset || attribute.Type == ColorType.Unset) continue;

                Color color = scheme.GetColor(attribute.Role, attribute.Type);
                graphic.SetColorWithoutAlpha(color);
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(graphic);
#endif
            }

            // get all scene object with MaterialColorGraphic component
            List<MaterialColorGraphic> graphics = FindObjectsOfType<MaterialColorGraphic>().ToList();

#if UNITY_EDITOR

            if (editPrefabs)
            {
                // get all prefabs in project with MaterialColorGraphic component
                MaterialColorGraphic[] prefabs = UnityEditor.AssetDatabase.FindAssets("t:Prefab")
                    .Select(UnityEditor.AssetDatabase.GUIDToAssetPath)
                    .Select(UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>)
                    .SelectMany(go => go.GetComponentsInChildren<MaterialColorGraphic>(true))
                    .ToArray();

                graphics.AddRange(prefabs);
            }
#endif

            foreach (MaterialColorGraphic materialColorGraphic in graphics)
            {
                if (materialColorGraphic == null) continue;
                if (materialColorGraphic.Role == ColorRole.Unset || materialColorGraphic.Type == ColorType.Unset) continue;
                Graphic graphic = materialColorGraphic.gameObject.GetComponent<Graphic>();
                if (graphic == null) continue;

                Color color = scheme.GetColor(materialColorGraphic.Role, materialColorGraphic.Type);
                graphic.SetColorWithoutAlpha(color);
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(graphic);
#endif
            }
        }
    }
}