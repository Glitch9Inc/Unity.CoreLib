using Glitch9.ExEditor;

namespace Glitch9.IO.Git
{
    public class VersionTypeSelector : EditorSelectorPopup<VersionTypeSelector, VersionIncrement>
    {
        protected override VersionIncrement DrawContent(VersionIncrement value)
        {
            if (ExGUILayout.EnumToolbar(value, out VersionIncrement updated))
            {
                return updated;
            }
            return value;
        }
    }
}