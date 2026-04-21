using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public Transform Player;

    public float speed = 5.0f;
    public float KeepDistance = 2f;

    private bool isFollowing = false;
    private Collider keyCollider;

    private void Start()
    {
        keyCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Player == null)
        {
            return;
        }

        if (!isFollowing)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, Player.position);

        if (distance > KeepDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                Player.position,
                speed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFollowing)
        {
            isFollowing = true;

            KeyHold keyHolder = other.GetComponent<KeyHold>();
            if (keyHolder != null)
            {
                keyHolder.hasKey = true;
            }

            if (keyCollider != null)
            {
                keyCollider.enabled = false;
            }
        }
    }
}