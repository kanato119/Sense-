using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltconveyorRight : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right;  //流れる方向
    public float moveSpeed = 3f;    //流れる速度

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if(rb != null )
            {

                Vector3 direction = moveDirection.normalized;
                Vector3 moveAmout = direction * moveSpeed * Time.deltaTime;

                rb.MovePosition(rb.position +  moveAmout);
            }
            else
            {
                other.transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
            }
        }
    }
}
