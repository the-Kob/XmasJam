using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Store : MonoBehaviour
{
    int shoppingCartLevel, surfboardLevel, fuelCapacityLevel;
    [HideInInspector]
    public int pickupLevel, glueLevel, refuelLevel;

    Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();

        shoppingCartLevel = surfboardLevel = fuelCapacityLevel = pickupLevel = glueLevel = refuelLevel = 0;
    }

    void UpgradeShoopingCart()
    {
        if(CanUpgrade(shoppingCartLevel))
        {
            shoppingCartLevel++;

            player.movementSpeedCartMultiplier += 0.2f; // Increases 20% each time, linear
        }
    }

    void UpgradeSurfboard()
    {
        if (CanUpgrade(surfboardLevel))
        {
            surfboardLevel++;

            player.maxMovementSpeedSurfboard += 0.2f; // Increases 20% each time, linear
        }
    }

    void UpgradeFuelCapacity()
    {
        if (CanUpgrade(fuelCapacityLevel))
        {
            fuelCapacityLevel++;

            player.fuelMultiplier += 0.5f; // increases 50% each time, linear
        }
    }

    void UpgradePickup()
    {
        if (CanUpgrade(pickupLevel))
        {
            pickupLevel++;
        }
    }

    void UpgradeGlue()
    {
        if (CanUpgrade(glueLevel))
        {
            glueLevel++;
        }
    }

    void UpgradeRefuel()
    {
        if (CanUpgrade(refuelLevel))
        {
            refuelLevel++;
        }
    }

    bool CanUpgrade(int level)
    {
        int cost = ((level * (level + 1)) / 2) * 10; // Cost is equal to the factorial of the level

        return cost <= player.coins;
    }
}
