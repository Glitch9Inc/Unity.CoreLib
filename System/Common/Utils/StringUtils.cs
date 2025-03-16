using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Glitch9
{
    public static class StringUtils
    {
        public static string JoinKeyValuePairs(params (string key, string value)[] values)
        {
            StringBuilder sb = StringBuilderPool.Get();
            foreach ((string key, string value) in values)
            {
                if (string.IsNullOrEmpty(value)) continue;
                sb.Append(key).Append(": ").Append(value).Append(", ");
            }

            sb.Length -= 1;
            return sb.ToString();
        }

        public static string ConvertToCase(this string input, StringCase stringCase)
        {
            return stringCase switch
            {
                StringCase.CamelCase => ToCamelCase(input),
                StringCase.PascalCase => ToPascalCase(input),
                StringCase.SnakeCase => ToSnakeCase(input),
                StringCase.KebabCase => ToKebabCase(input),
                StringCase.LowerCase => input.ToLowerInvariant(),
                StringCase.UpperCase => input.ToUpperInvariant(),
                _ => throw new ArgumentOutOfRangeException(nameof(stringCase), stringCase, null),
            };
        }

        private static string ToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string[] words = input.Split(new[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = i == 0 ? words[i].ToLower(CultureInfo.InvariantCulture) : Capitalize(words[i]);
            }
            return string.Concat(words);
        }

        private static string ToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string[] words = input.Split(new[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = Capitalize(words[i]);
            }
            return string.Concat(words);
        }

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return Regex.Replace(input, @"(\B[A-Z])", "_$1").ToLowerInvariant();
        }

        private static string ToKebabCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return Regex.Replace(input, @"(\B[A-Z])", "-$1").ToLowerInvariant();
        }

        private static string Capitalize(string word)
        {
            if (string.IsNullOrEmpty(word)) return word;
            return char.ToUpper(word[0], CultureInfo.InvariantCulture) + word.Substring(1).ToLower(CultureInfo.InvariantCulture);
        }

        public static string FixSlashes(this string path)
        {
            if (string.IsNullOrEmpty(path)) return path;
            return path.Replace('\\', '/').Replace("//", "/");
        }
    }
}