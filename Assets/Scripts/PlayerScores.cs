using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScores : MonoBehaviour
{
    [SerializeField] private int _palyerCard;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI cards;
    [SerializeField] private TextMeshProUGUI resources;
    [SerializeField] private TextMeshProUGUI road;
    [SerializeField] private TextMeshProUGUI point;
    [SerializeField] private Image profile;
    
    void Start()
    {
        
    }

    public void InitViews(Player _playersInfo)
    {
        name.text = _playersInfo.player_username;
        cards.text = _playersInfo.cards.ToString();
        resources.text = _playersInfo.resources.ToString();
        road.text = _playersInfo.road_length.ToString();
        point.text = _playersInfo.point.ToString();
    }

    // private void GetProfileImage()
    // {
    //     StartCoroutine(Network.GetRequest(_playersInfo[_palyerCard].player_avatar, setProfileImage));
    // }
    //
    // private void 
    
}
