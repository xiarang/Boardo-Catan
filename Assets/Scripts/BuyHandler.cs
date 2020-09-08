using System;
using Model;
using UnityEngine;
using Utils;
using Network = Utils.Network;


public class BuyHandler : MonoBehaviour
{
    public void OnBuyStarted(string sellable)
    {
        switch (sellable)
        {
            case nameof(Sellable.Road):
                BuyRoad();
                break;
            case nameof(Sellable.City):
                BuyCity();
                break;
            case nameof(Sellable.DevelopmentCard):
                BuyDevelopmentCard();
                break;
            case nameof(Sellable.House):
                BuyHouse();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sellable), sellable, null);
        }
    }

    private void BuyRoad()
    {
        var personal = UpdateMyPlayer.Personal;
        if (personal.brick_count == 0 || personal.wood_count == 0)
        {
            return;
        }
        StartCoroutine(Network.PostRequest(URL.CreateRoad, string.Empty, s => { }, URL.Headers()));
    }

    private void BuyHouse()
    {
        var personal = UpdateMyPlayer.Personal;
        if (personal.brick_count == 0 || personal.wood_count == 0 || personal.sheep_count == 0 ||
            personal.wheat_count == 0)
        {
            return;
        }

        StartCoroutine(Network.PostRequest(URL.CreateHome, string.Empty, s => { }, URL.Headers()));
    }

    private void BuyDevelopmentCard()
    {
        var personal = UpdateMyPlayer.Personal;
        if (personal.sheep_count == 0 || personal.wheat_count == 0 || personal.stone_count == 0)
        {
            return;
        }

        StartCoroutine(Network.PostRequest(URL.BuyDevelopmentCard, string.Empty, s =>
        {
            string message;
            string title;
            switch (s)
            {
                case "victory":
                    message = "تبریک! شما یک قدم به پیروزی نزدیکتر شدید.";
                    title = "کارت پیروزی";
                    break;
                case "monopoly":
                    message = "یک کارت مونوپولی برای شما.";
                    title = "کارت مونوپولی";
                    break;
                case "year_of_plenty":
                    message = "یک کارت سال فراوانی به کارت های شما اضافه شد.";
                    title = "کارت سال فراوانی";
                    break;
                case "road_building":
                    message = "شما در قرعه کشی بانک برنده کمک هزینه ساخت دو جاده شده اید.";
                    title = "کارت ساخت جاده";
                    break;
                case "knight":
                    message = "امنیت قلمرو خود را بیش از پیش تضمین کنید.";
                    title = "کارت شوالیه";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // var result = DisplayDialog(title, message, "باشه."); 
        }, URL.Headers()));
    }

    private void BuyCity()
    {
        var personal = UpdateMyPlayer.Personal;
        if (personal.stone_count < 3 || personal.wheat_count < 2)
        {
            return;
        }
        // TODO not complete
        // StartCoroutine(Network);
    }
}