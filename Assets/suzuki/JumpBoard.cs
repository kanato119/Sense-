using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpBoard : MonoBehaviour
{
    [SerializeField] private float jumpBoardFoceX = 0.0f;
    [SerializeField] private float jumpBoardFoceY = 30.0f;
    [SerializeField] private float jumpBoardFoceZ = 0.0f;
    [SerializeField]AudioClip jumpBoardSound;
    private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(jumpBoardSound);
        Rigidbody rb =  other.GetComponent<Rigidbody>();


        if (other.gameObject.CompareTag("Player"))
        {
         
            
            rb.velocity = new Vector3(jumpBoardFoceX, jumpBoardFoceY, jumpBoardFoceZ);
            


        }
    }




}