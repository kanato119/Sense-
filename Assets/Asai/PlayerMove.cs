using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移動スピード")]
    public float moveSpeed;

    [Header("ジャンプの高さ")]
    [SerializeField] float jumpForce;

    [Header("次のジャンプできるまでの時間")]
    [SerializeField] float jumpCooldown;

    [Header("重力")]
    [SerializeField] float fallMutipler;

    [Header(" ")]
    [SerializeField] float airMultiplier;
    bool readyToJump = true;

    [Header("ダッシュスピード")]
    [SerializeField] float runSpeed;

    float currentSpeed;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("地面にいるときの減速スピード(摩擦力)")]
    public float groundDrag;

    [Header("Rayの長さ")]
    public float playerHeight;

    //地面に触れいるかどうか
    bool grounded;

    //カメラ基準の向き
    public Transform orientation;

    //ADのキーを取得
    float horizontalInput;

    //WSのキーを取得
    float verticalInput;

    //移動方向
    Vector3 moveDirection;

    RaycastHit hit;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float rayDistance = playerHeight * 0.5f + 0.2f;
        Vector3 boxHalfExtents = new Vector3(1.8f, 0.1f, 1.8f);

        grounded = Physics.BoxCast(
            transform.position, 
            boxHalfExtents, 
            Vector3.down, 
            out hit, 
            transform.rotation, 
            rayDistance);

        //Rayを下に伸ばして地面かどうか判断する
        //grounded = Physics.Raycast(transform.position, Vector3.down, rayDistance);

        //入力取得
        MyInput();

        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = runSpeed;

        else
            currentSpeed = moveSpeed;

        //スピードコントロール
        SpeedControl();

        //地面にいるときに減速をかける
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        //Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red);

        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red);

        //Vector3 boxStart = transform.position;

        Vector3 boxEnd = transform.position + Vector3.down * rayDistance;

        //DrawBox(boxStart, boxHalfExtents, Color.green);

        DrawBox(boxEnd, boxHalfExtents, Color.blue);


    }

    private void FixedUpdate()
    {
        MovePlayer();

        //落下スピードを早くする
        if(rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.up * Physics.gravity.y * (fallMutipler - 1), ForceMode.Acceleration);
        }

        //キーを離したら空中にいる時間を短くする
        else if(rb. velocity.y > 0 &&!Input.GetKeyDown(jumpKey))
        {
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.5f,rb.velocity.z); 

            rb.AddForce(Vector3.up * Physics.gravity.y * (fallMutipler - 1), ForceMode.Acceleration);
        }
        
    }

    //キー入力の取得
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); //AD
        verticalInput = Input.GetAxisRaw("Vertical");     //WS

        if(Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void MovePlayer()
    {
        //カメラの向きによって移動方向を決める
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
        //力を入れて進む
        rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);

        else if(!grounded)
            //力を入れて進む
        rb.AddForce(moveDirection.normalized * currentSpeed * 10f * airMultiplier, ForceMode.Force);

        /*
        //地面に触れいる時だけ移動できる
        if (grounded)
        {
            //入力がある時進む
            if (moveDirection.magnitude > 0)
            {
                //力を入れて進む
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            else
            {
                // 入力が無いときはピタッと止める
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            }
        }
        */
    }

    private void SpeedControl()
    {
        //上下を無視した移動速度を取得
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //最大速度を超えそうになったら制限する
        if (flatVel.magnitude > currentSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currentSpeed;

            //YはそのままにしてXZだけ制限する
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    void DrawBox(Vector3 center, Vector3 halfExtents, Color color)
    {
        Vector3[] points = new Vector3[8];

        // 上面
        points[0] = center + new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
        points[1] = center + new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
        points[2] = center + new Vector3(halfExtents.x, halfExtents.y, halfExtents.z);
        points[3] = center + new Vector3(-halfExtents.x, halfExtents.y, halfExtents.z);

        // 下面
        points[4] = center + new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
        points[5] = center + new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);
        points[6] = center + new Vector3(halfExtents.x, -halfExtents.y, halfExtents.z);
        points[7] = center + new Vector3(-halfExtents.x, -halfExtents.y, halfExtents.z);

        // 上面
        Debug.DrawLine(points[0], points[1], color);
        Debug.DrawLine(points[1], points[2], color);
        Debug.DrawLine(points[2], points[3], color);
        Debug.DrawLine(points[3], points[0], color);

        // 下面
        Debug.DrawLine(points[4], points[5], color);
        Debug.DrawLine(points[5], points[6], color);
        Debug.DrawLine(points[6], points[7], color);
        Debug.DrawLine(points[7], points[4], color);

        // 縦
        Debug.DrawLine(points[0], points[4], color);
        Debug.DrawLine(points[1], points[5], color);
        Debug.DrawLine(points[2], points[6], color);
        Debug.DrawLine(points[3], points[7], color);
    }
}
