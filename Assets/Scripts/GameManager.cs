using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static float MAX_TIME_INACTIVE = 3.0f;

    public Player player;
    public CameraMovement cam;

    public MapManager mapManager;

    public bool isGameRunning;

    float timePassedInactive;

    public AudioManager audioManager;

    void Awake()
    {
        isGameRunning = false;
        timePassedInactive = 0.0f;
        audioManager.Play("AmbienceMusic");
    }

    void FixedUpdate()
    {
        if (!isGameRunning)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                PreparePlay();

                Invoke(nameof(Play), 3); // Let the camera go its place
            }
        }

        if (isGameRunning)
        {
            # region Move

            if (Input.GetKey(KeyCode.Space) && player.HasFuel())
            {
                player.Move();
            }

            #endregion

            #region Inactivity check

            if (Vector2.SqrMagnitude(player.rb.velocity) <= 0.01)
            {
                timePassedInactive += Time.fixedDeltaTime;
            }
            else
            {
                timePassedInactive = 0.0f;
            }

            if (timePassedInactive >= MAX_TIME_INACTIVE)
            {
                Reset();
            }

            #endregion
        }
    }

    void PreparePlay()
    {
        cam.SwitchToPlay();
    }

    void Play()
    {
        isGameRunning = true;
    }

    void Reset()
    {
        isGameRunning = false;
        timePassedInactive = 0.0f;

        mapManager.GenerateSectionAtZero();
        player.Reset();
        cam.SwitchToStore();
    }
}
