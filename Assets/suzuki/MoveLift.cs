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
    Vector3 PastTile;
    Vector3 CurrenTile;

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
        //前のポジションの取得
        Vector3 PastTile = transform.position;

        //移動する方向を取得
        Vector3 dir = (LiftPoints[LiftNum].position - transform.position).normalized;
        //移動スピードを固定
        transform.position += dir * LiftSpeed * Time.deltaTime;

        //移動量の計算（今の座標から前の座標を引いて出す）
        Vector3 CurrenTile = transform.position - PastTile;


        if (transform.position == LiftPoints[LiftNum].transform.position)
        {
            LiftNum++;
        }
        if (LiftPoints.Length <= LiftNum)
        {

            LiftNum = 0;
        }

        if (OnPlayer)
        {
            PlayerOnTileForce.MovePosition(GameObject.FindWithTag("Player").transform.position + CurrenTile);
            //collision.rigidbody.MovePosition(collision.rigidbody.position + CurrenTile);
        }



        //}
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //   {





    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnPlayer = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            OnPlayer = false;

        }
    }


}
