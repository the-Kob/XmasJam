using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static float MAX_TIME_INACTIVE = 3.0f;

    Player player;

    public bool isGameRunning;

    float timePassedInactive;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        isGameRunning = true; // TODO change this value to false, it's only true for debugging purposes
        timePassedInactive = 0.0f;
    }

    void FixedUpdate()
    {
        if (isGameRunning)
        {
            # region Move

            if (Input.GetKey(KeyCode.W) && player.HasFuel())
            {
                player.Move();
            }

            #endregion

            #region Inactivity check

            if (Vector2.SqrMagnitude(player.rb.velocity) <= 0.01)
            {
                timePassedInactive += Time.fixedDeltaTime;
            } else
            {
                timePassedInactive = 0.0f;
            }

            if(timePassedInactive >= MAX_TIME_INACTIVE)
            {
                Reset();
            }

            #endregion
        }

        Debug.Log(player.rb.velocity.x);
    }

    private void Reset()
    {
        isGameRunning = false;

        player.Reset();
    }
}
