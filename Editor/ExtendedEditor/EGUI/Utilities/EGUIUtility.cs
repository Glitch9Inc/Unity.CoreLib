using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditorInternal;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Glitch9.ExtendedEditor
{
    public static partial class EGUIUtility
    {
        private static string _glitch9Directory;

        public static string GetGlitch9Directory()
        {
            if (_glitch9Directory == null)
            {
                string[] guids = AssetDatabase.FindAssets("Glitch9 t:folder");
                _glitch9Directory = AssetDatabase.GUIDToAssetPath(guids[0]);
            }
            return _glitch9Directory;
        }

        public static string GetTexturePath(string directory, string textureFileName)
        {
            string glitch9 = GetGlitch9Directory();
            if (string.IsNullOrEmpty(glitch9))
            {
                Debug.LogError("Glitch9 directory not found.");
                return null;
            }

            StringBuilder sb = new();
            sb.Append(glitch9);
            sb.Append("/");
            sb.Append(directory);
            sb.Append("/");
            sb.Append(textureFileName);
            return sb.ToString();
        }

        public static void XmlValueChange(string xmlPath, string index, string value)
        {
            XDocument doc = XDocument.Load(xmlPath);

            try
            {
                IEnumerable<XElement> valueToChange = (from p in doc.Descendants("string")
                                                       where p.Attribute("name")?.Value == index
                                                       select p);

                foreach (XElement element in valueToChange)
                {
                    if (element.Value != value)
                    {
                        element.Value = value;
                        doc.Save(xmlPath);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        public static void DrawHorizontalLine(float thickness = 1f, int padding = 0)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.x -= 20;
            r.width += 40;
            EditorGUI.DrawRect(r, new Color(162f / 255f, 162f / 255f, 162f / 255f));
        }

        public static void DrawTitleLine()
        {
            float thickness = 1.2f;
            float height = 5f;
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(height));
            r.height = thickness;
            r.width -= 4;
            EditorGUI.DrawRect(r, Color.gray);
        }

        public static void DrawCircle(Rect r, Texture2D image = null, float margin = 6)
        {
            Texture2D tex =
                (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Codeqo/GUISkins/circle_android12.psd",
                    typeof(Texture2D));
            GUI.DrawTexture(r, tex);
            r.width -= margin;
            r.height -= margin;
            r.x += margin / 2;
            r.y += margin / 2;
            GUI.DrawTexture(r, image);
        }

        public static Rect DrawRoundedTextureAndroid(float size, Texture2D tex)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Width(size - 2), GUILayout.Height(size - 2));
            GUI.DrawTexture(r, tex);
            r.width += 2;
            r.height += 2;
            r.x--;
            r.y--;
            GUI.Box(r, "", EGUI.skin.GetStyle("rounded_texture"));
            return r;
        }

        public static Rect DrawRoundedTextureiOS(float size, Texture2D tex)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Width(size - 2), GUILayout.Height(size - 2));
            GUI.DrawTexture(r, tex);
            r.width += 2;
            r.height += 2;
            r.x--;
            r.y--;

            Texture2D roundedCorners = EditorGUITextures.iOSRoundedCorners;
            GUIStyle style = new GUIStyle(EGUI.skin.GetStyle("rounded_texture"));
            style.normal.background = roundedCorners;
            GUI.Box(r, "", style);
            return r;
        }

        public static void OverrideSprite(SerializedProperty _obj, SerializedProperty _spr)
        {
            if (_obj.objectReferenceValue != null && _spr.objectReferenceValue == null)
            {
                GameObject _gObj = _obj.objectReferenceValue as GameObject;
                Image _img = _gObj.GetComponent<Image>();

                if (_img != null)
                {
                    _spr.objectReferenceValue = _gObj.GetComponent<Image>().sprite;
                    Debug.Log("Sprite found. This game object already has a sprite.");
                }
            }
        }

        public static void PingScriptableObject(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogError("Asset path is null or empty.");
                return;
            }

            if (!assetPath.EndsWith(".asset"))
            {
                Debug.LogError("Asset path is not a valid .asset file.");
                return;
            }

            // Load the object from the provided asset path
            ScriptableObject obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);

            if (obj != null)
            {
                // Set the object as the active selection
                Selection.activeObject = obj;

                // Ping the object in the project window to highlight it
                EditorGUIUtility.PingObject(obj);
            }
            else
            {
                Debug.LogError("Could not find an object at the specified path: " + assetPath);
            }
        }

        public static void DrawBorders(Rect rect)
        {
            float thickness = 1.2f;
            Color color = Color.gray;

            // Save the current GUI color
            Color previousColor = GUI.color;

            // Set the GUI color to the border color
            GUI.color = color;

            // Draw the border
            GUI.DrawTexture(new Rect(rect.x, rect.y, rect.width, thickness), EditorGUIUtility.whiteTexture);                     // Top
            GUI.DrawTexture(new Rect(rect.x, rect.y, thickness, rect.height), EditorGUIUtility.whiteTexture);                    // Left
            GUI.DrawTexture(new Rect(rect.x + rect.width - thickness, rect.y, thickness, rect.height), EditorGUIUtility.whiteTexture); // Right
            GUI.DrawTexture(new Rect(rect.x, rect.y + rect.height - thickness, rect.width, thickness), EditorGUIUtility.whiteTexture); // Bottom

            // Reset the GUI color to the previous color
            GUI.color = previousColor;
        }

        public static void DrawTopAndBottomBorders(Rect rect)
        {
            float thickness = 1.2f;
            Color color = Color.gray;

            // Save the current GUI color
            Color previousColor = GUI.color;

            // Set the GUI color to the border color
            GUI.color = color;

            // Draw the border
            GUI.DrawTexture(new Rect(rect.x, rect.y, rect.width, thickness), EditorGUIUtility.whiteTexture);                     // Top
            GUI.DrawTexture(new Rect(rect.x, rect.y + rect.height - thickness, rect.width, thickness), EditorGUIUtility.whiteTexture); // Bottom

            // Reset the GUI color to the previous color
            GUI.color = previousColor;
        }

        public static void DrawTopBorder(Rect rect)
        {
            float thickness = 1.2f;
            Color color = Color.gray;

            // Save the current GUI color
            Color previousColor = GUI.color;

            // Set the GUI color to the border color
            GUI.color = color;

            // Draw the border
            GUI.DrawTexture(new Rect(rect.x, rect.y, rect.width, thickness), EditorGUIUtility.whiteTexture);                     // Top      

            // Reset the GUI color to the previous color
            GUI.color = previousColor;
        }


        public static Texture2D CreateTexture(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        public static void RecalculateColumnWidths(Rect position, int horizontalPaddingsSum, ref MultiColumnHeader multiColumnHeader)
        {
            int numberOfColumns = multiColumnHeader.state.columns.Length;
            float fixedTotal = horizontalPaddingsSum;

            // Find the fixed width column indices
            List<int> autoResizeColumnIndices = new();

            for (int i = 0; i < numberOfColumns; i++)
            {
                if (multiColumnHeader.state.columns[i].autoResize)
                {
                    autoResizeColumnIndices.Add(i);
                }
                else
                {
                    fixedTotal += multiColumnHeader.state.columns[i].minWidth;
                }
            }

            // Distribute the remaining space among auto-resizable columns
            float remainingWidth = position.width - fixedTotal;
            float widthPerAutoResizeColumn = remainingWidth / autoResizeColumnIndices.Count;

            foreach (MultiColumnHeaderState.Column column in multiColumnHeader.state.columns)
            {
                column.width = column.autoResize ? widthPerAutoResizeColumn : column.minWidth;
            }
        }

        public static Rect AdjustTreeViewRect(Rect cellRect)
        {
            cellRect.width += 6;
            cellRect.x -= 3;
            return cellRect;
        }

        public static Rect AdjustTreeViewMenuRect(Rect cellRect)
        {
            //cellRect.width += 6;
            cellRect.x -= 3;
            cellRect.y -= 1.2f;
            return cellRect;
        }

        public static void DrawHorizontalBorder(float yPos)
        {
            GUI.DrawTexture(new Rect(0, yPos, EditorGUIUtility.currentViewWidth, 1.2f), borderTexture);
        }

        public static ReorderableList CreateReorderableArray<T>(T[] array, Func<Rect, int, T, T> customElementDrawer, Action<T[]> onEdited, GUIContent label = null, float rectHeightMultiplier = 1)
        {
            label ??= new GUIContent("List");

            ReorderableList reorderableList = new(array, typeof(T), true, true, true, true);

            // Draw Header
            reorderableList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, label);
            };

            const float PADDING_HORIZONTAL = 7;
            const float PADDING_VERTICAL = 4;
            const float SPACE = 3;
            const float Y_OFFSET = 2;

            reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                T element = array[index];

                rect.y += Y_OFFSET;

                Rect boxRect = rect;
                boxRect.height -= SPACE;

                // Draw a help box behind the custom element drawer
                GUI.Box(boxRect, GUIContent.none, EditorStyles.helpBox);

                // Adjust rect for the custom element to be drawn inside the help box
                Rect contentRect = new(boxRect.x + PADDING_HORIZONTAL,
                    boxRect.y + PADDING_VERTICAL,
                    boxRect.width - PADDING_HORIZONTAL * 2,
                    boxRect.height - PADDING_VERTICAL * 2 - SPACE);

                array[index] = customElementDrawer(contentRect, index, element);
            };

            float sumAllSpaces = PADDING_VERTICAL * 2 + SPACE;
            // Element height
            reorderableList.elementHeightCallback = (int index) => EditorGUIUtility.singleLineHeight * rectHeightMultiplier + sumAllSpaces;

            reorderableList.onAddCallback = (ReorderableList rl) =>
            {
                T[] oldArray = array;
                T[] newArray = new T[oldArray.Length + 1];

                for (int i = 0; i < oldArray.Length; i++)
                {
                    newArray[i] = oldArray[i];
                }

                newArray[newArray.Length - 1] = default(T);
                array = newArray;

                rl.list = array;

                onEdited?.Invoke(array);

                //rl.list.Add(default(T));
            };

            reorderableList.onRemoveCallback = (ReorderableList rl) =>
            {
                if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to delete this element?", "Yes", "No"))
                {
                    //ReorderableList.defaultBehaviours.DoRemoveButton(rl);

                    int removeIndex = rl.index;
                    if (removeIndex < 0 || removeIndex >= array.Length) return;

                    T[] oldArray = array;
                    T[] newArray = new T[oldArray.Length - 1];

                    int newPos = 0;
                    for (int i = 0; i < oldArray.Length; i++)
                    {
                        if (i == removeIndex) continue;

                        newArray[newPos] = oldArray[i];
                        newPos++;
                    }

                    array = newArray;
                    rl.list = array;

                    onEdited?.Invoke(array);
                }
            };

            return reorderableList;
        }

        public static ReorderableList CreateReorderableList<T>(T[] array, Func<Rect, int, T, T> customElementDrawer, GUIContent label, Func<int, float> heightMultiplierPerIndex)
        {
            label ??= new GUIContent("List");

            ReorderableList reorderableList = new(array, typeof(T), true, true, true, true);

            // Draw Header
            reorderableList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, label);
            };

            const float PADDING_HORIZONTAL = 7;
            const float PADDING_VERTICAL = 4;
            const float SPACE = 3;
            const float Y_OFFSET = 2;

            reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                T element = array[index];

                rect.y += Y_OFFSET;

                Rect boxRect = rect;
                boxRect.height -= SPACE;

                // Draw a help box behind the custom element drawer
                GUI.Box(boxRect, GUIContent.none, EditorStyles.helpBox);

                // Adjust rect for the custom element to be drawn inside the help box
                Rect contentRect = new(boxRect.x + PADDING_HORIZONTAL,
                    boxRect.y + PADDING_VERTICAL,
                    boxRect.width - PADDING_HORIZONTAL * 2,
                    boxRect.height - PADDING_VERTICAL * 2 - SPACE);

                array[index] = customElementDrawer(contentRect, index, element);
            };

            // Configure element height per index
            reorderableList.elementHeightCallback = (int index) =>
            {
                float rectHeightMultiplier = heightMultiplierPerIndex?.Invoke(index) ?? 1f;
                return EditorGUIUtility.singleLineHeight * rectHeightMultiplier + (PADDING_VERTICAL * 2 + SPACE);
            };

            reorderableList.onAddCallback = (ReorderableList rl) =>
            {
                rl.list.Add(default(T));
            };

            reorderableList.onRemoveCallback = (ReorderableList rl) =>
            {
                if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to delete this element?", "Yes", "No"))
                {
                    ReorderableList.defaultBehaviours.DoRemoveButton(rl);
                }
            };

            return reorderableList;
        }



        public static ReorderableList CreateReorderableDictionary(Dictionary<string, string> dictionary, GUIContent label = null, float rectHeightMultiplier = 1)
        {
            label ??= new GUIContent("Dictionary");

            // Convert the dictionary to a List of KeyValuePair
            List<KeyValuePair<string, string>> list = new(dictionary);

            ReorderableList reorderableList = new(list, typeof(KeyValuePair<string, string>), true, true, true, true);

            // Draw Header
            reorderableList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, label);
            };

            const float PADDING_HORIZONTAL = 7;
            const float PADDING_VERTICAL = 4;
            const float SPACE = 3;
            const float Y_OFFSET = 2;

            // Element drawing
            reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                KeyValuePair<string, string> entry = list[index];

                rect.y += Y_OFFSET;

                Rect boxRect = rect;
                boxRect.height -= SPACE;

                // Draw a help box behind the element fields
                GUI.Box(boxRect, GUIContent.none, EditorStyles.helpBox);

                // Adjust rect for the key-value pair to be drawn inside the help box
                Rect contentRect = new(boxRect.x + PADDING_HORIZONTAL,
                    boxRect.y + PADDING_VERTICAL,
                    boxRect.width - PADDING_HORIZONTAL * 2,
                    boxRect.height - PADDING_VERTICAL * 2 - SPACE);

                string key = EditorGUI.TextField(new Rect(contentRect.x, contentRect.y, contentRect.width / 2 - 2, contentRect.height), entry.Key);
                string value = EditorGUI.TextField(new Rect(contentRect.x + contentRect.width / 2 + 2, contentRect.y, contentRect.width / 2 - 2, contentRect.height), entry.Value);

                if (!key.Equals(entry.Key) || !value.Equals(entry.Value)) // Check if modified
                {
                    list[index] = new KeyValuePair<string, string>(key, value);
                    dictionary[entry.Key] = value; // Update dictionary
                    if (!key.Equals(entry.Key))
                    {
                        dictionary.Remove(entry.Key); // Remove old entry
                        dictionary.Add(key, value); // Add new entry
                    }
                }
            };

            // Handle element heights
            float sumAllSpaces = PADDING_VERTICAL * 2 + SPACE;
            reorderableList.elementHeightCallback = (int index) => EditorGUIUtility.singleLineHeight * rectHeightMultiplier + sumAllSpaces;

            // Add and remove callbacks
            reorderableList.onAddCallback = (ReorderableList rl) =>
            {
                rl.list.Add(new KeyValuePair<string, string>("newKey", "newValue"));
                dictionary.Add("newKey", "newValue");
            };

            reorderableList.onRemoveCallback = (ReorderableList rl) =>
            {
                KeyValuePair<string, string> entry = list[rl.index];
                if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to delete this element?", "Yes", "No"))
                {
                    dictionary.Remove(entry.Key);
                    ReorderableList.defaultBehaviours.DoRemoveButton(rl);
                }
            };

            return reorderableList;
        }



        public static void DragAndDropArea(Rect dropArea, Action<string> onDrop)
        {
            if (onDrop == null) return;

            Event evt = Event.current; // Get the current event

            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!dropArea.Contains(evt.mousePosition))
                        return;

                    // Change the visual mode
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        foreach (UnityEngine.Object draggedObject in DragAndDrop.objectReferences)
                        {
                            // Handle dragged object
                            string path = AssetDatabase.GetAssetPath(draggedObject);
                            Debug.Log("Dragged in file: " + path);
                            onDrop(path);
                        }
                    }
                    Event.current.Use();
                    break;
            }
        }
    }
}