using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Glitch9.ExEditor
{
    public class ExGUIUtility
    {
        public static Texture2D grayTexture
        {
            get
            {
                if (_grayTexture == null) _grayTexture = ExGUIUtility.CreateTexture(2, 2, new Color(0.7f, 0.7f, 0.7f, 1f));
                return _grayTexture;
            }
        }

        private static Texture2D _grayTexture;

        public static Texture2D darkGrayTexture
        {
            get
            {
                if (_darkGrayTexture == null) _darkGrayTexture = ExGUIUtility.CreateTexture(2, 2, new Color(0.65f, 0.65f, 0.65f, 1f));
                return _darkGrayTexture;
            }
        }

        private static Texture2D _darkGrayTexture;

        public static Texture2D borderTexture
        {
            get
            {
                if (_borderTexture == null) _borderTexture = ExGUIUtility.CreateTexture(2, 2, new Color(0.6f, 0.6f, 0.6f, 1f));
                return _borderTexture;
            }
        }

        private static Texture2D _borderTexture;


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

        public static Rect DrawRoundedTexture(float size, Texture2D tex)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Width(size - 2), GUILayout.Height(size - 2));
            GUI.DrawTexture(r, tex);
            r.width += 2;
            r.height += 2;
            r.x--;
            r.y--;
            GUI.Box(r, "", ExGUI.skin.GetStyle("rounded_texture"));
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

        public static void DrawHorizontalBorder(float yPos)
        {
            GUI.DrawTexture(new Rect(0, yPos, EditorGUIUtility.currentViewWidth, 1.2f), borderTexture);
        }
    }
}