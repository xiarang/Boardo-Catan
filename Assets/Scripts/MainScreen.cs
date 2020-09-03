using UnityEngine;
using Utils;
using Network = Utils.Network;

public class MainScreen : MonoBehaviour
{
    public static Color PlayerColor;
    public static int ThiefResourceNumber;

    [SerializeField] private SpriteRenderer[] boardTiles;
    [SerializeField] private SpriteRenderer[] tilTags;
    [SerializeField] private Sprite[] resources;
    [SerializeField] private Sprite[] numbers;

    private Tiles _catanBoard;
    private static Players _players;
    private PlayerScores[] _playersScoreboard;

    private void InitBoard()
    {
        for (var i = 0; i < 19; i++)
        {
            var tile = _catanBoard.board[i];
            tilTags[i].sprite = numbers[tile.number - 2];
            boardTiles[i].sprite = resources[GetResourceID(tile.resource)];
        }
    }

    private void UpdatePersonal()
    {
        
    }

    private void Start()
    {
        _playersScoreboard = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponentsInChildren<PlayerScores>();
        GetBoardInfo();
        GetPlayers();
        SetPlayerColor("green");
    }

    private void GetPlayers()
    {
        URL.SetRoomName("9b717be4-a042-4b94-837f-b673f13d3241");
        StartCoroutine(Network.GetRequest(URL.GetPlayers, PlayerInit));
    }

    private void GetBoardInfo()
    {
        URL.SetRoomName("9b717be4-a042-4b94-837f-b673f13d3241");
        StartCoroutine(Network.GetRequest(URL.GetBoard, BoardInit));
    }


    private void PlayerInit(string response)
    {
        response = "{\"otherPlayers\":" + response + "}";
        _players = JsonUtility.FromJson<Players>(response);
        for (var index = 0; index < _playersScoreboard.Length; index++)
        {
            var item = _playersScoreboard[index];
            item.InitViews(_players.otherPlayers[index]);
        }
    }

    private void BoardInit(string response)
    {
        response = "{\"board\":" + response + "}";
        _catanBoard = JsonUtility.FromJson<Tiles>(response);
        InitBoard();
    }

    static void SetPlayerColor(string color)
    {
        switch (color)
        {
            case "blue":
                PlayerColor = Colors.Blue;
                break;
            case "green":
                PlayerColor = Colors.Green;
                break;
            case "red":
                PlayerColor = Colors.Red;
                break;
            case "orange":
                PlayerColor = Colors.Orange;
                break;
        }
    }

    static int GetResourceID(string resource)
    {
        switch (resource)
        {
            case "brick":
                return 2;
            case "sheep":
                return 4;
            case "wheat":
                return 0;
            case "wood":
                return 1;
            case "stone":
                return 3;
            case "desert":
                return 5;
        }

        return -1;
    }
}