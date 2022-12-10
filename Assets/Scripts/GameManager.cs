using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float maxTimeInactive;

    Player player;
    CameraMovement cam;
    public Canvas canvas;
    public TextMeshProUGUI playerCoinsText;

    public MapManager mapManager;

    public bool isGameRunning;

    float timePassedInactive;

    public AudioManager audioManager;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        cam = FindObjectOfType<CameraMovement>();

        isGameRunning = false;
        timePassedInactive = 0.0f;
        audioManager.Play("AmbienceMusic");
    }

    void FixedUpdate()
    {
        playerCoinsText.text = "Coins: " + player.coins;

        if (!isGameRunning)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                PreparePlay();
            }
        }

        if (isGameRunning)
        {
            # region Move

            if (Input.GetKey(KeyCode.Space) && player.HasFuel())
            {
                player.Move();
            }
            else
            {
                player.StopParticle();
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

            if (timePassedInactive >= maxTimeInactive)
            {
                Reset();
            }

            #endregion
        }
    }

    public void PreparePlay()
    {
        player.UpdateFuel();
        cam.SwitchToPlay();
        canvas.gameObject.SetActive(false);

        Invoke(nameof(Play), 3); // Let the camera go its place
    }

    void Play()
    {
        isGameRunning = true;
        mapManager.DestroyAllSections();
        mapManager.reseting = false;
    }

    void Reset()
    {
        isGameRunning = false;
        timePassedInactive = 0.0f;
        mapManager.reseting = true;
        mapManager.GenerateSectionAtZero();
        player.Reset();
        cam.SwitchToStore();
    }
}
