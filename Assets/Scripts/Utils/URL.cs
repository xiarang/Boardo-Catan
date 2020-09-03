namespace Utils
{
    public class URL
    {
        private static string RoomName { get; set; }
        public static string Token = "Token 58998a8632efec6b3810f7a2833dc300fe2a937f";
        public static void SetRoomName(string value) => RoomName = value;
        private const string BaseURL = "http://192.168.1.6:8000";
        public static readonly string GetBoard = $"{BaseURL}/catan/map/";
        public static readonly string GetPlayers = $"{BaseURL}/catan/player_info/";
        public static readonly string Personal = $"{BaseURL}/catan/personal/";

    }
}