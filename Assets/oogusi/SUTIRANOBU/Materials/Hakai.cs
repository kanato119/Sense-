using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hakai : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(new Vector3(-1, -1, 0) * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.transform.position = new Vector3(0, 0, 0);
        }
    }
}