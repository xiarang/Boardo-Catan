using System.Linq;
using UnityEngine;
using Utils;
using Network = Utils.Network;

public class Road : MonoBehaviour
{
    public int builtID = -1;
    [SerializeField] public Settlement s1, s2;

    private void OnMouseDown()
    {
        //todo: check road bought
        if (!GameController.ShouldRoadClickable) return;
        if(GameController.Turn != MainScreen.ThisPlayerID) return;
        if (!IsRoadPositionValid())
        {
            // todo Uncomment for build.
            MainScreen.showDialog("خطا", "نمی توانید جاده خود را اینجا بسازید");
            return;
        }

        builtID = MainScreen.ThisPlayerID;
        ChangeRoadColor(MainScreen.ThisPlayerPlayerColor.GetColor());
        Debug.Log(gameObject.name);
        GameController.ShouldRoadClickable = false;
        if (GameController.Action == GameState.init1 || GameController.Action == GameState.init2)
        {
            var body = "{\"vertex\": " + MainScreen.SelectedSettlement + ", \"road_v1\": " + s1.name +
                       ", \"road_v2\": " + s2.name + "}";
            StartCoroutine(Network.PostRequest(GameController.Action == GameState.init1 ? URL.Init1 : URL.Init2, body,
                s => { }, URL.Headers(), true));
        }
    }

    public void ChangeRoadColor(Color color)
    {
        var road = GetComponent<SpriteRenderer>();
        road.color = color;
    }

    private bool IsRoadPositionValid()
    {
        if (builtID != -1) return false;
        if (s1.builtID == MainScreen.ThisPlayerID || s2.builtID == MainScreen.ThisPlayerID) return true;
        if (s1.roads.Any(s1Road => s1Road.builtID == MainScreen.ThisPlayerID))
            return s1.builtID == MainScreen.ThisPlayerID || s1.builtID == -1;
        if (s2.roads.Any(s2Road => s2Road.builtID == MainScreen.ThisPlayerID))
            return s2.builtID == MainScreen.ThisPlayerID || s2.builtID == -1;
        return false;
    }
}