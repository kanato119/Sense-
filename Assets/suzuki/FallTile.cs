using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallTile : MonoBehaviour
{
    Rigidbody rb;

    bool hoge = false;

    [SerializeField] float timer = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;


    }

    // Update is called once per frame
    void Update()
    {

        if (hoge)
        {

            timer -= Time.deltaTime;

            //0秒になったら
            if (timer < 0)
            {
                //RigidBody追加
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;


            }
            else { 
            
                rb.constraints = RigidbodyConstraints.FreezeRotation ;
                rb.constraints= RigidbodyConstraints.FreezePosition ;
            
            }
          

        }

        //Debug.Log(timer);


    }


    private void OnCollisionEnter(Collision collision)
    {

        hoge = true;
    }

 
  

}
