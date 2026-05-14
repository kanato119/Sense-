using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    [SerializeField] float groundDrag;

    [Header("空中にいるときの減速スピード(摩擦力)")]
    [SerializeField] float airDrag;

    [Header("Rayの長さ")]
    [SerializeField] float playerHeight;

    [SerializeField] AudioClip JumpSE;

    //地面に触れいるかどうか
    public bool grounded;

    private bool wasGrounded;

    //カメラ基準の向き
    [SerializeField] Transform orientation;

    //ADのキーを取得
    float horizontalInput;

    //WSのキーを取得
    float verticalInput;

    //移動方向
    Vector3 moveDirection;

    RaycastHit hit;

    AudioSource audio;

    Animator animator;

    Rigidbody rb;

    //追加（鈴木）
    public MoveLift pMoveLift;

    private bool OnTile;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

        rb.freezeRotation = true;

        currentSpeed = moveSpeed;

        //追加（鈴木）
        OnTile = false;
    }

    // Update is called once per frame
    void Update()
    {
        float rayDistance = playerHeight * 0.5f + 0.2f;

        Vector3 orijin = transform.position;

        //入力取得
        MyInput();

        if(!wasGrounded && grounded)
        {
            readyToJump = false;

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        wasGrounded = grounded;

        bool isInput = horizontalInput != 0 || verticalInput!=0;
        bool isRuning = Input.GetKey(KeyCode.LeftShift);

        currentSpeed = isRuning ? runSpeed : moveSpeed;

        animator.SetBool("Run", isRuning && isInput && grounded);
        animator.SetBool("Walk",!isRuning && isInput && grounded);

        animator.SetBool("Jump 0", !grounded);

        animator.SetBool("Grounded", grounded);

        animator.SetFloat("yVelocity", rb.velocity.y);



        //スピードコントロール
        SpeedControl();

        //地面にいるときに減速をかける
        if (grounded)
        {
            rb.drag = groundDrag;
        }

        else
        {
            rb.drag = airDrag;
        }

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
            Jump();
        }

    }

    private void MovePlayer()
    {
        //カメラの向きによって移動方向を決める
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.y = 0f;

        if(moveDirection != Vector3.zero)
        {
            Quaternion targetRotaion = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotaion, 10f * Time.deltaTime);


        }

        if(grounded)
        {
            //力を入れて進む
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);

            
        }

        else if(!grounded)
        {
            //力を入れて進む
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        if(grounded && moveDirection.magnitude<0.1f)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }

    }

    private void SpeedControl()
    {
        //上下を無視した移動速度を取得
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        bool isMoving = flatVel.magnitude > 0.2f;



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

        animator.SetTrigger("Jump");

        audio.PlayOneShot(JumpSE);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }


    //追加（鈴木）
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {

            Debug.Log("JIJIJIJIJIJIJIJI");

            GameObject obj = collision.gameObject;
            
            MoveLift lift = obj.GetComponent<MoveLift>();

            Debug.Log(lift.TileVector());

            transform.position += lift.TileVector();


        }
    }

}
