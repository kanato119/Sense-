using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gessow : MonoBehaviour
{



    [SerializeField] public GameObject Sumi;

    [SerializeField] float Time;

    private void Start()
    {
        Sumi.SetActive(false);
    }

    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {

        //  Sumi.Equals = true;



        Sumi.SetActive(true);
        Invoke("OffImg", 2.0f);

    }

    public void OffImg()
    {

        Sumi.SetActive(false);


    }

}
