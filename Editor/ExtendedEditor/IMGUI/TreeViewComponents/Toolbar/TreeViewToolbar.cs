using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Glitch9.ExtendedEditor.IMGUI
{
    public class TreeViewToolbar
    {
        private const float k_ButtonWidth = 90f;
        private const float k_SearchFieldMaxWidth = 300f;
        private const float k_SearchFieldY = 2f;


        public string SearchString { get; private set; }
        public bool HasSearchField { get; private set; }

        private SearchField _searchField;

        private readonly Dictionary<string, TreeViewToolbarItem> _menuItems;
        private readonly Action _onSearchStringUpdated;

        public TreeViewToolbar(IEnumerable<TreeViewToolbarItem> toolbarItems, Action onSearchStringUpdated)
        {
            _menuItems = new Dictionary<string, TreeViewToolbarItem>();
            _onSearchStringUpdated = onSearchStringUpdated;

            foreach (TreeViewToolbarItem item in toolbarItems)
            {
                if (item.Menu == TreeViewToolbarMenu.Custom)
                {
                    _menuItems.Add(item.CustomMenuName, item);
                }
                else if (item.Menu == TreeViewToolbarMenu.SearchField)
                {
                    HasSearchField = true;
                }
                else
                {
                    _menuItems.Add(item.Menu.ToString(), item);
                }
            }
        }

        private Rect GetMenuButtonRect(int buttonIndex)
        {
            // y is always 20 because the toolbar height is 20
            return new Rect(k_ButtonWidth * buttonIndex, 0, 100, 20);
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
                    KeyValuePair<string, TreeViewToolbarItem> kvp = _menuItems.ElementAt(i);

                    if (kvp.Value.Equals(null))
                    {
                        continue;
                    }

                    if (DrawMenuButton(kvp.Key))
                    {
                        kvp.Value.Action(GetMenuButtonRect(i));
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

        private bool DrawMenuButton(string menuName)
        {
            return GUILayout.Button(menuName, EditorStyles.toolbarButton, GUILayout.Width(k_ButtonWidth));
        }

        private void DrawSearchField(Rect rect)
        {
            _searchField ??= new();
            string searchString = _searchField.OnToolbarGUI(rect, SearchString);
            
            if (searchString != SearchString)
            {
                SearchString = searchString;
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    _onSearchStringUpdated?.Invoke();
                }
            }
        }
    }
}