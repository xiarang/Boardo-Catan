using System;
using UnityEngine;

namespace Utils
{
    public enum PlayerColors
    {
        Blue,
        Green,
        Orange,
        Red,
    }
    public static class Utils
    {
        public static Color GetColor(this PlayerColors playerColors)
        {
            switch (playerColors)
            {
                case PlayerColors.Red:
                    return new Color(254f / 255f, 0f, 0f, 1f);
                case PlayerColors.Blue:
                    return new Color(0f, 174f/255f, 240f/255f, 1f);
                case PlayerColors.Green:
                    return new Color(96f/255f, 207f/255f, 64f/255f, 1f);
                case PlayerColors.Orange:
                    return new Color(255f/255f, 153f/255f, 51f/255f, 1f);
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerColors), playerColors, null);
            }
        }
    }
}
