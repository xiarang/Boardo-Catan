namespace Utils
{
    public class URL
    {
        private static string RoomName { get; set; }
        public static void SetRoomName(string value) => RoomName = value;
        private const string BaseURL = "http://192.168.1.6:8000";
        public static readonly string GetBoard = $"{BaseURL}/catan/map/{RoomName}";
        public static readonly string GetPlayers = $"{BaseURL}/catan/player_info/{RoomName}";
        public static readonly string Personal = $"{BaseURL}/catan/personal/{RoomName}";

    }
}