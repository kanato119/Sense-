using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpBoard : MonoBehaviour
{
    //ジャンプの強さと方向
    [SerializeField] private Vector3 jumpBoardFoce =  new Vector3(0.0f, 30.0f, 0.0f );
    
    [SerializeField]AudioClip jumpBoardSound;
    private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        //音
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(jumpBoardSound);

        Rigidbody rb =  other.GetComponent<Rigidbody>();

        //プレイヤーが触れたら飛ばす
        if (other.gameObject.CompareTag("Player"))
        {
            rb.velocity = jumpBoardFoce;
        }
    }
}