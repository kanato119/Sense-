using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveLift : MonoBehaviour
{

    [SerializeField]Transform[] LiftPoints;

    [SerializeField]private float LiftSpeed =3.0f;

    [SerializeField]private int LiftNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {



        //LiftPointsの配列を調べる
        //for (int i = 0; i < LiftPoints.Length; i++)
        //{


        //transform.position = Vector3.Lerp(transform.position,
        //    LiftPoints[0].transform.position,
        //    Time.deltaTime * LiftSpeed);


        Vector3 dir = (LiftPoints[LiftNum].position-transform.position).normalized;
        transform.position += dir * LiftSpeed * Time.deltaTime;

        if ( transform.position == LiftPoints[LiftNum].transform.position) {
            LiftNum++;
        }
        if (LiftPoints.Length <= LiftNum)
        {

            LiftNum = 0;
        }

        //}





    }
}
