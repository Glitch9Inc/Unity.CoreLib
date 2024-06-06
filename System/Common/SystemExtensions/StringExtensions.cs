using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Vector2 = UnityEngine.Vector2;
using Vector4 = UnityEngine.Vector4;

namespace Glitch9
{
    public static class StringExtensions
    {
        public static bool LengthCheck(this string text, int minLength, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                GNLog.Warning("Text is null or empty.");
                return false;
            }

            if (text.Length < minLength)
            {
                GNLog.Warning($"Text is too short. Min: {minLength}, Current: {text.Length}");
                return false;
            }

            if (text.Length > maxLength)
            {
                GNLog.Warning($"Text is too long. Max: {maxLength}, Current: {text.Length}");
                return false;
            }

            return true;
        }

        public static int ToInt(this string text)
        {
            if (int.TryParse(text, out int result))
            {
                return result;
            }
            else
            {
                GNLog.Error($"Failed to parse int: {text}");
                return 0;
            }
        }

        public static string RemoveLineBreaks(this string text)
        {
            return text.Replace("\r", "").Replace("\n", "");
        }

        public static string Format(this string text, params object[] args)
        {
            try
            {
                return args.IsNullOrEmpty() ? text : string.Format(text, args);
            }
            catch (Exception e)
            {
                GNLog.Exception(e);
                return text;
            }
        }

        public static string VectorToString(this Vector2 vector)
        {
            return $"{vector.x},{vector.x}";
        }

        public static string VectorToString(this Vector4 vector)
        {
            return $"{vector.x},{vector.x},{vector.z},{vector.w}";
        }

        public static Vector2 StringToVector2(this string vectorString)
        {
            string[] components = vectorString.Split(',');

            if (components.Length == 2 && float.TryParse(components[0], out float x) && float.TryParse(components[1], out float y))
            {
                return new Vector2(x, y);
            }
            else
            {
                GNLog.Error($"Failed to parse vector string: {vectorString}");
                return Vector2.zero;
            }
        }

        public static Vector4 StringToVector4(this string vectorString)
        {
            string[] components = vectorString.Split(',');

            if (components.Length == 4 && float.TryParse(components[0], out float x) && float.TryParse(components[1], out float y) && float.TryParse(components[2], out float z) && float.TryParse(components[3], out float w))
            {
                return new Vector4(x, y, z, w);
            }
            else
            {
                GNLog.Error($"Failed to parse vector string: {vectorString}");
                return Vector4.zero;
            }
        }

        public static string CapitalizeFirstLetter(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return char.ToUpper(text[0]) + text.Substring(1);
        }

        public static string[] SafeSplit(this string text, char separator)
        {
            if (string.IsNullOrEmpty(text)) return new string[0];
            if (!text.Contains(separator))
            {
                GNLog.Warning($"'{text}' does not contain '{separator}'");
                return new string[] { text };
            }
            return text.Split(separator);
        }

        public static bool Search(this string text, string keyword)
        {
            if (string.IsNullOrEmpty(text)) return false;
            if (string.IsNullOrEmpty(keyword)) return false;
            return text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static StringBuilder AppendBlue(this StringBuilder builder, string text)
        {
            return builder.Append($"<color=blue>{text}</color>");
        }

        public static StringBuilder AppendRed(this StringBuilder builder, string text)
        {
            return builder.Append($"<color=red>{text}</color>");
        }

        public static StringBuilder AppendGreen(this StringBuilder builder, string text)
        {
            return builder.Append($"<color=green>{text}</color>");
        }

        public static string ToSnakeCase(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            StringBuilder sb = new();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    if (i > 0 && sb[sb.Length - 1] != '_') sb.Append('_');
                    sb.Append(char.ToLower(c));
                }
                else
                {
                    sb.Append(c);
                }
            }

            string result = sb.ToString();
            result = Regex.Replace(result, "_{2,}", "_");
            result = result.Trim('_');
            return result.ToLower();
        }

        public static string ToPascalCase(this string text)
        {
            return text.SmartCapitalizationWithUnderscores(false)?.Replace("_", ""); // 최종 결과에서 언더스코어 제거
        }

        public static string ToDisplayText(this string text)
        {
            return text.SmartCapitalizationWithUnderscores(true)?.Replace("_", ""); // 최종 결과에서 언더스코어 제거
        }

        private static string SmartCapitalizationWithUnderscores(this string text, bool addSpace)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            // 예외 단어를 포함한 문자열 분할
            List<string> words = Regex.Split(text, "(_+)")
                .Where(s => !string.IsNullOrEmpty(s) && s != "_")
                .ToList();

            StringBuilder sb = new();
            foreach (string word in words)
            {
                sb.Append(char.ToUpper(word[0]) + word.Substring(1));
                if (addSpace) sb.Append(" ");
            }
            return sb.ToString();
        }

        public static string FixSpaces(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            // 대문자 뒤에 공백이 없는 경우 공백 추가
            // 하지만 대문자가 2번이상 연속되는 경우는 공백 추가하지 않음
            StringBuilder sb = new();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    if (i > 0 && text[i - 1] != ' ' && (i == text.Length - 1 || text[i + 1] != ' ')) sb.Append(' ');
                }
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}