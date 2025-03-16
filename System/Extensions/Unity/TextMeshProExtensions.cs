using TMPro;
using UnityEngine;

namespace Glitch9
{
    public static class TextMeshProExtensions
    {
        public static void SetColor(this TextMeshProUGUI tmp, Color color)
        {
            tmp.color = color;
        }

        public static int GetLineCount(this TextMeshProUGUI tmp)
        {
            return tmp.textInfo.lineCount;
        }

        public static float GetHeight(this TextMeshProUGUI tmp)
        {
            return tmp.preferredHeight;
        }

        public static void SetTextOrHide(this TextMeshProUGUI text, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                text.gameObject.SetActive(false);
            }
            else
            {
                text.gameObject.SetActive(true);
                text.text = value;
            }
        }
    }
}
