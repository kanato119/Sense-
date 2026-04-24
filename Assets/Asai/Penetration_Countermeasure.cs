using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Penetration_Countermeasure : MonoBehaviour
{

    private GameObject Parent;

    private Vector3 Position;

    private RaycastHit Hit;

    //private float Distace;

    private int Mask;

    public float smoothSpeed = 10f;

    public float radius = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        Parent = transform.root.gameObject;

        Position = transform.localPosition;

        //Distace = Vector3.Distance(Parent.transform.position,transform.position);

        Mask = ~(1 << LayerMask.NameToLayer("whatIsWall"));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPos = Parent.transform.TransformPoint(Position);
        Vector3 dir = desiredPos - Parent.transform.position;

        float Distace = dir.magnitude;

        if(dir != Vector3.zero)
            dir.Normalize();

        Vector3 targetPos;



        /*
        if (Physics.CheckSphere(Parent.transform.position, radius, Mask))
        {
            targetPos = Vector3.Lerp(transform.position, Parent.transform.position, 1);
        }

        */
        if (Physics.SphereCast(Parent.transform.position, radius,dir, out Hit, Distace, Mask))
        {
            targetPos = Parent.transform.position + dir * Hit.distance;
        }

        else
        {
            targetPos = Parent.transform.TransformPoint(Position);
     
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);

        Debug.DrawRay(Parent.transform.position, dir * Distace, Color.red);
    }
}
