using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utils;
using Network = Utils.Network;
using RTLTMPro;

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
    private TurnHandler[] _turnHandlers;

    public static int ThisPlayerID;
    public static PlayerColors ThisPlayerPlayerColor;
    public static RTLTextMeshPro BoxMessage;
    public static string SelectedSettlement;

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
        //todo: change by getting init1 from server and cityClickable
        GameController.Action = GameState.init1;
        GameController.ShouldSettlementClickable = true;
        BoxMessage = GameObject.Find("notifyMessage").GetComponent<RTLTextMeshPro>();
        BoxMessage.text = "مکان خانه اول را مشخص کنید.";

        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _turnHandlers = _canvas.GetComponentsInChildren<TurnHandler>();
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
                Debug.Log(Players.otherPlayers[index].player);
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

            UpdateTurn(22);
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

    private void UpdateTurn(int turn)
    {
        GameController.Turn = turn;
        var player = Players.otherPlayers.First(_player => _player.player == turn);
        var index = Array.IndexOf(Players.otherPlayers, player);
        var actualIndex = index > (int) ThisPlayerPlayerColor ? index - 1 :
            index == (int) ThisPlayerPlayerColor ? 3 : index;
        for (var i = 0; i < _turnHandlers.Length; i++)
        {
            _turnHandlers[i].UpdateTurn(i != actualIndex
                ? new Color(108f / 255f, 93f / 255f, 68f / 255f)
                : ((PlayerColors) index).GetColor());
        }
    }

    /**
     * These are functions that will be called by js.
     */
    /**
     * if it's your first init turn, play it.
     */
    public void Init1(string[] args)
    {
        var turn = int.Parse(args[0]);
        UpdateTurn(turn);
    }

    /**
     * if it's your second init turn, play it.
     */
    public void Init2(string[] args)
    {
        var turn = int.Parse(args[0]);
    }

    /**
     * Update UI based on other players first init.
     */
    public void PlayedInit1(string[] args)
    {
        var turn = int.Parse(args[0]);
        var vertex = int.Parse(args[1]);
        var road1 = int.Parse(args[2]);
        var road2 = int.Parse(args[3]);
    }

    /**
     * Update UI based on other players second init.
     */
    public void PlayedInit2(string[] args)
    {
        var turn = int.Parse(args[0]);
        var vertex = int.Parse(args[1]);
        var road1 = int.Parse(args[2]);
        var road2 = int.Parse(args[3]);
    }

    /**
     * A player played it's Year of Plenty card. Update UI accordingly.
     */
    public void PlayYearOfPlenty(string[] args)
    {
        var turn = int.Parse(args[0]);
        var resource1 = args[1];
        var resource2 = args[2];
    }

    /**
     * A player played it's Road building card. Update UI accordingly.
     */
    public void PlayRoadBuilding(string[] args)
    {
        var turn = int.Parse(args[0]);
        var r1v1 = int.Parse(args[1]);
        var r1v2 = int.Parse(args[2]);
        var r2v1 = int.Parse(args[3]);
        var r2v2 = int.Parse(args[4]);
    }

    /**
     * A player played it's Monopoly card. Update UI accordingly.
     */
    public void PlayMonopoly(string[] args)
    {
        var turn = int.Parse(args[0]);
        var resource = args[1];
    }

    /**
     * A player played it's Knight card. Update UI accordingly.
     */
    public void PlayKnightCard(string[] args)
    {
        var turn = int.Parse(args[0]);
        var tile = int.Parse(args[1]);
    }

    /**
     * It's time to roll the dice. Post to server if it's your turn.
     */
    public void Dice(string[] args)
    {
        var turn = int.Parse(args[0]);
    }

    /**
     * A player rolled the dice. If it's you, update your personal.
     */
    public void PlayedDice(string[] args)
    {
        var turn = int.Parse(args[0]);
    }

    /**
     * It's about moving thief tile. Move it if it's your turn. 
     */
    public void ThiefTile(string[] args)
    {
        var turn = int.Parse(args[0]);
    }

    /**
     * Trade, buy or build time. Do it if it's your turn.
     */
    public void TradeBuyBuild(string[] args)
    {
        var turn = int.Parse(args[0]);
    }

    /**
     * Player built a home. Update UI accordingly.
     */
    public void BuildHome(string[] args)
    {
        var turn = int.Parse(args[0]);
        var vertex = int.Parse(args[1]);
    }

    /**
     * Player built a road. Update UI accordingly.
     */
    public void BuildRoad(string[] args)
    {
        var turn = int.Parse(args[0]);
        var vertex1 = int.Parse(args[1]);
        var vertex2 = int.Parse(args[2]);
    }

    /**
     * Player built a city. Update UI accordingly.
     */
    public void BuildCity(string[] args)
    {
        var turn = int.Parse(args[0]);
        var vertex = int.Parse(args[1]);
    }

    /**
     * Player bought a development card. Notify other players.
     */
    public void BoughtDevelopmentCard(string[] args)
    {
        var turn = int.Parse(args[0]);
    }

    /**
     * Someone has offered a tradition. If you want and can, accept it.
     */
    public void TradeOffer(string[] args)
    {
        var turn = int.Parse(args[0]);
        var trade = args[1];
        var tradeRequest = JsonUtility.FromJson<TradeRequest>(trade);
    }

    /**
     * Someone has answered to a trade. If it was to you, update UI.
     */
    public void AnsweredTrade(string[] args)
    {
        var player = int.Parse(args[0]);
        var answer = bool.Parse(args[1]);
        var tradeId = int.Parse(args[2]);
    }

    /**
     * A player chose an accept to a trade request. Update UI.
     */
    public void AcceptedTrade(string[] args)
    {
        var turn = int.Parse(args[0]);
        var player = int.Parse(args[0]);
    }

    /**
     * A player traded with bank. Update UI accordingly.
     */
    public void TradeBank(string[] args)
    {
        var turn = int.Parse(args[0]);
        var give = args[1];
        var want = args[2];
    }

    /**
     * Game finished.
     */
    public void Finish(string[] args)
    {
        var winner = int.Parse(args[0]);
    }
}