using Glitch9.ExtendedEditor;
using UnityEngine;

namespace Glitch9.Internal.Git
{
    public class GitVersionSelector : EditorSelectorPopup<GitVersionSelector, VersionIncrement>
    {
        protected override void Initialize()
        {
            minSize = new Vector2(WINDOW_WIDTH, 170);
            maxSize = new Vector2(WINDOW_WIDTH, 170);
        }

        protected override VersionIncrement DrawContent(VersionIncrement value)
        {
            if (EGUILayout.EnumToolbar(value, out VersionIncrement updated))
            {
                value = updated;
            }
            return value;
        }
    }
}