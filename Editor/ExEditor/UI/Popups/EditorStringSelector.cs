using Glitch9.ExEditor;

namespace Glitch9
{
    public class EditorStringSelector : EditorSelectorPopup<EditorStringSelector, string>
    {
        /// <summary>
        /// 스트링을 복수로 보내서 1개를 선택하게 하는 팝업
        /// </summary>
        protected override string DrawContent(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            int index = ValueList.IndexOf(value);
            index = ExGUILayout.StringListToolbar(index, ValueList, null, 1);
            if (index < 0) return value;
            if (index >= ValueList.Count) return value;
            return ValueList[index];
        }
    }
}