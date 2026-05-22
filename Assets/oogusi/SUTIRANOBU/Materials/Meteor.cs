using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 0.1f;
    public Vector3 direction = new Vector3(-1, -1, 0);

    private Transform startPoint;

    void Start()
    {
        GameObject sp = GameObject.Find("StartPoint");
        if (sp != null)
        {
            startPoint = sp.transform;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = direction.normalized * speed;

        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (startPoint != null)
            {
                other.transform.position = startPoint.position;
            }
        }

        Destroy(gameObject);
    }
}