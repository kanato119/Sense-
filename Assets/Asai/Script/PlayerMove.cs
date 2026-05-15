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

    [Header("空中での移動倍率")]
    [SerializeField] float airMultiplier;

    //ジャンプ可能かどうか
    bool readyToJump = true;

    [Header("ダッシュスピード")]
    [SerializeField] float runSpeed;

    //現在のスピード
    float currentSpeed;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("地面にいるときの減速スピード(摩擦力)")]
    [SerializeField] float groundDrag;

    [Header("空中にいるときの減速スピード(摩擦力)")]
    [SerializeField] float airDrag;

    [Header("Rayの長さ")]
    [SerializeField] float playerHeight;

    [Header("ジャンプSE")]
    [SerializeField] AudioClip JumpSE;

    //地面に触れいるかどうか
    public bool grounded;

    //前フレームで接地していたか
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
        //AudioSource取得
        audio = GetComponent<AudioSource>();

        //Rigidbody取得
        rb = GetComponent<Rigidbody>();

        //Animator取得
        animator = GetComponent<Animator>();

        //回転させないように
        rb.freezeRotation = true;

        //初期スピード
        currentSpeed = moveSpeed;

        //追加（鈴木）
        OnTile = false;
    }

    // Update is called once per frame
    void Update()
    {
        //入力取得
        MyInput();

        //着地した瞬間にジャンプをできるようにする
        if(!wasGrounded && grounded)
        {
            readyToJump = false;

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //現在の接地状態を保存
        wasGrounded = grounded;

        //入力されているか
        bool isInput = horizontalInput != 0 || verticalInput!=0;
        
        //現在のスピード切り替え
        bool isRuning = Input.GetKey(KeyCode.LeftShift);

        //アニメーション制御
        currentSpeed = isRuning ? runSpeed : moveSpeed;

        //アニメーション
        animator.SetBool("Run", isRuning && isInput && grounded);
        animator.SetBool("Walk",!isRuning && isInput && grounded);

        //空中ならJumpアニメーションを流す
        animator.SetBool("Jump 0", !grounded);

        //接地状態をAnimatorへ
        animator.SetBool("Grounded", grounded);

        // Y方向速度をAnimatorへ
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
        //プレイヤー移動
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

        //ジャンプ
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
        //上方向速度リセット
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //上方向へ力を加える
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        //ジャンプアニメーション
        animator.SetTrigger("Jump");

        //ジャンプSE再生
        audio.PlayOneShot(JumpSE);
    }

    //ジャンプ可能に戻す
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
