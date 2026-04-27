using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{

    [Header("振り子の回転")]
    public float swingspeed = 1.0f;//振り子の速度
    public float Swinglimit = 70f;//振り子の角度
    public bool randomStart = false;

    [Header("オブジェクトの移動")]
    public Vector3 moveDirection = Vector3.right;   //移動方向
    public float moveSpeed = 2.0f;                  //移動速度
    public float moveRange = 3.0f;                  //移動距離

    [Header("棒と先端の球")]
    public Transform bar; //棒
    public Transform ball;//球

    [Header("棒の設定")]
    public float barLength = 4.0f;//棒の長さ
    public float barThickness = 0.5f;//棒の太さ

    [Header("球の設定")]
    public float ballSize = 1f;       //ボールの大きさ
    public bool attachBallToBarEnd = true; //棒の先端にボールを合わせるか

    private float random = 0;
    private Vector3 startPos;

    private MeshCollider meshCol;

    void Awake()
    {
        if (randomStart)

            random = Random.Range(0f, 1f);
    }

    

    private void Start()
    {
        //meshCol = GetComponent<MeshCollider>();

        //if (meshCol != null)
        //{
        //    // MeshCollider を無効化
        //    meshCol.enabled = false;
        //    }
            startPos = transform.position;
        AdjustParts();
    }

    private void Update()
    {
        float angle = Swinglimit * Mathf.Sin(Time.time + random * swingspeed)*swingspeed;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);


        // 親オブジェクトの移動
        Vector3 dir = moveDirection.normalized;
        float move = Mathf.Sin(Time.time * moveSpeed) * moveRange;
        transform.position = startPos + dir * move;

    }   

    private void OnValidate()
    {

        AdjustParts();
    }

    private void AdjustParts()
    {
        if (bar != null)
        {
            // 棒の大きさ
            bar.localScale = new Vector3(barThickness, barLength, barThickness);

            // 棒の上端が親(PendulumRoot)に来るようにする
            bar.localPosition = new Vector3(0f, -barLength / 2f, 0f);
        }

        if (ball != null)
        {
            // 球の大きさ
            ball.localScale = new Vector3(ballSize, ballSize, ballSize);

            // 棒の下端のさらに球半径ぶん下に置く
            ball.localPosition = new Vector3(0f, -barLength - ballSize / 2f, 0f);
        }
    }
}




