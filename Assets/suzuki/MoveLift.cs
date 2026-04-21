using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveLift : MonoBehaviour
{

    [SerializeField]Transform[] LiftPoints;

    [SerializeField]private float LiftSpeed =3.0f;

    [SerializeField]private int LiftNum = 0;

    //プレイヤーのRigidBody


    //このオブジェクトのRigidBody
    private Rigidbody TileRb;

    //オブジェクトの速度
    private Vector3 LastTileForce;


    // Start is called before the first frame update
    void Start()
    {

        //playerのタグからプレイヤーのRigidBodyの取得
        GameObject PlayerSaveForce = GameObject.FindWithTag("Player");
        Rigidbody PlayerOnTileForce =PlayerSaveForce.GetComponent<Rigidbody>();



    }

    // Update is called once per frame
    void Update()
    {




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
    private void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {

           


        }

    }



}
