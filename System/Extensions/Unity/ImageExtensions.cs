using JetBrains.Annotations;
using UnityEngine;

namespace Glitch9
{
    public static class ImageExtensions
    {
        public static Sprite ToSprite([CanBeNull] Texture2D tex)
        {
            if (tex == null) return null;
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
        }
    }
}