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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        xViewpointControl += Input.GetAxisRaw("Mouse X") * mouseSetSensiX;
        yViewpointControl -= Input.GetAxisRaw("Mouse Y") * mouseSetSensiY;

        yViewpointControl = Mathf.Clamp(yViewpointControl, minY, maxY);

        Quaternion rotation = Quaternion.Euler(yViewpointControl, xViewpointControl, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -disatans);
        Vector3 desiredPosiotion = target.position + Vector3.up * height + offset;

        transform.position = desiredPosiotion;
        transform.LookAt(target.position + Vector3.up * height);

        orientation.rotation = Quaternion.Euler(0, xViewpointControl, 0);
    }
}
