
using UnityEngine;

namespace Glitch9.UI.MaterialDesign
{
    //[CreateAssetMenu(fileName = UnityMenu.MaterialDesign.COLOR_SCHEME_NAME, menuName = UnityMenu.MaterialDesign.COLOR_SCHEME_CREATE, order = UnityMenu.MaterialDesign.COLOR_SCHEME_ORDER)]
    public class ColorScheme : ScriptableObject
    {
        private static class Tooltips
        {
            internal const string SURFACE = "A role used for backgrounds and large, low-emphasis areas of the screen.";
            internal const string PRIMARY_SECONDARY_TERTIARY = "Accent color roles used to emphasize or de-emphasize foreground elements.";
            internal const string CONTAINER = "Roles used as a fill color for foreground elements like buttons. They should not be used for text or icons.";
            internal const string ON = "Roles starting with this term indicate a color for text or icons on top of its paired parent color. For example, on primary is used for text and icons against the primary fill color.";
            internal const string VARIANT = "Roles ending with this term offer a lower emphasis alternative to its non-variant pair. For example, outline variant is a less emphasized version of the outline color.";
        }

        [SerializeField] private MaterialColor primary = MaterialColor.Create(ColorRole.Primary);
        [SerializeField] private MaterialColor secondary = MaterialColor.Create(ColorRole.Secondary);
        [SerializeField] private MaterialColor tertiary = MaterialColor.Create(ColorRole.Tertiary);
        [SerializeField] private MaterialColor surface = MaterialColor.Create(ColorRole.Surface);
        [SerializeField] private MaterialColor outline = MaterialColor.Create(ColorRole.Outline);
        [SerializeField] private MaterialColor error = MaterialColor.Create(ColorRole.Error);


        // Test Features
        [SerializeField] private float containerAdjustment = 4.6f;


        public MaterialColor Primary => primary;
        public MaterialColor Secondary => secondary;
        public MaterialColor Tertiary => tertiary;
        public MaterialColor Surface => surface;
        public MaterialColor Outline => outline;
        public MaterialColor Error => error;


        public Color GetColor(ColorRole colorRole, ColorType colorType)
        {
            switch (colorRole)
            {
                case ColorRole.Primary:
                    return primary.GetColor(colorType);

                case ColorRole.Secondary:
                    return secondary.GetColor(colorType);

                case ColorRole.Tertiary:
                    return tertiary.GetColor(colorType);

                case ColorRole.Surface:
                    return surface.GetColor(colorType);

                case ColorRole.Outline:
                    return outline.GetColor(colorType);

                case ColorRole.Error:
                    return error.GetColor(colorType);

                default:
                    return primary.GetColor(colorType);
            }
        }
    }
}