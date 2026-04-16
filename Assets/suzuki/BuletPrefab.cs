using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BuletPrefab : MonoBehaviour
{


    [SerializeField] float KnockBackForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();

            if (rb != null)
            {
                Vector3 dir = rb.transform.position - transform.position;
                dir.y = 0;
                dir = dir.normalized;

                Vector3 force = dir * KnockBackForce;
                force.y = 5f;

                rb.AddForce(force, ForceMode.Impulse);
                Destroy(gameObject);
                
            }

        }
        else if (!other.CompareTag("ArrowTag"))
        {
            Destroy(gameObject);
        }
    }
}