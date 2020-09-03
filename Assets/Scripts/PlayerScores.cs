using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScores : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI cards;
    [SerializeField] private TextMeshProUGUI resources;
    [SerializeField] private TextMeshProUGUI road;
    [SerializeField] private TextMeshProUGUI point;
    [SerializeField] private Image profile;
    
    void Start()
    {
        
    }

    public void InitViews(Player playersInfo)
    {
        name.text = playersInfo.player_username;
        cards.text = playersInfo.cards.ToString();
        resources.text = playersInfo.resources.ToString();
        road.text = playersInfo.road_length.ToString();
        point.text = playersInfo.point.ToString();
        GetProfileImage(playersInfo.player_avatar);
    }

    private void GetProfileImage(string url)
    {
        StartCoroutine(Network.GetTexture(url, SetProfileImage));
    }

    private void SetProfileImage(Texture response)
    {
        Debug.Log(response.ToString());
        profile.sprite = response.ToSprite();
    }
    
}
