using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gessow : MonoBehaviour
{



    [SerializeField] public GameObject Sumi;

    [SerializeField] float Time;

    [SerializeField] AudioClip GessowSE;
    private AudioSource audioSource;
    private void Start()
    {
        Sumi.SetActive(false);
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {

        //  Sumi.Equals = true;



        Sumi.SetActive(true);
        Invoke("OffImg", 2.0f);
        audioSource.PlayOneShot(GessowSE);


    }

    public void OffImg()
    {

        Sumi.SetActive(false);


    }

}
