using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public class TreeViewMenu
    {
        private const float k_ButtonWidth = 96f;
        private const float k_SearchFieldMaxWidth = 300f;
        private const float k_SearchFieldY = 2f;
        private const float k_DropdownMenuWidth = 120f;
        private const float k_DropdownMenuHeight = 20f;


        public string SearchString { get; private set; }
        public bool HasSearchField { get; private set; }

        private SearchField _searchField;

        private readonly Dictionary<string, TreeViewMenuItem> _menuItems;
        private readonly Action _onSearchStringUpdated;

        public TreeViewMenu(IEnumerable<TreeViewMenuItem> toolbarItems, Action onSearchStringUpdated)
        {
            _menuItems = new Dictionary<string, TreeViewMenuItem>();
            _onSearchStringUpdated = onSearchStringUpdated;

            foreach (TreeViewMenuItem item in toolbarItems)
            {
                if (item.MenuType == TreeViewMenuType.Custom)
                {
                    _menuItems.Add(item.CustomMenuName, item);
                }
                else if (item.MenuType == TreeViewMenuType.SearchField)
                {
                    HasSearchField = true;
                }
                else
                {
                    _menuItems.Add(item.MenuType.ToString(), item);
                }
            }
        }

        private Rect GetMenuButtonRect(int buttonIndex, int customWidth)
        {
            float menuButtonWidth = k_ButtonWidth;
            if (customWidth != -1) menuButtonWidth = customWidth;
            // y is always 20 because the toolbar height is 20
            return new Rect(menuButtonWidth * buttonIndex, 0, k_DropdownMenuWidth, k_DropdownMenuHeight);
        }

        private Rect GetSearchFieldRect(Rect position)
        {
            float leftOverViewWidth = position.width - k_ButtonWidth * _menuItems.Count;
            float searchFieldWidth = Mathf.Min(leftOverViewWidth, k_SearchFieldMaxWidth);
            return new Rect(position.width - searchFieldWidth, k_SearchFieldY, searchFieldWidth, 24);
        }

        internal void OnGUI(Rect position)
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                for (int i = 0; i < _menuItems.Count; i++)
                {
                    KeyValuePair<string, TreeViewMenuItem> kvp = _menuItems.ElementAt(i);

                    if (kvp.Value.Equals(null))
                    {
                        continue;
                    }

                    if (DrawMenuButton(kvp.Key, kvp.Value.Width))
                    {
                        kvp.Value.Action(GetMenuButtonRect(i, kvp.Value.Width));
                    }
                }

                GUILayout.FlexibleSpace();

                if (HasSearchField)
                {
                    DrawSearchField(GetSearchFieldRect(position));
                }
            }
            GUILayout.EndHorizontal();
        }

        private bool DrawMenuButton(string menuName, int customWidth)
        {
            float menuButtonWidth = k_ButtonWidth;
            if (customWidth != -1) menuButtonWidth = customWidth;
            return GUILayout.Button(menuName, EditorStyles.toolbarButton, GUILayout.Width(menuButtonWidth));
        }

        private void DrawSearchField(Rect rect)
        {
            _searchField ??= new();
            string searchString = _searchField.OnToolbarGUI(rect, SearchString);
            
            if (searchString != SearchString)
            {
                SearchString = searchString;
                _onSearchStringUpdated?.Invoke();
            }
        }
    }
}