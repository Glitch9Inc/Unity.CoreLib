using System;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    /// <summary>
    /// Represents a toolbar item for a tree view.
    /// </summary>
    public class TreeViewToolbarItem
    {
        public TreeViewToolbarMenu Menu { get; set; }
        public string CustomMenuName { get; set; }
        public Action<Rect> Action { get; set; }

        public TreeViewToolbarItem(TreeViewToolbarMenu menu, Action<Rect> action = null)
        {
            Menu = menu;
            Action = action;
        }

        public TreeViewToolbarItem(string customMenuName, Action<Rect> action)
        {
            Menu = TreeViewToolbarMenu.Custom;
            CustomMenuName = customMenuName;
            Action = action;
        }
    }
}