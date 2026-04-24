using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveLift : MonoBehaviour
{

    [SerializeField] Transform[] LiftPoints;

    [SerializeField] private float LiftSpeed = 3.0f;

    [SerializeField] private int LiftNum = 0;


    [SerializeField] private bool OnPlayer;
    //プレイヤーのRigidBody

    Vector3 Movement;

    GameObject PlayerSaveForce;
    Rigidbody PlayerOnTileForce;
    // Start is called before the first frame update
    void Start()
    {

        //playerのタグからプレイヤーのRigidBodyの取得
        GameObject PlayerSaveForce = GameObject.FindWithTag("Player");
        Rigidbody PlayerOnTileForce = PlayerSaveForce.GetComponent<Rigidbody>();

        OnPlayer = false;
       

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        /*Publicの関数の中に入れた
        ////前のポジションの取得
        //Vector3 PastTile = transform.position;

        ////移動する方向を取得
        //Vector3 dir = (LiftPoints[LiftNum].position - transform.position).normalized;
        ////移動スピードを固定
        //transform.position += dir * LiftSpeed * Time.deltaTime;

        ////移動量の計算（今の座標から前の座標を引いて出す）
        Vector3 CurrenTile = transform.position - PastTile;
          */

        Movement = new Vector3(0.0f, 0.0f, 0.0f);


        //前のポジションの取得
        Vector3 PastTile = transform.position;

        //移動する方向を取得
        Vector3 dir = (LiftPoints[LiftNum].position - transform.position).normalized;
        //移動スピードを固定
        transform.position += dir * LiftSpeed * Time.deltaTime;

        //移動量の計算（今の座標から前の座標を引いて出す）
        Vector3 CurrenTile = transform.position - PastTile;
        Movement = transform.position - PastTile;

        Debug.Log(Movement);

        // TileVector();
       

        //誤差を許容
        if (Vector3.Distance(transform.position, LiftPoints[LiftNum].position) < 1.0f)
        {
            LiftNum++;
        }
        if (LiftPoints.Length <= LiftNum)
        {

            LiftNum = 0;
        }

        //if (OnPlayer)
        //{
        //    PlayerOnTileForce.MovePosition(PlayerSaveForce.transform.position += CurrenTile);
        //    collision.rigidbody.MovePosition(collision.rigidbody.position + CurrenTile);


        //}

        //}
    }


    public Vector3 TileVector()
    {
        ////前のポジションの取得
        //Vector3 PastTile = transform.position;

        ////移動する方向を取得
        //Vector3 dir = (LiftPoints[LiftNum].position - transform.position).normalized;
        ////移動スピードを固定
        //transform.position += dir * LiftSpeed * Time.deltaTime;

        ////移動量の計算（今の座標から前の座標を引いて出す）
        //Vector3 CurrentTile = transform.position - PastTile;

        return Movement;

    }
    public void TileOnPlayerMove(Collision collision)
    {

            //Collision collision;

        if (collision.gameObject.CompareTag("Player"))
        {   //rb.velocity += CurrentTile / Time.fixedDeltaTime;

            /*
 
            Rigidbody rb = collision.rigidbody;

            rb.AddForce(CurrentTile / Time.fixedDeltaTime, ForceMode.VelocityChange);
             */




        }
    }




}
