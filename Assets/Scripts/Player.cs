using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Vector2 spawnPos;

    public float maxMovementSpeed;
    public float accelaration;

    public float maxFuel;
    float currentFuel;

    [HideInInspector]
    public int coins;

    [HideInInspector]
    public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Reset();
    }

    public void Move()
    {
        float speedDifference = maxMovementSpeed - rb.velocity.x;

        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelaration, 1.25f);

        rb.AddForce(movement * Vector2.right * Time.fixedDeltaTime);

        currentFuel -= Time.fixedDeltaTime;
    }

    public bool hasFuel()
    {
        return currentFuel > 0;
    }

    public void Reset()
    {
        currentFuel = maxFuel;

        transform.position = spawnPos;
    }
}
