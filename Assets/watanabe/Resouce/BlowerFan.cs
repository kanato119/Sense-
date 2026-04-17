using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowerFan : MonoBehaviour
{
   
    public Vector3 blowDirection = Vector3.up;
    public float blowPower = 20f;
    public float onTime = 3f;
    public float offTime = 3f;

    public Vector3 boxSize = new Vector3(2f, 2f, 5f); //風の範囲
    public Vector3 boxOffset = new Vector3 (0f, 0f, 2.5f);

    private bool isOn = true;
    private float timer = 0f;

    private bool wasInside = false;
    private Vector3 lastVelocity;

    private void Update()
    {
        timer += Time.deltaTime;

        if (isOn)
        {
            if (timer >= onTime)
            {
                isOn = false;
                timer = 0f;
            }
        }

        else
        {
            if (timer >= offTime)
            {
                isOn = true;
                timer = 0f;
            }
        }

    }

    private void FixedUpdate()
    {
        if (!isOn) return;

        Vector3 center = transform.position + transform.TransformDirection(boxOffset);
        Collider[] hits = Physics.OverlapBox(center, boxSize * 0.5f, transform.rotation);

        bool isInside = false;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {

                isInside = true;

                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null && isOn)
                {
                    rb.AddForce(transform.forward * blowPower, ForceMode.Acceleration);
                    lastVelocity = rb.velocity;
                }



            }
        }

        if(wasInside && isInside)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if(player != null)
            {

                Rigidbody rb = player.GetComponent<Rigidbody>();

                if(rb != null)
                {
                    rb.velocity = lastVelocity;
                }


            }
        }

        wasInside = isInside;



    }

        
    




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position,transform.rotation,Vector3.one);

        Gizmos.DrawWireCube(boxOffset, boxSize);

        Gizmos.matrix = oldMatrix;
    }


}
