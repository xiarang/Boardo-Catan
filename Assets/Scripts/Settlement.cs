using UnityEngine;

public class Settlement : MonoBehaviour
{
    [SerializeField] private Sprite city;
    [SerializeField] private Sprite house;

    private void OnMouseDown()
    {
        ChangeHolderColor();
        Debug.Log(gameObject.name);
    }

    private void ChangeHolderColor()
    {
        var holder = GetComponent<SpriteRenderer>();
        holder.sprite = house;
        holder.color = MainScreen.PlayerColor;
    }
}