using UnityEngine;
using UnityEngine.UI;
using Utils;

public class TurnHandler : MonoBehaviour
{
    public void UpdateTurn(Color color)
    {
        gameObject.GetComponent<Image>().color = color;
    }
}
