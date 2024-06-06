using System.Collections.Generic;
using Glitch9.UI;
using UnityEditor;
using UnityEngine;

namespace Glitch9.ExtendedEditor
{
    public static partial class EGUIUtility
    {
        private static readonly Dictionary<string, Texture2D> _textures = new();

        private static Texture2D GetTexture(string key)
        {
            return _textures.GetValueOrDefault(key);
        }

        private static Texture2D CreateColorTexture(string key, Color color)
        {
            Texture2D texture = CreateTexture(2, 2, color);
            _textures.Add(key, texture);
            return texture;
        }

        public static Texture2D GetColorTexture(GUIColor color)
        {
            return color switch
            {
                GUIColor.Green => greenTexture,
                GUIColor.Purple => purpleTexture,
                GUIColor.Yellow => yellowTexture,
                GUIColor.Blue => blueTexture,
                GUIColor.Red => redTexture,
                GUIColor.Orange => orangeTexture,
                GUIColor.White => EditorGUIUtility.whiteTexture,
                _ => grayTexture
            };
        }


        public static Texture2D grayTexture
        {
            get
            {
                string key = nameof(grayTexture);
                Texture2D texture = GetTexture(key);
                if (texture != null) return texture;
                return CreateColorTexture(key, new Color(0.8f, 0.8f, 0.8f, 1f));
            }
        }

        public static Texture2D darkGrayTexture
        {
            get
            {
                string key = nameof(darkGrayTexture);
                Texture2D texture = GetTexture(key);
                if (texture != null) return texture;
                return CreateColorTexture(key, new Color(0.65f, 0.65f, 0.65f, 1f));
            }
        }



        public static Texture2D borderTexture
        {
            get
            {
                string key = nameof(borderTexture);
                Texture2D texture = GetTexture(key);
                if (texture != null) return texture;
                return CreateColorTexture(key, new Color(0.6f, 0.6f, 0.6f, 1f));
            }
        }


        public static Texture2D greenTexture
        {
            get
            {
                string key = nameof(greenTexture);
                Texture2D texture = GetTexture(key);
                if (texture != null) return texture;
                return CreateColorTexture(key, new Color(0.75f, 0.9f, 0.75f, 1f));
            }
        }

        public static Texture2D purpleTexture
        {
            get
            {
                string key = nameof(purpleTexture);
                Texture2D texture = GetTexture(key);
                if (texture != null) return texture;
                return CreateColorTexture(key, new Color(0.75f, 0.75f, 0.9f, 1f));
            }
        }

        public static Texture2D yellowTexture
        {
            get
            {
                string key = nameof(yellowTexture);
                Texture2D texture = GetTexture(key);
                if (texture != null) return texture;
                return CreateColorTexture(key, new Color(0.9f, 0.9f, 0.75f, 1f));
            }
        }

        public static Texture2D blueTexture
        {
            get
            {
                string key = nameof(blueTexture);
                Texture2D texture = GetTexture(key);
                if (texture != null) return texture;
                return CreateColorTexture(key, new Color(0.6f, 0.75f, 0.9f, 1f));
            }
        }

        public static Texture2D redTexture
        {
            get
            {
                string key = nameof(redTexture);
                Texture2D texture = GetTexture(key);
                if (texture != null) return texture;
                return CreateColorTexture(key, new Color(0.9f, 0.75f, 0.75f, 1f));
            }
        }

        public static Texture2D orangeTexture
        {
            get
            {
                string key = nameof(orangeTexture);
                Texture2D texture = GetTexture(key);
                if (texture != null) return texture;
                return CreateColorTexture(key, new Color(0.9f, 0.75f, 0.6f, 1f));
            }
        }
    }
}