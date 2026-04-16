using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gessow : MonoBehaviour
{

    [SerializeField] bool OnGimik;

    [SerializeField] float huga=2;
    // Start is called before the first frame update
    void Start()
    {
        OnGimik = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.parent.position;
        //transform.rotation = transform.parent.rotation;
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {

            OnGimik = true;

        }


        if (OnGimik)
        {


            gameObject.GetComponent<Renderer>().enabled = true;

            huga -= Time.deltaTime;

        }
        else
        {

            gameObject.GetComponent<Renderer>().enabled = false;
        }

        if (huga >= 0)
        {

            OnGimik = false;

            huga = 2;
        }


    }
}
