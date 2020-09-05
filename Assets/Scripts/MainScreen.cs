using Model;
using UnityEngine;
using UnityEngine.UI;
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
    private UpdateMyPlayer _myPlayer;
    

    //todo: remove id after get personal
    public static int ID;
    public static string Username;
    public static string PersonalProfileResource;
    private PlayerScores[] _playersScoreboard;
    private Canvas _canvas;

    private void InitBoard()
    {
        for (var i = 0; i < 19; i++)
        {
            var tile = _catanBoard.board[i];
            tilTags[i].sprite = numbers[tile.number - 2];
            boardTiles[i].sprite = resources[GetResourceID(tile.resource)];
        }
    }

    private void Start()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _playersScoreboard = _canvas.GetComponentsInChildren<PlayerScores>();
        _myPlayer = _canvas.GetComponentInChildren<UpdateMyPlayer>();
        foreach (var o in GameObject.FindGameObjectsWithTag("Loading"))
        {
            o.SetActive(false);
        }
        URL.SetToken("58998a8632efec6b3810f7a2833dc300fe2a937f");
        URL.SetRoomName("9b717be4-a042-4b94-837f-b673f13d3241");
        _myPlayer.UpdatePlayer();
        GetBoardInfo();
        GetPlayers();
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
                if (_players.otherPlayers[index].player == ID)
                {
                    var myPlayer = _players.otherPlayers[index];
                    PersonalProfileResource = myPlayer.player_avatar;
                    Username = myPlayer.player_username;
                    index++;
                }

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