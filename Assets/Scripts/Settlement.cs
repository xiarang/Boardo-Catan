using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Settlement : MonoBehaviour
{
    [SerializeField] private Sprite city;
    [SerializeField] private Sprite house;
    [SerializeField] private Road[] roads;
    private int _builtID = -1;
    
    private bool _isCity = false;

    private void OnMouseDown()
    {
        if (_builtID != -1 && (_builtID != MainScreen.ThisPlayerID || _isCity)) return;
        //todo: set the owner road id to _buildID
        ChangeHolderColor();
        Debug.Log(gameObject.name);
        MainScreen.RoadBought = false;
    }

    private void ChangeHolderColor()
    {
        var holder = GetComponent<SpriteRenderer>();
        holder.sprite = house;
        holder.color = MainScreen.ThisPlayerColor;
    }
}