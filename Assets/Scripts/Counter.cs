using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Counter : MonoBehaviour
{
    [SerializeField] int currentNumber = 0;

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

    void Start()
    {
    }
}