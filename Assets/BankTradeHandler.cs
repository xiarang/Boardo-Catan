using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BankTradeHandler : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown give;
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
}
