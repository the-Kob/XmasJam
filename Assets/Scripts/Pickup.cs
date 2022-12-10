using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [HideInInspector]
    public int value;

    Store store;

    void Awake()
    {
        store = FindObjectOfType<Store>();

        value = 2 * store.pickupLevel;
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
