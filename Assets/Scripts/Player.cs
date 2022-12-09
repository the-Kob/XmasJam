using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Vector2 spawnPos;

    float movementSpeed;
    public float maxMovementSpeed;
    [HideInInspector]
    public float initialMaxMovementSpeed; // used to reset the value

    public float acceleration;
    [HideInInspector]
    public float initialAcceleration; // used to reset the value
    public float glueAccelerationMultipliyer;

    public float maxFuel;
    [HideInInspector]
    public float currentFuel;

    [HideInInspector]
    public int coins;

    [HideInInspector]
    public bool underGlueEffect;

    [HideInInspector]
    public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        initialMaxMovementSpeed = maxMovementSpeed;
        initialAcceleration = acceleration;

        coins = 0; // Coins are only set to 0 at the start of the game

        Reset();
    }

    public void Move()
    {
        float speedDifference = maxMovementSpeed - rb.velocity.x;

        float movement = speedDifference * acceleration;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

        if (!underGlueEffect)
        {
            currentFuel -= Time.deltaTime;
        }
    }

    public bool HasFuel()
    {
        return currentFuel > 0;
    }

    public void AddCoins(int value)
    {
        coins += value;
    }

    public void SniffGlue(float movementSpeedPlus, float duration)
    {
        underGlueEffect = true;

        maxMovementSpeed += movementSpeedPlus;
        acceleration *= glueAccelerationMultipliyer;

        Invoke(nameof(ResetGlue), duration);
    }

    void ResetGlue()
    {
        underGlueEffect = false;

        maxMovementSpeed = initialMaxMovementSpeed;
        acceleration = initialAcceleration;
    }

    public void RefillFuel(float fuelPlus)
    {
        currentFuel += fuelPlus;

        if(currentFuel > maxFuel)
        {
            currentFuel = maxFuel;
        }
    }

    public void Reset()
    {
        currentFuel = maxFuel;

        underGlueEffect = false;

        transform.position = spawnPos;
    }
}
