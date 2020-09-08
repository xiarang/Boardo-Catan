using System.Collections.Generic;
using Model;
using RTLTMPro;
using UnityEngine;
using Utils;
using Network = Utils.Network;

public class TradeHandler : MonoBehaviour
{
    private RTLTextMeshPro _willingWheat;
    private RTLTextMeshPro _willingSheep;
    private RTLTextMeshPro _willingBrick;
    private RTLTextMeshPro _willingWood;
    private RTLTextMeshPro _willingStone;
    private RTLTextMeshPro _offeringWheat;
    private RTLTextMeshPro _offeringSheep;
    private RTLTextMeshPro _offeringBrick;
    private RTLTextMeshPro _offeringWood;
    private RTLTextMeshPro _offeringStone;

    private void Start()
    {
        _willingBrick = GameObject.Find("willingBrick").GetComponent<RTLTextMeshPro>();
        _willingWheat = GameObject.Find("willingWheat").GetComponent<RTLTextMeshPro>();
        _willingWood = GameObject.Find("willingWood").GetComponent<RTLTextMeshPro>();
        _willingSheep = GameObject.Find("willingSheep").GetComponent<RTLTextMeshPro>();
        _willingStone = GameObject.Find("willingStone").GetComponent<RTLTextMeshPro>();
        _offeringBrick = GameObject.Find("offeringBrick").GetComponent<RTLTextMeshPro>();
        _offeringWheat = GameObject.Find("offeringWheat").GetComponent<RTLTextMeshPro>();
        _offeringWood = GameObject.Find("offeringWood").GetComponent<RTLTextMeshPro>();
        _offeringSheep = GameObject.Find("offeringSheep").GetComponent<RTLTextMeshPro>();
        _offeringStone = GameObject.Find("offeringStone").GetComponent<RTLTextMeshPro>();
    }

    public void OnBuyStarted()
    {
        if (_willingBrick.text == "0" &&
            _willingWheat.text == "0" &&
            _willingWood.text == "0" &&
            _willingSheep.text == "0" &&
            _willingStone.text == "0" ||
            _offeringBrick.text == "0" &&
            _offeringWheat.text == "0" &&
            _offeringWood.text == "0" &&
            _offeringSheep.text == "0" &&
            _offeringStone.text == "0")
        {
            return;
        }

        var data = new TradeRequest
        {
            give = new Trade
            {
                brick = int.Parse(_offeringBrick.text),
                sheep = int.Parse(_offeringSheep.text),
                stone = int.Parse(_offeringStone.text),
                wheat = int.Parse(_offeringWheat.text),
                wood = int.Parse(_offeringWood.text)
            },
            want = new Trade
            {
                brick = int.Parse(_willingBrick.text),
                sheep = int.Parse(_willingSheep.text),
                stone = int.Parse(_willingStone.text),
                wheat = int.Parse(_willingWheat.text),
                wood = int.Parse(_willingWood.text)
            }
        };
        StartCoroutine(Network.PostRequest(URL.Trade, JsonUtility.ToJson(data), s => { }, URL.Headers(), true));
    }
}