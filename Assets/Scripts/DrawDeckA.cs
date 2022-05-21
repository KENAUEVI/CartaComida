using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawDeckA : MonoBehaviour
{
    public GameObject Card_brocolli;
    public GameObject Card_daifuku;
    public GameObject Player_Area;

    public void OnClick() 
    {
        for (int i = 0; i < 5; i++) {
            GameObject card = Instantiate(Card_brocolli, new Vector2(0,0), Quaternion.identity);
            card.transform.SetParent(Player_Area.transform, false); // localização inicial da carta

        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
