using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] boardTiles;
    [SerializeField] SpriteRenderer[] tilTags;
    [SerializeField] Sprite[] resources;
    [SerializeField] Sprite[] numbers;

    public static Color PlayerColor;
    public static int ThiefResourceNumber;
    // string[] res = { "field", "forest", "hill", "mountain", "pasture" };

    void Start()
    {
        InitTiles();
        InitTileTags();
        SetPlayerColor("green");
    }

    private void InitTileTags()
    {
        for (var i = 0; i < 19; i++)
        {
            tilTags[i].sprite = numbers[i % 11];
        }
    }

    private void InitTiles()
    {
        for (var i = 1; i < 19; i++)
        {
            var r = UnityEngine.Random.Range(0, 5);
            boardTiles[i].sprite = resources[r];
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

    // Update is called once per frame
    void Update()
    {
    }
}