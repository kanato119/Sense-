using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpBoard : MonoBehaviour
{
    [SerializeField] private float jumpBoardFoceX = 0.0f;
    [SerializeField] private float jumpBoardFoceY = 30.0f;
    [SerializeField] private float jumpBoardFoceZ = 0.0f;



    private void OnTriggerEnter(Collider other)
    {

        Rigidbody rb =  other.GetComponent<Rigidbody>();


        if (other.gameObject.CompareTag("Player"))
        {
         
            
            rb.velocity = new Vector3(jumpBoardFoceX, jumpBoardFoceY, jumpBoardFoceZ);
            


        }
    }




}