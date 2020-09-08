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
        public static string GetBoard() => $"{BaseURL}/catan/map/{RoomName}";
        public static string GetPlayers() => $"{BaseURL}/catan/player_info/{RoomName}";
        public static string Personal() => $"{BaseURL}/catan/personal/{RoomName}";
        public static string Pass() => $"{BaseURL}/catan/end/{RoomName}";
        public static string Trade() => $"{BaseURL}/catan/trade/{RoomName}";
        public static string TradeBank() => $"{BaseURL}/catan/trade_bank/{RoomName}";
        public static string Init1() => $"{BaseURL}/catan/init1/{RoomName}";
        public static string Init2() => $"{BaseURL}/catan/init2/{RoomName}";

    }
}