using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Vector2 spawnPos;

    public float maxMovementSpeed;
    [HideInInspector]
    public float initialMaxMovementSpeed; // used to reset the value

    public float accelaration;
    public float glueAccelarationMultipliyer;

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

        coins = 0; // Coins are only set to 0 at the start of the game

        Reset();
    }

    public void Move()
    {
        float speedDifference = maxMovementSpeed - rb.velocity.x;

        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelaration, 1.25f);

        rb.AddForce(movement * Vector2.right * Time.fixedDeltaTime);

        if (!underGlueEffect)
        {
            currentFuel -= Time.fixedDeltaTime;
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

        accelaration *= glueAccelarationMultipliyer;

        Invoke(nameof(ResetGlue), duration);
    }

    void ResetGlue()
    {
        underGlueEffect = false;

        maxMovementSpeed = initialMaxMovementSpeed;

        accelaration /= glueAccelarationMultipliyer;
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
