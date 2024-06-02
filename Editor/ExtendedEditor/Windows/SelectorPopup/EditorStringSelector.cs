namespace Glitch9.ExtendedEditor
{
    /// <summary>
    /// Provides a popup for selecting a string value
    /// </summary>
    public class EditorStringSelector : EditorSelectorPopup<EditorStringSelector, string>
    {
        protected override string DrawContent(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            int index = ValueList.IndexOf(value);
            index = EGUILayout.StringListToolbar(index, ValueList, null, 1);
            if (index < 0) return value;
            if (index >= ValueList.Count) return value;
            return ValueList[index];
        }
    }
}