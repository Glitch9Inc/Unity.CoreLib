using UnityEngine;
using UnityEngine.UI;

namespace Glitch9
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Convert a hex string into a color
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Color ToColor(this string hex)
        {
            return hex.TryParseToColor(out Color color) ? color : Color.white;
        }

        public static bool TryParseToColor(this string hex, out Color color)
        {
            try
            {
                hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
                hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
                byte a = 255;//assume fully visible unless specified in hex
                byte r = byte.Parse(hex.Substring(0, 2), global::System.Globalization.NumberStyles.HexNumber);
                byte g = byte.Parse(hex.Substring(2, 2), global::System.Globalization.NumberStyles.HexNumber);
                byte b = byte.Parse(hex.Substring(4, 2), global::System.Globalization.NumberStyles.HexNumber);
                //Only use alpha if the string has enough characters
                if (hex.Length == 8)
                {
                    a = byte.Parse(hex.Substring(6, 2), global::System.Globalization.NumberStyles.HexNumber);
                }
                color = new Color32(r, g, b, a);
                return true;
            }
            catch
            {
                color = Color.white;
                return false;
            }
        }

        public static string ToHex(this Color color)
        {
            // ex : #FFFFFF
            return "#" + ColorUtility.ToHtmlStringRGB(color);
        }

        public static string SetColor(this string text, Color color)
        {
            return "<color=" + color.ToHex() + ">" + text + "</color>";
        }

        public static Color Adjust(this Color color, float brightness, float saturation, float hue)
        {
            Color.RGBToHSV(color, out float h, out float s, out float v);
            h += hue;
            s += saturation;
            v += brightness;
            return Color.HSVToRGB(h, s, v);
        }

        public static Color GetRarityColor(int rarity, bool glow = false)
        {
            if (!glow)
            {
                return rarity switch
                {
                    1 => ExColor.jade,
                    2 => ExColor.sapphire,
                    3 => ExColor.royalpurple,
                    4 => ExColor.scarlet,
                    5 => ExColor.clementine,
                    _ => ExColor.steel,
                };
            }
            else
            {
                return rarity switch
                {
                    1 => ExColor.fern,
                    2 => ExColor.azure,
                    3 => ExColor.periwrinkle,
                    4 => ExColor.salmon,
                    5 => ExColor.honey,
                    _ => ExColor.pastel,
                };
            }
        }

        public static Color Darken(this Color color, float amount)
        {
            return new Color(color.r - amount, color.g - amount, color.b - amount, color.a);
        }

        public static Color Lighten(this Color color, float amount)
        {
            return new Color(color.r + amount, color.g + amount, color.b + amount, color.a);
        }

        public static Color SetAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        public static Color AdjustBrightness(this Color color, float factor)
        {
            // 값이 낮을수록 더 높은 factor를 적용
            float rate = 1.25f;

            return new Color(
                Mathf.Clamp01(color.r * (1 - color.r * rate) * factor),
                Mathf.Clamp01(color.g * (1 - color.g * rate) * factor),
                Mathf.Clamp01(color.b * (1 - color.b * rate) * factor),
                color.a
            );
        }

        public static Color GetContrastingColor(this Color color)
        {
            float luminance = 0.2126f * color.r + 0.7152f * color.g + 0.0722f * color.b;
            bool makeItDarker = luminance > 0.5f;

            float multiplier = makeItDarker ? 0.1f : 10f;

            return new Color(
                Mathf.Clamp01(color.r * multiplier),
                Mathf.Clamp01(color.g * multiplier),
                Mathf.Clamp01(color.b * multiplier),
                color.a
            );
        }

        public static Color GetComplementaryColor(this Color color)
        {
            return new Color(1 - color.r, 1 - color.g, 1 - color.b, color.a);
        }

        public static Color GetMaterialOnColor(this Color color)
        {
            // example : {base} => {onBase}
            // example1: b8f397 => 072100
            // example2: 386a1f => ffffff
            // example3: 386666 => ffffff
            // example4: d9e7cb => 131f0d
            // example5: bbebec => 002020

            float luminance = 0.2126f * color.r + 0.7152f * color.g + 0.0722f * color.b;
            bool darken = luminance > 0.5f;

            // calculate the new color
            float adjustment = darken ? -0.5f : 0.5f;
            float r = color.r + adjustment;
            float g = color.g + adjustment;
            float b = color.b + adjustment;

            return new Color(r, g, b, color.a);
        }


        public static void SetColorWithoutAlpha(this Graphic graphic, Color color)
        {
            graphic.color = new Color(color.r, color.g, color.b, graphic.color.a);
        }
    }
}