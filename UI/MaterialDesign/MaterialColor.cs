using System;
using UnityEngine;

namespace Glitch9.UI.MaterialDesign
{
    [Serializable]
    public struct MaterialColor
    {
        public static Color GetColor(ColorRole role, ColorType type)
        {
            return MaterialDesignManager.GetMaterialColor(role).GetColor(type);
        }


        public ColorRole role;

        /// <summary>
        /// The base color
        /// </summary>
        public Color color;

        /// <summary>
        /// Text and icons against base color
        /// </summary>
        public Color onColor;

        /// <summary>
        /// Standout fill color against surface(background), for key components like FAB
        /// </summary>
        public Color container;

        /// <summary>
        /// Text and icons against container
        /// </summary>
        public Color onContainer;
        public Color dim;
        public Color bright;
        public Color variant;
        public Color onVariant;
        

        public readonly Color GetColor(ColorType type)
        {
            switch (type)
            {
                case ColorType.Base:
                    return color;

                case ColorType.OnBase:
                    return onColor;

                case ColorType.Container:
                    return container;

                case ColorType.OnContainer:
                    return onContainer;

                case ColorType.Dim:
                    return dim;

                case ColorType.Bright:
                    return bright;

                case ColorType.Variant:
                    return variant;

                case ColorType.OnVariant:
                    return onVariant;

                default:
                    return color;
            }
        }

        public static MaterialColor Create(ColorRole role)
        {
            MaterialColor newColor = new();
            newColor.role = role;
            return newColor;
        }
    }
}