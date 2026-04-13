using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltconveyorLeft : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right;  //流れる方向
    public float moveSpeed = 3f;    //流れる力

    private Vector3 lastVelocity;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {

                Vector3 direction = moveDirection.normalized;
                Vector3 moveAmout = direction * moveSpeed * Time.deltaTime;

                rb.MovePosition(rb.position + moveAmout);



                //今ベルトコンベアで動かしている速度を保存
                lastVelocity = direction * moveSpeed;


            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();


            if (rb != null)
            {

                rb.velocity = lastVelocity;
            }
        }

    }
}


