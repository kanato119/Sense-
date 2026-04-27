using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ham : MonoBehaviour
{
    public Transform startPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f)
                {
                    collision.transform.position = startPoint.position;
                    break;
                }
            }
        }
    }
}