using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerrZone : MonoBehaviour
{
    [SerializeField] private PlayerMovement Pm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Ground Hit");
            Pm.grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            Pm.grounded = false;
        }
    }
}
