using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    bool isGlue;

    public float baseDuration;
    float duration;

    public float baseMaxMovementSpeedThreshold;
    float maxMovementSpeedThreshold;

    public float baseFuelRestorePercentage;
    float fuelRestorePercentage;

    Store store;

    void Awake()
    {
        store = FindObjectOfType<Store>();

        if (isGlue)
        {
            duration = baseDuration + baseDuration * (store.glueLevel * 0.167f); // Each level increases the glue duration by around 0.5 second
            maxMovementSpeedThreshold = baseMaxMovementSpeedThreshold + (store.glueLevel * 0.1f);
        } else
        {
            fuelRestorePercentage = baseFuelRestorePercentage + (store.refuelLevel * 0.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            Player player = collision.gameObject.GetComponent<Player>();

            if (isGlue)
            {
                float movementSpeedPlus;

                if(player.isOnCart)
                {
                    movementSpeedPlus = player.maxMovementSpeedCart * maxMovementSpeedThreshold;
                } else
                {
                    movementSpeedPlus = player.maxMovementSpeedSurfboard * maxMovementSpeedThreshold;
                }

                player.SniffGlue(movementSpeedPlus, duration);
            }
            else
            {
                float fuelPlus = player.maxFuel * fuelRestorePercentage;

                player.RefillFuel(fuelPlus);
            }
        }
    }
}
