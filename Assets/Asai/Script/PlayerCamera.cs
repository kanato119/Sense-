using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("ターゲット設定")]
    [SerializeField] Transform target;

    //プレイヤーの向きを制御用
    public Transform orientation;

    [Header("カメラの距離")]
    [SerializeField] float disatans;
    [SerializeField] float height;

    [Header("カメラの感度")]
    [SerializeField] float mouseSetSensiX;
    [SerializeField] float mouseSetSensiY;

    [Header("視点制御")]
    [SerializeField] float minY;//下方向
    [SerializeField] float maxY;//上方向

    [SerializeField] float xViewpointControl;//横回転
    [SerializeField] float yViewpointControl;//縦回転

    [SerializeField] float BaseSensi;

    [Header("壁貫通防止")]
    [SerializeField] LayerMask whatIsWall; 
    [SerializeField] float sphereRadius = 0.3f; 
    [SerializeField] float wallOffset = 0.2f; 

    // Start is called before the first frame update
    void Start()
    {
        //マウスカーソルを非表示＆中央固定
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //保存されている感度を取得
        float savedValue = PlayerPrefs.GetFloat("SavedSliderValue", mouseSetSensiX);

        //感度設定を適用
        SetSensi(savedValue);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //マウス入力取得
        xViewpointControl += Input.GetAxisRaw("Mouse X") * mouseSetSensiX;
        yViewpointControl -= Input.GetAxisRaw("Mouse Y") * mouseSetSensiY;

        //上下視点制御
        yViewpointControl = Mathf.Clamp(yViewpointControl, minY, maxY);

        //回転情報作成
        Quaternion rotation = Quaternion.Euler(yViewpointControl, xViewpointControl, 0); 

        //カメラを後ろへ移動するためのオフセット
        Vector3 offset = rotation * new Vector3(0, 0, -disatans); 

        //プレイヤー位置 + 高さ
        Vector3 targetPos = target.position + Vector3.up * height; 

        //本来配置したいカメラ位置
        Vector3 desiredPosition = targetPos + offset; 

        //ターゲットからカメラの位置
        Vector3 direction = desiredPosition - targetPos; 

        //最終的なカメラ位置
        Vector3 finalPosition = desiredPosition; 

        Debug.DrawLine(targetPos, desiredPosition, Color.red); 

        // 壁判定
        if (Physics.SphereCast( 
            targetPos,            //開始位置
            sphereRadius,         //球の半径
            direction.normalized, //向き
            out RaycastHit hit,   //当たった情報
            direction.magnitude,  //距離
            whatIsWall))          //判定レイヤー
        { 
            Debug.Log("壁に当たった");
            
            //壁にめり込まない位置へ移動
            finalPosition = hit.point - direction.normalized * wallOffset;
        } 

        //カメラ位置反映
        transform.position = finalPosition;

        //カメラ回転反映
        transform.rotation = rotation;
        
        //プレイヤー向き更新
        orientation.rotation = Quaternion.Euler(0, xViewpointControl, 0);
    }

    //感度変更用関数
    public void SetSensi(float value)
    {
        mouseSetSensiX = value * BaseSensi;
        mouseSetSensiY = value * BaseSensi;
    }
}
