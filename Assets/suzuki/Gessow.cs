using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gessow : MonoBehaviour
{

   

    [SerializeField] public Image Sumi;

    // Start is called before the first frame update
   
    private void OnCollisionEnter(Collision collision)
    {

      //  Sumi.Equals = true;

        Invoke("OffImag", 2.0f);
    }

    public void OffImg()
    {


    } 

}
