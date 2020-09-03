using TMPro;
using UnityEngine;

namespace Utils
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private int currentNumber;

        public void OnPlusPressed()
        {
            Debug.Log(currentNumber.ToString());
            currentNumber++;
            SetText();
        }

        public void OnMinusPressed()
        {
            if (currentNumber == 0) return;
            Debug.Log(currentNumber.ToString());
            currentNumber--;
            SetText();
        }

        public void SetText()
        {
            var textComponent = GetComponent<TextMeshProUGUI>();
            textComponent.text = (currentNumber).ToString();
        }
    }
}