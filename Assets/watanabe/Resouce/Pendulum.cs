using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Pendulum : MonoBehaviour
{

    [Header("振り子の回転")]
    public float speed = 1.0f;//振り子の速度
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
    public float barLength = 4.0f;




    private float random = 0;

    private float previousAngle = 0;

    public Vector3 pivotOffset = new Vector3(0f, 2f, 0f);

    void Awake()
    {
        if (randomStart)

            random = Random.Range(0f, 1f);
    }

    private void Update()
    {
        float angle = Swinglimit * Mathf.Sin(Time.time + random * speed);
        float deltaAngle = angle - previousAngle;

        Vector3 pivot = transform.TransformPoint(pivotOffset);

        transform.RotateAround(pivot, transform.forward, deltaAngle);

        previousAngle = angle;

    }


}
