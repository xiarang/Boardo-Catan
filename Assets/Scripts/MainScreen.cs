using System.Collections.Generic;
using Model;
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

    //todo: remove id after get personal
    private int id = 22;
    private string _personalProfileResource;
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
        // URL.SetRoomName("9b717be4-a042-4b94-837f-b673f13d3241");
        var header = new Dictionary<string, string>
        {
            {"Authorization", "Token 58998a8632efec6b3810f7a2833dc300fe2a937f"}
        };
        URL.SetRoomName("9b717be4-a042-4b94-837f-b673f13d3241");
        StartCoroutine(Network.GetRequest(URL.GetPlayers(), PlayerInit, header));
    }

    private void GetBoardInfo()
    {
        URL.SetRoomName("9b717be4-a042-4b94-837f-b673f13d3241");
        // URL.SetRoomName("9b717be4-a042-4b94-837f-b673f13d3241");
        var header = new Dictionary<string, string>
        {
            {"Authorization", "Token 58998a8632efec6b3810f7a2833dc300fe2a937f"}
        };
        StartCoroutine(Network.GetRequest(URL.GetBoard(), BoardInit, header));
    }


    private void PlayerInit(string response)
    {
        response = "{\"otherPlayers\":" + response + "}";
        _players = JsonUtility.FromJson<Players>(response);
        int index = 0;
        foreach (var player in _playersScoreboard)
        {
            if (_players.otherPlayers[index].player == id)
            {
                _personalProfileResource = _players.otherPlayers[index].player_avatar;
                index++;
            }
            player.InitViews(_players.otherPlayers[index]);


            index++;
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