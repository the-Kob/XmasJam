using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Player player;
    public Canvas canvas;

    bool isFollowingPlayer, isLocked;

    public Transform storePosition, playPosition;
    public float transitionDuration = 2.5f;

    Vector3 playerOffset = new Vector3(5f, 1.4f, -10);

    void Awake()
    {
        player = FindObjectOfType<Player>();

        isFollowingPlayer = false;
        isLocked = true;
    }

    void Update()
    {
        if (isLocked)
        {
            if (!isFollowingPlayer)
            {
                transform.position = storePosition.position;
            }
            else
            {
                transform.position = player.transform.position + playerOffset;

            }
        }
    }

    public void SwitchToPlay()
    {
        isLocked = false;
        isFollowingPlayer = true;

        StartCoroutine(Transition(playPosition));
    }

    public void SwitchToStore()
    {
        isLocked = false;
        isFollowingPlayer = false;

        StartCoroutine(Transition(storePosition));

        Invoke(nameof(ShowStore), transitionDuration);
    }

    IEnumerator Transition(Transform target)
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;

        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);


            transform.position = Vector3.Lerp(startingPos, target.position, t);
            yield return 0;
            isLocked = true;
        }
    }

    void ShowStore()
    {
        canvas.gameObject.SetActive(true);
    }
}
