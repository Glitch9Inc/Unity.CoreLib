using System;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    public class EGUIButtonEntry
    {
        public GUIContent label;
        public Action action;

        public EGUIButtonEntry(string label, Action action)
        {
            this.label = new GUIContent(label);
            this.action = action;
        }

        public EGUIButtonEntry(GUIContent label, Action action)
        {
            this.label = label;
            this.action = action;
        }
    }
}