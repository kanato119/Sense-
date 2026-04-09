using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpBoard : MonoBehaviour
{
    [SerializeField] private float jumpBoardFoce = 30.0f;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {



            other.gameObject.GetComponent<Rigidbody>().AddForce(0, jumpBoardFoce, 0, ForceMode.Impulse);

        }
    }




}