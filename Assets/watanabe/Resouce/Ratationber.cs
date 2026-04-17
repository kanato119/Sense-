using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratationber : MonoBehaviour
{
    public Vector3 rotateAxis = Vector3.up; //回転軸
    public float rotateSpeed = 120f;        //回転速度

    public float knockbackPower = 100f; // 吹っ飛ぶ強さ
    public float upPower = 10f;         // 上方向の強さ

    private void Update()
    {
        transform.Rotate(rotateAxis*rotateSpeed* Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if(rb != null)
            {
                Vector3 direction =(collision.transform.position - transform.position).normalized;
                direction.y = 0f;

                Vector3 addVelocity = direction * knockbackPower + Vector3.up * upPower;
                rb.velocity += addVelocity;

            }
        }


    }

}
