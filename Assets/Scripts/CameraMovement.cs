using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Player player;

    bool isFollowingPlayer, isLocked;

    public Transform storePosition, playPosition;
    public float transitionDuration = 2.5f;

    Vector3 playerOffset = new Vector3(6, 1.5f, -10);

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
                //float paralax = (player.transform.position.x - startPos) * paralaxEffect;
                //transform.position = new Vector3(startPos + paralax, transform.position.y, transform.position.z);
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
}
