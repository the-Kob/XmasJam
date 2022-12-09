using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    int value;

    void Awake()
    {
        #region Set value with store level
        int storePickupLevel = 0; // TODO Needs to be retrieved from the store, probably a global variable
        
        switch(storePickupLevel)
        {
            case 0:
                value = 1;
                break;
            case 1:
                value = 2;
                break;
            case 2:
                value = 5;
                break;
            case 3:
                value = 10;
                break;
            case 4:
                value = 25;
                break;
            case 5:
                value = 50;
                break;
        }
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            Player player = collision.gameObject.GetComponent<Player>();
            player.AddCoins(value);
        }
    }
}
