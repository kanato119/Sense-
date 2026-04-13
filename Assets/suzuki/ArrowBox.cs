using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArrowBox : MonoBehaviour
{

    [SerializeField] Transform BuletPosition;
    [SerializeField] GameObject Bulet;

    //球の発射レート
    [SerializeField] float BuletLate;
  
    [SerializeField] float Speed;

    float Num;


    [SerializeField] Vector3 Direction;

   

    // Start is called before the first frame update
    void Start()
    {
        //numに発射レートを入れる
        Num = BuletLate;
    }

    // Update is called once per frame
    void Update()
    {


        Num -= Time.deltaTime;

        if (Num <= 0)
        {

            //BuletPositionの座標を取得
            Vector3 BuletPoint = BuletPosition.position;

            //球の向きを親オブジェクトと同じにする
            GameObject newBulet = Instantiate(Bulet, BuletPoint, this.gameObject.transform.rotation);

            //Y軸も同じにする
             Direction = newBulet.transform.up;

            //球のオブジェクトに衝撃を加える
            newBulet.GetComponent<Rigidbody>().AddForce(Direction * Speed, ForceMode.Impulse);

            //名前の変更
            newBulet.name = Bulet.name;

            //numに発射レートを入れる
            Num = BuletLate;

        }


     }
   
    }

