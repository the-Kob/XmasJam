using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    bool isGlue;

    float duration;

    float maxMovementSpeedThreshold; // Ranged from 0 to 1, works as a percentage

    float fuelRestorePercentage; // Ranged from 0 to 1, works as a percentage

    void Awake()
    {
        if (isGlue)
        {
            #region Set duration with store level
            int storeGlueDurationLevel = 0; // TODO Needs to be retrieved from the store, probably a global variable

            switch (storeGlueDurationLevel)
            {
                case 0: // Base value
                    duration = 3f;
                    break;
                case 1:
                    duration = 4.75f;
                    break;
                case 2:
                    duration = 6.5f;
                    break;
                case 3:
                    duration = 8.25f;
                    break;
                case 4:
                    duration = 10f;
                    break;
            }
            #endregion

            #region Set movement speed threshold with store level
            int storeGlueMovementSpeedThresholdLevel = 0; // TODO Needs to be retrieved from the store, probably a global variable

            switch (storeGlueMovementSpeedThresholdLevel)
            {
                case 0: // Base value
                    maxMovementSpeedThreshold = 0.1f;
                    break;
                case 1:
                    maxMovementSpeedThreshold = 0.25f;
                    break;
                case 2:
                    maxMovementSpeedThreshold = 0.5f;
                    break;
                case 3:
                    maxMovementSpeedThreshold = 0.75f;
                    break;
                case 4:
                    maxMovementSpeedThreshold = 1f;
                    break;
            }
            #endregion
        } else
        {
            #region Set percentage of fuel restore with store level
            int storeFuelRestorePercentageLevel = 0; // TODO Needs to be retrieved from the store, probably a global variable

            switch (storeFuelRestorePercentageLevel)
            {
                case 0: // Base value
                    fuelRestorePercentage = 0.1f;
                    break;
                case 1:
                    fuelRestorePercentage = 0.25f;
                    break;
                case 2:
                    fuelRestorePercentage = 0.5f;
                    break;
                case 3:
                    fuelRestorePercentage = 0.75f;
                    break;
                case 4:
                    fuelRestorePercentage = 1f;
                    break;
            }
            #endregion
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
                float movementSpeedPlus = player.maxMovementSpeed * maxMovementSpeedThreshold;

                player.SniffGlue(movementSpeedPlus, duration);
            }
            else
            {
                float fuelPlus = player.currentFuel * fuelRestorePercentage;

                player.RefillFuel(fuelPlus);
            }
        }
    }
}
