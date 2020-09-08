using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Network = Utils.Network;

public class TradeProfileHandler : MonoBehaviour
{
    [SerializeField] private Image avatar;
    [SerializeField] private RTLTextMeshPro username;
    [SerializeField] private int index;

    private void Start()
    {
        int playerIndex;

        for (playerIndex = 0; playerIndex < MainScreen.Players.otherPlayers.Length; playerIndex++)
        {
            if (MainScreen.Players.otherPlayers[playerIndex].player == MainScreen.ThisPlayerID)
            {
                break;
            }
        }

        int myId;
        if (index < playerIndex)
        {
            myId = index;
        }
        else
        {
            myId = index + 1;
        }

        username.text = MainScreen.Players.otherPlayers[myId].player_username;
        StartCoroutine(Network.GetTexture(MainScreen.Players.otherPlayers[myId].player_avatar,
            texture => avatar.sprite = texture.ToSprite(), URL.Headers()));
        foreach (var o in GameObject.FindGameObjectsWithTag("Loading"))
        {
            o.SetActive(false);
        }
    }
}