using System.Text;

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
    }
}