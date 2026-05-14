using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("ターゲット設定")]
    [SerializeField] Transform target;

    public Transform orientation;

    [Header("カメラの距離")]
    [SerializeField] float disatans;
    [SerializeField] float height;

    [Header("カメラの感度")]
    [SerializeField] float mouseSetSensiX;
    [SerializeField] float mouseSetSensiY;

    [Header("視点制御")]
    [SerializeField] float minY;
    [SerializeField] float maxY;

    [SerializeField] float xViewpointControl;
    [SerializeField] float yViewpointControl;

    [SerializeField] float BaseSensi;

    [Header("壁貫通防止")]
    [SerializeField] LayerMask whatIsWall; 
    [SerializeField] float sphereRadius = 0.3f; 
    [SerializeField] float wallOffset = 0.2f; 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        float savedValue = PlayerPrefs.GetFloat("SavedSliderValue", mouseSetSensiX);
        SetSensi(savedValue);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        xViewpointControl += Input.GetAxisRaw("Mouse X") * mouseSetSensiX;
        yViewpointControl -= Input.GetAxisRaw("Mouse Y") * mouseSetSensiY;

        yViewpointControl = Mathf.Clamp(yViewpointControl, minY, maxY);

        Quaternion rotation = Quaternion.Euler(yViewpointControl, xViewpointControl, 0); 

        Vector3 offset = rotation * new Vector3(0, 0, -disatans); 

        Vector3 targetPos = target.position + Vector3.up * height; 

        Vector3 desiredPosition = targetPos + offset; 

        Vector3 direction = desiredPosition - targetPos; 

        Vector3 finalPosition = desiredPosition; 

        Debug.DrawLine(targetPos, desiredPosition, Color.red); 

        // 壁判定
        if (Physics.SphereCast( 
            targetPos, 
            sphereRadius, 
            direction.normalized, 
            out RaycastHit hit, 
            direction.magnitude, 
            whatIsWall)) 
        { 
            Debug.Log("壁に当たった");
            finalPosition = hit.point - direction.normalized * wallOffset;
        } 

        transform.position = finalPosition;

        transform.rotation = rotation;
        orientation.rotation = Quaternion.Euler(0, xViewpointControl, 0);
    }

    public void SetSensi(float value)
    {
        mouseSetSensiX = value * BaseSensi;
        mouseSetSensiY = value * BaseSensi;
    }
}
