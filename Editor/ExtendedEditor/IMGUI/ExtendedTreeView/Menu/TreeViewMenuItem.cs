using System;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    /// <summary>
    /// Represents a toolbar item for a tree view.
    /// </summary>
    public class TreeViewMenuItem
    {
        public TreeViewMenuType MenuType { get; set; }
        public string CustomMenuName { get; set; }
        public Action<Rect> Action { get; set; }
        public int Width { get; set; }
 
        public TreeViewMenuItem(TreeViewMenuType menuType, Action<Rect> action = null, int width = -1)
        {
            MenuType = menuType;
            Action = action;
            Width = width;
        }

        public TreeViewMenuItem(string customMenuName, Action<Rect> action, int width = -1)
        {
            MenuType = TreeViewMenuType.Custom;
            CustomMenuName = customMenuName;
            Action = action;
            Width = width;
        }
    }
}