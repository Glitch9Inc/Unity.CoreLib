using UnityEngine;

namespace Glitch9
{
    public static class ImageExtensions
    {
        public static Sprite ToSprite(this Texture2D texture)
        {
            if (texture == null) return null;
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
    }
}