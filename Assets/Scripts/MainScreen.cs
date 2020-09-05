using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utils;
using Network = Utils.Network;

public class MainScreen : MonoBehaviour
{
    public static int ThiefResourceNumber;

    [SerializeField] private SpriteRenderer[] boardTiles;
    [SerializeField] private SpriteRenderer[] tileTags;
    [SerializeField] private Sprite[] resources;
    [SerializeField] private Sprite[] numbers;

    private Tiles _catanBoard;
    private static Players _players;
    private UpdateMyPlayer _myPlayer;
    private PlayerScores[] _playersScoreboard;
    private Canvas _canvas;
    private Scene _scene;
    private Road[] _boardRoads;
    private Settlement[] _boardSettlements;
    private readonly Color[] _playersColor = {Colors.Blue, Colors.Green, Colors.Orange, Colors.Red};

    public static int ThisPlayerID;
    public static Color ThisPlayerColor;
    public static bool RoadBought = false;
    public static bool CityBought = false;

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
            if (item.name == "roads")
            {
                _boardRoads = item.GetComponentsInChildren<Road>();
                Debug.Log(_boardRoads.Length + " h " + _boardSettlements.Length);
            }
            else if (item.name == "settelments")
                _boardSettlements = item.GetComponentsInChildren<Settlement>();
            
        }
    }

    private void GetPlayers()
    {
        StartCoroutine(Network.GetRequest(URL.GetPlayers(), response =>
        {
            response = "{\"otherPlayers\":" + response + "}";
            _players = JsonUtility.FromJson<Players>(response);
            var index = 0;
            foreach (var player in _playersScoreboard)
            {
                if (_players.otherPlayers[index].player == ThisPlayerID)
                {
                    ThisPlayerColor = _playersColor[index];
                    _myPlayer.UpdateColor(ThisPlayerColor);
                    index++;
                }

                _players.otherPlayers[index].Color = _playersColor[index];
                player.InitViews(_players.otherPlayers[index]);
                index++;
            }
        }, URL.Headers()));
    }

    private void GetBoardInfo()
    {
        StartCoroutine(Network.GetRequest(URL.GetBoard(), response =>
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
}