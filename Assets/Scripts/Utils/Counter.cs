using System;
using Model;
using TMPro;
using UnityEngine;

namespace Utils
{
    public class Counter : MonoBehaviour
    {

        private int _currentNumber;
        [SerializeField] private Resource resources;

        private void OnIncrease()
        {
            _currentNumber++;
            SetText();
        }

        public void OnIncreaseOffer()
        {
            switch (resources)
            {
                case Resource.Brick:
                    if (_currentNumber == UpdateMyPlayer.Personal.brick_count)
                    {
                        return;
                    }
                    break;
                case Resource.Sheep:
                    if (_currentNumber == UpdateMyPlayer.Personal.sheep_count)
                    {
                        return;
                    }
                    break;
                case Resource.Stone:
                    if (_currentNumber == UpdateMyPlayer.Personal.stone_count)
                    {
                        return;
                    }
                    break;
                case Resource.Wheat:
                    if (_currentNumber == UpdateMyPlayer.Personal.wheat_count)
                    {
                        return;
                    }
                    break;
                case Resource.Wood:
                    if (_currentNumber == UpdateMyPlayer.Personal.wood_count)
                    {
                        return;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                break;
            }
            OnIncrease();
        }

        public void OnDecrease()
        {
            if (_currentNumber == 0) return;
            _currentNumber--;
            SetText();
        }

        public void OnIncreaseWilling()
        {
            OnIncrease();
        }

        private void SetText()
        {
            var textComponent = GetComponent<TextMeshProUGUI>();
            textComponent.text = _currentNumber.ToString();
        }

        public void OnReset()
        {
            _currentNumber = 0;
            SetText();
        }
    }
}