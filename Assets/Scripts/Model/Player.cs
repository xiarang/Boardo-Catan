using System;
using Utils;

namespace Model
{
    [Serializable]
    public class Player
    {
        public int catan_event;
        public int player;
        public bool has_long_road_card;
        public bool has_largest_army;
        public int knight_card_played;
        public int cards;
        public int resources;
        public int point;
        public int road_length;
        public string player_avatar;
        public string player_username;
        [NonSerialized] public PlayerColors Color;
    }

    [Serializable]
    public class Players
    {
        public Player[] otherPlayers;
    }
}