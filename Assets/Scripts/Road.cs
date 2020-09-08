using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class Road : MonoBehaviour
{
    public int builtID = -1;
    [SerializeField] private Settlement s1, s2;

    private void OnMouseDown()
    {
        //todo: check road bought
        if (!GameController.ShouldRoadClickable) return;
        if (builtID != -1) return;
        if (!IsRoadPositionValid()) return;
        builtID = MainScreen.ThisPlayerID;
        ChangeRoadColor();
        Debug.Log(gameObject.name);
        GameController.ShouldRoadClickable = false;
    }

    private void ChangeRoadColor()
    {
        var road = GetComponent<SpriteRenderer>();
        road.color = MainScreen.ThisPlayerPlayerColor.GetColor();
    }

    private bool IsRoadPositionValid()
    {
        if (s1.builtID == MainScreen.ThisPlayerID || s2.builtID == MainScreen.ThisPlayerID) return true;
        if (s1.roads.Any(s1Road => s1Road.builtID == MainScreen.ThisPlayerID))
            return s1.builtID == MainScreen.ThisPlayerID || s1.builtID == -1;
        if (s2.roads.Any(s2Road => s2Road.builtID == MainScreen.ThisPlayerID))
            return s2.builtID == MainScreen.ThisPlayerID || s2.builtID == -1;
        return false;
    }
}