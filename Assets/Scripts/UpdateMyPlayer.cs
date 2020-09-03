using System.Collections.Generic;
using Model;
using RTLTMPro;
using UnityEngine;
using Utils;
using Network = Utils.Network;

public class UpdateMyPlayer : MonoBehaviour
{
    [SerializeField] private RTLTextMeshPro longestArmy;
    [SerializeField] private RTLTextMeshPro armySize;
    [SerializeField] private RTLTextMeshPro longestRoad;
    [SerializeField] private RTLTextMeshPro YearOfPlenty;
    [SerializeField] private RTLTextMeshPro Monopoly;
    [SerializeField] private RTLTextMeshPro RoadBuilding;
    [SerializeField] private RTLTextMeshPro Wheat;
    [SerializeField] private RTLTextMeshPro Wood;
    [SerializeField] private RTLTextMeshPro Brick;
    [SerializeField] private RTLTextMeshPro Stone;
    [SerializeField] private RTLTextMeshPro Sheep;

    public void UpdatePlayer()
    {
        // ReSharper disable once IteratorMethodResultIsIgnored
        Network.GetRequest(URL.Personal(), response =>
        {
            var personal = JsonUtility.FromJson<Personal>(response);
            Brick.text = personal.brick_count.ToString();
            longestArmy.text = personal.has_largest_army ? "2" : "0";
            armySize.text = personal.knight.ToString();
            longestRoad.text = personal.has_long_road_card ? "2" : "0";
            YearOfPlenty.text = personal.year_of_plenty.ToString();
            Monopoly.text = personal.monopoly_count.ToString();
            RoadBuilding.text = personal.road_building_count.ToString();
            Wheat.text = personal.wheat_count.ToString();
            Wood.text = personal.wood_count.ToString();
            Stone.text = personal.stone_count.ToString();
            Sheep.text = personal.sheep_count.ToString();
        }, new Dictionary<string, string>());
    }

    public void Pass()
    {
        
    }

    public void StartBuy()
    {
        
    }
}