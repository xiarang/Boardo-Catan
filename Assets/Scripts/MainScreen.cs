using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utils;
using Network = Utils.Network;
using RTLTMPro;
using UnityEditor;

// actions -> init1, init2 : first enable settlement click and after that road click
// play_development_card -> if has any dev card: play or not=> need rolled dice button | else -> post to dice
// trade_buy_build -> enable buy button in my player ui
// thief_tile -> tell player to select new thief position and post that to server
// dice -> need a button
// trade_question -> show a dialog accept or not

public class MainScreen : MonoBehaviour
{
    public static int ThiefResourceNumber;

    [SerializeField] private SpriteRenderer[] boardTiles;
    [SerializeField] private SpriteRenderer[] tileTags;
    [SerializeField] private Sprite[] resources;
    [SerializeField] private Sprite[] numbers;

    private Tiles _catanBoard;
    public static Players Players;
    private UpdateMyPlayer _myPlayer;
    private PlayerScores[] _playersScoreboard;
    private Canvas _canvas;
    private Scene _scene;
    private Road[] _boardRoads;
    private Settlement[] _boardSettlements;

    public static int ThisPlayerID;
    public static PlayerColors ThisPlayerPlayerColor;
    public static RTLTextMeshPro BoxMessage;
    public static string SelectedSettlement;
    public static string[] SelectedRoad = {"0", "0"};

    [DllImport("__Internal")]
    public static extern void showDialog(string label, string body);

    [DllImport("__Internal")]
    public static extern void showDialogWithImage(string label, string body, string image);

    [DllImport("__Internal")]
    public static extern void rollDice(string dice1, string dice2);

    private void InitBoard()
    {
        for (var i = 0; i < 19; i++)
        {
            var tile = _catanBoard.board[i];
            tileTags[i].sprite = numbers[tile.number - 2];
            boardTiles[i].sprite = resources[GetResourceID(tile.resource)];
        }
    }

    private void Start()
    {
        // PlayerSettings.SetAspectRatio(AspectRatio.Aspect16by10, true);
        //todo: change by getting init1 from server and cityClickable
        GameController.Action = GameState.init1;
        GameController.ShouldSettlementClickable = true;
        BoxMessage = GameObject.Find("notifyMessage").GetComponent<RTLTextMeshPro>();
        BoxMessage.text = "مکان خانه اول را مشخص کنید.";

        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _playersScoreboard = _canvas.GetComponentsInChildren<PlayerScores>();
        FindBoardElements();
        _myPlayer = _canvas.GetComponentInChildren<UpdateMyPlayer>();
        URL.SetToken("58998a8632efec6b3810f7a2833dc300fe2a937f");
        URL.SetRoomName("9b717be4-a042-4b94-837f-b673f13d3241");
        _myPlayer.UpdatePlayer();
        GetBoardInfo();
        GetPlayers();
    }

    private void FindBoardElements()
    {
        foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            switch (item.name)
            {
                case "roads":
                    _boardRoads = item.GetComponentsInChildren<Road>();
                    Debug.Log(_boardRoads.Length + " h " + _boardSettlements.Length);
                    break;
                case "settlements":
                    _boardSettlements = item.GetComponentsInChildren<Settlement>();
                    break;
            }
        }
    }

    private void GetPlayers()
    {
        StartCoroutine(Network.GetRequest(URL.GetPlayers, response =>
        {
            response = "{\"otherPlayers\":" + response + "}";
            Players = JsonUtility.FromJson<Players>(response);
            var index = 0;
            foreach (var player in _playersScoreboard)
            {
                if (Players.otherPlayers[index].player == ThisPlayerID)
                {
                    ThisPlayerPlayerColor = (PlayerColors) index;
                    _myPlayer.UpdateColor(ThisPlayerPlayerColor);
                    index++;
                }

                Players.otherPlayers[index].Color = (PlayerColors) index;
                player.InitViews(Players.otherPlayers[index]);
                index++;
            }
        }, URL.Headers()));
    }

    private void GetBoardInfo()
    {
        StartCoroutine(Network.GetRequest(URL.GetBoard, response =>
        {
            response = $"{{\"board\":{response}}}";
            _catanBoard = JsonUtility.FromJson<Tiles>(response);
            InitBoard();
        }, URL.Headers()));
    }

    public static int GetResourceID(string resource)
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

    /*
     * These are functions that will be called by js.
     **/

    /*
     * if it's your turn, play it.
     */
    public void PlayInit1(string[] args)
    {
        var turn = int.Parse(args[0]);
    }

    public void PlayInit2(string[] args)
    {
        var turn = int.Parse(args[0]);
    }

    public void PlayedInit1(string[] args)
    {
        var turn = int.Parse(args[0]);
        var vertex = int.Parse(args[1]);
        var road1 = int.Parse(args[2]);
        var road2 = int.Parse(args[3]);
    }

    public void PlayedInit2(string[] args)
    {
        var turn = int.Parse(args[0]);
        var vertex = int.Parse(args[1]);
        var road1 = int.Parse(args[2]);
        var road2 = int.Parse(args[3]);
    }

    public void PlayYearOfPlenty(string[] args)
    {
        var turn = int.Parse(args[0]);
        var resource1 = args[1];
        var resource2 = args[2];
    }

    public void PlayRoadBuilding(string[] args)
    {
        var turn = args[0];
        var r1v1 = int.Parse(args[1]);
        var r1v2 = int.Parse(args[2]);
        var r2v1 = int.Parse(args[3]);
        var r2v2 = int.Parse(args[4]);
    }
}