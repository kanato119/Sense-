using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BuletPrefab : MonoBehaviour
{


    [SerializeField]float KnockBackForce;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) { 
        

            Rigidbody rb = other.GetComponent<Rigidbody>();

        rb.velocity = Vector3.zero;


        Vector3 dir=( other.transform.position-transform.position ).normalized;

        rb.AddForce(dir * KnockBackForce, ForceMode.VelocityChange);

        }
        else if(!other.gameObject.CompareTag("ArrowTag"))
        {

            Destroy(this.gameObject);

        }
        
    }
}
