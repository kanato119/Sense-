using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 20f;
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (startPoint != null)
            {
                collision.transform.position = startPoint.position;
            }
        }

        Destroy(gameObject);
    }
}