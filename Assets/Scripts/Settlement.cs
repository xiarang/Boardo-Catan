using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

public class Settlement : MonoBehaviour
{
    [SerializeField] private Sprite city;
    [SerializeField] private Sprite house;
    [SerializeField] public Road[] roads;
    public int builtID = -1;
    public bool isCity = false;

    private void OnMouseDown()
    {
        if (!GameController.ShouldSettlementClickable) return;
        if (!IsُSettlementPositionValid())
        {
            // todo Uncomment for build.
            // MainScreen.showDialog("خطا", "نمی توانید خانه یا شهر خود را اینجا بنا کنید");
            return;
        }
        //todo: set the owner road id to _buildID
        ChangeHolderColor();
        Debug.Log(gameObject.name);
        GameController.ShouldSettlementClickable = false;
        builtID = MainScreen.ThisPlayerID;
        MainScreen.SelectedSettlement = gameObject.name;
        if (GameController.Action == GameState.init1)
        {
            MainScreen.BoxMessage.text = "مکان جاده اول را مشخص کنید.";
            GameController.ShouldRoadClickable = true;
        }
    }

    private bool IsُSettlementPositionValid()
    {
        if (builtID != -1 && (builtID != MainScreen.ThisPlayerID || isCity)) return false;
        if (roads.Any(road => road.builtID == MainScreen.ThisPlayerID))
        {
            return true;
        }

        return GameController.Action == GameState.init1;
    }

    private void ChangeHolderColor()
    {
        var holder = GetComponent<SpriteRenderer>();
        holder.sprite = house;
        holder.color = MainScreen.ThisPlayerPlayerColor.GetColor();
    }
}