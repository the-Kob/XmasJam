using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class Store : MonoBehaviour
{
    int shoppingCartLevel, surfboardLevel, fuelCapacityLevel;
    [HideInInspector]
    public int pickupLevel, glueLevel, refuelLevel;

    public TextMeshProUGUI
        shoppingCartLevelText, shoppingCartCostText,
        surfboardLevelText, surfboardCostText,
        fuelCapacityLevelText, fuelCapacityCostText,
        pickupLevelText, pickupCostText,
        glueLevelText, glueCostText,
        refuelLevelText, refuelCostText;

    Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();

        shoppingCartLevel = surfboardLevel = fuelCapacityLevel = pickupLevel = glueLevel = refuelLevel = 0;
    }

    private void Update()
    {
        shoppingCartLevelText.text = "Lvl. " + shoppingCartLevel;
        shoppingCartCostText.text = "" + CalculateCost(shoppingCartLevel);

        surfboardLevelText.text = "Lvl. " + surfboardLevel;
        surfboardCostText.text = "" + CalculateCost(surfboardLevel);

        fuelCapacityLevelText.text = "Lvl. " + fuelCapacityLevel;
        fuelCapacityCostText.text = "" + CalculateCost(fuelCapacityLevel);

        pickupLevelText.text = "Lvl. " + pickupLevel;
        pickupCostText.text = "" + CalculateCost(pickupLevel);

        glueLevelText.text = "Lvl. " + glueLevel;
        glueCostText.text = "" + CalculateCost(glueLevel);

        refuelLevelText.text = "Lvl. " + refuelLevel;
        refuelCostText.text = "" + CalculateCost(refuelLevel);
    }

    public void UpgradeShoopingCart()
    {
        if (CanUpgrade(shoppingCartLevel))
        {
            shoppingCartLevel++;

            player.movementSpeedCartMultiplier += 0.2f; // Increases 20% each time, linear
            player.coins -= CalculateCost(shoppingCartLevel);
        }
        else
        {
            // Insert not enough food popup
        }
    }

    public void UpgradeSurfboard()
    {
        if (CanUpgrade(surfboardLevel))
        {
            surfboardLevel++;

            player.maxMovementSpeedSurfboard += 0.2f; // Increases 20% each time, linear
            player.coins -= CalculateCost(surfboardLevel);
        }
        else
        {
            // Insert not enough food popup
        }
    }

    public void UpgradeFuelCapacity()
    {
        if (CanUpgrade(fuelCapacityLevel))
        {
            fuelCapacityLevel++;

            player.fuelMultiplier += 0.5f; // increases 50% each time, linear
            player.coins -= CalculateCost(fuelCapacityLevel);
        }
        else
        {
            // Insert not enough food popup
        }
    }

    public void UpgradePickup()
    {
        if (CanUpgrade(pickupLevel))
        {
            pickupLevel++;
            player.coins -= CalculateCost(pickupLevel);
        }
        else
        {
            // Insert not enough food popup
        }
    }

    public void UpgradeGlue()
    {
        if (CanUpgrade(glueLevel))
        {
            glueLevel++;
            player.coins -= CalculateCost(glueLevel);
        }
        else
        {
            // Insert not enough food popup
        }
    }

    public void UpgradeRefuel()
    {
        if (CanUpgrade(refuelLevel))
        {
            refuelLevel++;
            player.coins -= CalculateCost(refuelLevel);
        }
        else
        {
            // Insert not enough food popup
        }
    }

    bool CanUpgrade(int level)
    {
        int cost = CalculateCost(level);

        if (level == 0)
        {
            return 5 <= player.coins;
        }

        return cost <= player.coins;
    }

    int CalculateCost(int level)
    {
        return level == 0 ? 5 : ((level * (level + 1)) / 2) * 15; // Cost is equal to the factorial of the level
    }
}
