using UnityEngine;

namespace Utils
{
    public static class Utility
    {
        public static Sprite ToSprite(this Texture resource)
        {
            var tex = (Texture2D) resource;
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f),
                100.0f);
            return sprite;
        }
    }
}