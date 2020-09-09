using Model;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Network = Utils.Network;

public class UpdateMyPlayer : MonoBehaviour
{
    [SerializeField] private RTLTextMeshPro longestArmy;
    [SerializeField] private RTLTextMeshPro playedSoldiers;
    [SerializeField] private RTLTextMeshPro soldiers;
    [SerializeField] private RTLTextMeshPro longestRoad;
    [SerializeField] private RTLTextMeshPro yearOfPlenty;
    [SerializeField] private RTLTextMeshPro monopoly;
    [SerializeField] private RTLTextMeshPro roadBuilding;
    [SerializeField] private RTLTextMeshPro wheat;
    [SerializeField] private RTLTextMeshPro wood;
    [SerializeField] private RTLTextMeshPro brick;
    [SerializeField] private RTLTextMeshPro stone;
    [SerializeField] private RTLTextMeshPro sheep;
    [SerializeField] private RTLTextMeshPro victoryPoint;
    [SerializeField] private RTLTextMeshPro totalPoint;
    [SerializeField] private RTLTextMeshPro username;
    [SerializeField] private Image profileImage;
    [SerializeField] private Image pColor;
    [SerializeField] private Button trade;
    [SerializeField] private Button pass;
    [SerializeField] private Button dice;
    public static Personal Personal;


    public void UpdatePlayer()
    {
        StartCoroutine(Network.GetRequest(URL.Personal, response =>
        {
            var personal = JsonUtility.FromJson<Personal>(response);
            Personal = personal;
            brick.text = personal.brick_count.ToString();
            longestArmy.text = personal.has_largest_army ? "2" : "0";
            playedSoldiers.text = personal.knight_card_played.ToString();
            soldiers.text = personal.knight.ToString();
            longestRoad.text = personal.has_long_road_card ? "2" : "0";
            yearOfPlenty.text = personal.year_of_plenty.ToString();
            monopoly.text = personal.monopoly_count.ToString();
            roadBuilding.text = personal.road_building_count.ToString();
            wheat.text = personal.wheat_count.ToString();
            wood.text = personal.wood_count.ToString();
            stone.text = personal.stone_count.ToString();
            sheep.text = personal.sheep_count.ToString();
            victoryPoint.text = personal.victory_point.ToString();
            totalPoint.text = $"امتیاز کل: {personal.point}";
            username.text = personal.player_username;
            MainScreen.ThisPlayerID = personal.player;
            StartCoroutine(Network.GetTexture(personal.player_avatar,
                texture => { profileImage.sprite = texture.ToSprite(); }, URL.Headers()));
        }, URL.Headers()));
    }

    public void SetEnableButtons(bool enable)
    {
        trade.interactable = enable;
        pass.interactable = enable;
        dice.interactable = enable;
    }

    public void UpdateColor(PlayerColors color)
    {
        pColor.color = color.GetColor();
        Personal.Color = color;
    }

    public void Pass()
    {
        StartCoroutine(Network.PostRequest(URL.Pass, string.Empty, s => { }, URL.Headers()));
    }

    public void ONDicePressed()
    {
        MainScreen.rollDice("2", "3");
    }
}