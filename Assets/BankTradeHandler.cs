using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;
using Network = Utils.Network;

public class BankTradeHandler : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown give, want;
    [SerializeField] private TMP_Dropdown.OptionData brick, wheat, sheep, stone, wood, desert;
    private void Start()
    {
        var personal = UpdateMyPlayer.Personal;
        var options = new List<TMP_Dropdown.OptionData>();
        if (personal.wood_count >= 4)
        {
            options.Add(wood);
        }

        if (personal.wheat_count >= 4)
        {
            options.Add(wheat);
        }

        if (personal.brick_count >= 4)
        {
            options.Add(brick);
        }

        if (personal.stone_count >= 4)
        {
            options.Add(stone);
        }

        if (personal.sheep_count >= 4)
        {
            options.Add(sheep);
        }

        if (options.Count == 0)
        {
            options.Add(desert);
        }

        give.ClearOptions();
        give.AddOptions(options);
    }

    public void OnBankTradeStarted()
    {
        if (give.options.Contains(desert))
        {
            return;
        }

        string request;
        if (give.options[give.value].text == "آجر")
        {
            request = "brick";
        }
        else if (give.options[give.value].text == "چوب")
        {
            request = "wood";
        }
        else if (give.options[give.value].text == "گوسفند")
        {
            request = "sheep";
        }
        else if (give.options[give.value].text == "سنگ")
        {
            request = "stone";
        }
        else
        {
            request = "wheat";
        }
        string will;
        if (want.options[want.value].text == "آجر")
        {
            will = "brick";
        }
        else if (want.options[want.value].text == "چوب")
        {
            will = "wood";
        }
        else if (want.options[want.value].text == "گوسفند")
        {
            will = "sheep";
        }
        else if (want.options[want.value].text == "سنگ")
        {
            will = "stone";
        }
        else
        {
            will = "wheat";
        }

        StartCoroutine(Network.PostRequest(URL.TradeBank, "{\"give\": \"" + request + "\", \"want\": \"" + will + "\"}", s => { },
            URL.Headers(), true));
    }
}
