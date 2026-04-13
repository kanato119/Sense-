using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BuletPrefab : MonoBehaviour
{

    [SerializeField] float Speed;
    [SerializeField] Vector3 Direction;

    [SerializeField] float KnockBack;


    

 


    private void OnTriggerEnter(Collider other)
    {

        Rigidbody rb = other.GetComponent<Rigidbody>();
        

        if (other.gameObject.CompareTag("Player"))
        {

            Debug.Log(Direction * (Speed * KnockBack));

            Vector3 KnockBuckDirection=(transform.position-other.transform.position).normalized;


            rb.AddForce(KnockBuckDirection * Speed, ForceMode.Impulse);





        }
    }
}
