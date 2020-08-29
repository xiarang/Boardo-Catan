using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] boardTiles;
    [SerializeField] Sprite[] resources;
    string[] res = { "field", "forest", "hill", "mountain", "pasture" };
    // Start is called before the first frame update
    void Start()
    {
        initTiles();
    }

    private void initTiles() { 
    
        List<string> tilesRes = new List<string>() { "desert" };
        for (int i = 1; i < 19; i++) { 
            tilesRes.Add(res[Random.Range(0, 5)]);
            var r = Random.Range(0, 5);
            boardTiles[i].sprite = resources[r];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
