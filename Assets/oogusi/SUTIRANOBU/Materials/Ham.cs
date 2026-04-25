using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ham : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = new Vector3(0, 0, 0);
        }
    }
}