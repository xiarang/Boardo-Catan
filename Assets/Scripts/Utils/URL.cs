using System.Collections.Generic;

namespace Utils
{
    public static class URL
    {
        private static string RoomName { get; set; }
        public static void SetRoomName(string value) => RoomName = value;
        private static string Token { get; set; }
        public static void SetToken(string value) => Token = value;

        public static Dictionary<string, string> Headers() => new Dictionary<string, string>
        {
            {"Authorization", $"Token {Token}"}
        };

        private const string BaseURL = "http://192.168.1.6:8000";
        private const string Game = "catan";
        private static string _buildPath(string path) => $"{BaseURL}/{Game}/{path}/{RoomName}";
        public static string GetBoard => _buildPath("map");
        public static string GetPlayers => _buildPath("player_info");
        public static string Personal => _buildPath("personal");
        public static string Pass => _buildPath("end");
        public static string Trade => _buildPath("trade");
        public static string TradeBank => _buildPath("trade_bank");
        public static string Init1 => _buildPath("init1");
        public static string Init2 => _buildPath("init2");
        public static string PlayYearOfPlenty => _buildPath("play_year_of_plenty");
        public static string PlayRoadBuilding => _buildPath("play_road_building");
        public static string PlayKnightCard => _buildPath("play_knight_card");
        public static string Dice => _buildPath("dice");
        public static string CreateHome => _buildPath("create_home");
        public static string CreateCity => _buildPath("create_city");
        public static string CreateRoad => _buildPath("create_road");
        public static string BuyDevelopmentCard => _buildPath("buy_development_card");
    }
}