using UnityEngine;
using UnityEngine.UI;
using Utils;

public class MainScreen : MonoBehaviour
{
    public static Color PlayerColor;
    public static int ThiefResourceNumber;

    [SerializeField] SpriteRenderer[] boardTiles;
    [SerializeField] SpriteRenderer[] tilTags;
    [SerializeField] Sprite[] resources;
    [SerializeField] Sprite[] numbers;

    private Tiles _catanBoard;
    public static Players Players;
    private PlayerScores[] _playersScoreboard;

    private void InitBoard()
    {
        for (int i = 0; i < 19; i++)
        {
            var tile = _catanBoard.board[i];
            tilTags[i].sprite = numbers[tile.number - 2];
            boardTiles[i].sprite = resources[GetResourceID(tile.resource)];
        }
    }

    void Start()
    {
        _playersScoreboard = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponentsInChildren<PlayerScores>();
        GetBoardInfo();
        GetPlayers();
        SetPlayerColor("green");
    }

    private void GetPlayers()
    {
        string url = URL.BaseURL + URL.GetPlayers + "9b717be4-a042-4b94-837f-b673f13d3241";
        StartCoroutine(Network.GetRequest(url, PlayerInit));
    }

    private void GetBoardInfo()
    {
        string url = URL.BaseURL + URL.GetBoard + "9b717be4-a042-4b94-837f-b673f13d3241";
        StartCoroutine(Network.GetRequest(url, BoardInit));
    }


    private void PlayerInit(string response)
    {
        response = "{\"otherPlayers\":" + response + "}";
        Players = JsonUtility.FromJson<Players>(response);
        for (var index = 0; index < _playersScoreboard.Length; index++)
        {
            var item =  _playersScoreboard[index];
            item.InitViews(Players.otherPlayers[index]);
        }
    }

    private void BoardInit(string response)
    {
        response = "{\"board\":" + response + "}";
        _catanBoard = JsonUtility.FromJson<Tiles>(response);
        InitBoard();
    }

    void SetPlayerColor(string color)
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

    int GetResourceID(string resource)
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