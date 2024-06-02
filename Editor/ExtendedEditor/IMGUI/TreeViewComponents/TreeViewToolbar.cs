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
        public string SearchString { get; private set; }
        public bool HasSearchField { get; private set; }
        
        private SearchField _searchField;
    
        private readonly Dictionary<string, TreeViewToolbarItem> _menuItems;

        public TreeViewToolbar(IEnumerable<TreeViewToolbarItem> toolbarItems)
        {
            _menuItems = new Dictionary<string, TreeViewToolbarItem>();

            foreach (TreeViewToolbarItem item in toolbarItems)
            {
                if (item.Menu == TreeViewToolbarMenu.Custom)
                {
                    _menuItems.Add(item.CustomMenuName, item);
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

        internal void OnGUI()
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

                    if (kvp.Value.Menu == TreeViewToolbarMenu.SearchField)
                    {
                        DrawSearchField(GetMenuButtonRect(i));
                        continue;
                    }

                    if (DrawMenuButton(kvp.Key))
                    {
                        kvp.Value.Action(GetMenuButtonRect(i));
                    }
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
            InitializeSearchField();
            SearchString = _searchField.OnToolbarGUI(rect, SearchString);
        }

        private void InitializeSearchField()
        {
            if (_searchField == null)
            {
                _searchField = new();
                HasSearchField = true;
            }
        }
    }
}