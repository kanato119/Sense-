using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    [Header("BGM")]

   // public AudioClip startBGM;   // スタートのBGM

    [SerializeField] AudioSource startAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        startAudioSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
