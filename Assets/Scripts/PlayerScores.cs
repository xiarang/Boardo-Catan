using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Network = Utils.Network;

public class PlayerScores : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI cards;
    [SerializeField] private TextMeshProUGUI resources;
    [SerializeField] private TextMeshProUGUI road;
    [SerializeField] private TextMeshProUGUI point;
    [SerializeField] private Image profile;

    public void InitViews(Player playersInfo)
    {
        playerName.text = playersInfo.player_username;
        cards.text = playersInfo.cards.ToString();
        resources.text = playersInfo.resources.ToString();
        road.text = playersInfo.road_length.ToString();
        point.text = playersInfo.point.ToString();
        GetProfileImage(playersInfo.player_avatar);
    }

    private void GetProfileImage(string url)
    {
        StartCoroutine(Network.GetTexture(url, SetProfileImage, URL.Headers()));
    }

    private void SetProfileImage(Texture response)
    {
        profile.sprite = response.ToSprite();
    }
}