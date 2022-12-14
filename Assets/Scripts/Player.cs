using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Vector2 spawnPos;

    float movementSpeed;
    public float maxMovementSpeedCart;
    public float maxMovementSpeedSurfboard;
    [HideInInspector]
    public float initialMaxMovementSpeedCart; // used to reset the value
    [HideInInspector]
    public float initialMaxMovementSpeedSurfboard; // used to reset the value
    [HideInInspector]
    public float movementSpeedCartMultiplier;
    [HideInInspector]
    public float movementSpeedSufboardMultiplier;
    [HideInInspector]
    public bool isOnCart;

    public float acceleration;
    [HideInInspector]
    public float initialAcceleration; // used to reset the value
    public float glueAccelerationMultipliyer;

    public float baseMaxFuel;
    [HideInInspector]
    public float maxFuel;
    [HideInInspector]
    public float currentFuel;
    [HideInInspector]
    public float fuelMultiplier;

    [HideInInspector]
    public int coins;

    [HideInInspector]
    public bool underGlueEffect;

    [HideInInspector]
    public Rigidbody2D rb;

    public Animator animator;

    public ParticleSystem fireExtinguisherParticle;

    public TextMeshProUGUI playerFuel, playerDistance;

    public AudioManager audioManager;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        initialMaxMovementSpeedCart = maxMovementSpeedCart;
        initialMaxMovementSpeedSurfboard = maxMovementSpeedSurfboard;
        movementSpeedCartMultiplier = 1.0f;
        movementSpeedSufboardMultiplier = 1.0f;
        initialAcceleration = acceleration;
        fuelMultiplier = 1.0f;

        coins = 0; // Coins are only set to 0 at the start of the game

        // get animator component
        animator = GetComponent<Animator>();

        Reset();
    }

    private void Update()
    {
        playerFuel.text = "Fuel: " + currentFuel.ToString("F1") + "/" + maxFuel.ToString("0");
        playerDistance.text = "Distance: " + (int)(transform.position.x - spawnPos.x);
    }

    public void Move()
    {
        float maxSpeed;

        if (isOnCart)
        {
            maxSpeed = maxMovementSpeedCart * movementSpeedCartMultiplier;
        }
        else
        {
            maxSpeed = maxMovementSpeedSurfboard * movementSpeedSufboardMultiplier;
        }

        float speedDifference = maxSpeed - rb.velocity.x;

        float movement = speedDifference * acceleration;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

        if (!underGlueEffect)
        {
            currentFuel -= Time.deltaTime;
            // play the fire extinguisher particle
            fireExtinguisherParticle.Play();
            audioManager.Play("FireExtinguisher");
        }
        else
        {
            // stop the fire extinguisher particle
            fireExtinguisherParticle.Stop();
            audioManager.Stop("FireExtinguisher");
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

    public void UpdateFuel()
    {
        maxFuel = baseMaxFuel * fuelMultiplier;

        currentFuel = maxFuel;
    }

    public void SniffGlue(float movementSpeedPlus, float duration)
    {
        if (underGlueEffect)
        {
            // When the player is already under the glue effect when sniffing another glue tube,
            // we just need to delay when the reset to the altered stats happens.
            // For that we cancel all of the invokes of the method ResetGlue and then we Invoke it again.

            CancelInvoke(nameof(ResetGlue));
            Invoke(nameof(ResetGlue), duration);
        }
        else
        {
            underGlueEffect = true;

            initialMaxMovementSpeedCart = maxMovementSpeedCart;
            initialMaxMovementSpeedSurfboard = maxMovementSpeedSurfboard;

            maxMovementSpeedCart += movementSpeedPlus;
            maxMovementSpeedSurfboard += movementSpeedPlus;
            acceleration *= glueAccelerationMultipliyer;
            animator.SetBool("GotGlue", true);
            Invoke(nameof(ResetGlue), duration);
        }

    }

    void ResetGlue()
    {
        underGlueEffect = false;
        animator.SetBool("GotGlue", false);
        maxMovementSpeedCart = initialMaxMovementSpeedCart;
        maxMovementSpeedSurfboard = initialMaxMovementSpeedSurfboard;
        acceleration = initialAcceleration;
    }

    public void RefillFuel(float fuelPlus)
    {
        currentFuel += fuelPlus;

        if (currentFuel > maxFuel)
        {
            currentFuel = maxFuel;
        }
    }

    public void Reset()
    {
        underGlueEffect = false;

        isOnCart = true;

        transform.position = spawnPos;
    }

    // stop particle system
    public void StopParticle()
    {
        fireExtinguisherParticle.Stop();
        audioManager.Stop("FireExtinguisher");
    }
}
