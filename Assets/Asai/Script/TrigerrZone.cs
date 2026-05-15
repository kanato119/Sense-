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

    //地面に触れていたら
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Tile"))
        {
            Debug.Log("Ground Hit");
            Pm.grounded = true;
        }
    }

    //地面に触れていなかったら
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ground") || other.CompareTag("Tile"))
        {
            Pm.grounded = false;
        }
    }
}
