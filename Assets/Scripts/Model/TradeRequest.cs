using System;

namespace Model
{
    [Serializable]
    public class TradeRequest
    {
        public Trade give;
        public Trade want;
    }

    [Serializable]
    public class Trade
    {
        public int wheat;
        public int stone;
        public int wood;
        public int sheep;
        public int brick;
    }
}