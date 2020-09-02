using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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


    // string[] res = { "field", "forest", "hill", "mountain", "pasture" };

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
        GetBoardInfo();
        SetPlayerColor("green");
    }

    private void GetBoardInfo()
    {
        (string, int) t1;
        StartCoroutine(GetRequest(URL.BaseURL + URL.GetBoard + "9b717be4-a042-4b94-837f-b673f13d3241"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            //todo: get token from browser
            webRequest.SetRequestHeader("Authorization", "Token 58998a8632efec6b3810f7a2833dc300fe2a937f");
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                string response = "{\"board\":" + webRequest.downloadHandler.text + "}";
                _catanBoard = JsonUtility.FromJson<Tiles>(response);
                InitBoard();
            }
        }
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

    // Update is called once per frame
    void Update()
    {
    }
}