// ReSharper disable All
namespace Glitch9.UI
{
    /// <summary>
    /// 자주 쓰는 UI 컴포넌트를 찾기 쉽게 하기 위해 사용되는 UI 타입입니다.
    /// </summary>
    public enum UIType
    {
        Unset,
        TMP_Title,
        TMP_Subtitle,
        TMP_Desc,
        IMG_Icon,
        IMG_Fill,
        IMG_Background,
        IMG_Illust,
    }

    public static class UITypeExtensions
    {
        public static string[] GetKeywords(this UIType uiType)
        {
            return uiType switch
            {
                UIType.Unset => null,
                UIType.TMP_Title => new[] { "title" },
                UIType.TMP_Subtitle => new[] { "subtitle" },
                UIType.TMP_Desc => new[] { "desc" },
                UIType.IMG_Icon => new[] { "icon", "ic" },
                UIType.IMG_Fill => new[] { "fill" },
                UIType.IMG_Background => new[] { "bg", "background" },
                UIType.IMG_Illust => new[] { "illust" },
                _ => null
            };
        }
    }
}