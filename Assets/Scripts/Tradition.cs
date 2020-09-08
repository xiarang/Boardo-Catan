using UnityEngine;

public class Tradition : MonoBehaviour
{
    public void OnClose()
    {
        gameObject.SetActive(false);
    }

    public void OnOpen()
    {
        gameObject.SetActive(true);
    }
}
