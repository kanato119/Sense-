using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climing : MonoBehaviour
{
    [Header("プレイヤーのカメラの向き")]
    public Transform orientaion;
    [Header("Player")]
    public Rigidbody rb;
    [Header("Playerスクリプト")]
    public PlayerMovement pm;
    //アニメーション
    Animator animator;

    [Header("壁として判定")]
    public LayerMask whatIsWall;
    [Header("壁検出の距離")]
    public float detectionLength;
    [Header("SphereCastの半径")]
    public float sphereCastRadius;
    [Header("壁に足して許容する角度")]
    public float maxWallLookAngle;



    [SerializeField] float climbDuration = 0.5f;
    [SerializeField] float climbHeightOffset = 1.5f;
    [SerializeField] float climbForwardOffset = 0.5f;

    [Header("SphereCastの高さを変える")]
    [SerializeField] float wallCheckHeight;

    //壁に当たった情報
    private RaycastHit frontWallHit;
    //前に壁があるかどうか
    private bool wallFront;
    //実査の壁との角度
    private float wallLookAngle;

    //上っているかどうか
    private bool climbing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        //登っているときの処理
        if (climbing) return;

        //壁の検出
        WallCheck();

        if(wallFront && Input.GetKeyDown(KeyCode.E) && wallLookAngle < maxWallLookAngle)
        {
            bool spaceAbove = !Physics.Raycast(transform.position + Vector3.up * climbHeightOffset,
                orientaion.forward,
                detectionLength,
                whatIsWall);

            if(spaceAbove)
            {
                StartCoroutine(ClimbLedge());
            }
        }

        Debug.Log(wallFront);
    }

    /*
    private void StateMachine()
    {
        if(wallFront && Input.GetKey(KeyCode.E) && wallLookAngle < maxWallLookAngle)
        {
            if (!climbing && climbTimer > 0) StartClimbing();

            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimbing();
        }

        else
        {
            if(climbing)StopClimbing();

            
        }
    }
    */
    private void WallCheck()
    {
        Vector3 origin = transform.position + Vector3.up * wallCheckHeight;

        wallFront = Physics.SphereCast(origin, sphereCastRadius, orientaion.forward, out frontWallHit, detectionLength, whatIsWall);

        if(wallFront)
        {
            wallLookAngle = Vector3.Angle(orientaion.forward, -frontWallHit.normal);
        }


    }

     IEnumerator ClimbLedge()
    {
        climbing = true;

        pm.enabled = false;
        rb.isKinematic = true;

        animator.SetTrigger("Climb");

        Vector3 startPos = transform.position;

        Vector3 targetPos = 
            frontWallHit.point + 
            frontWallHit.normal * 0.3f + 
            Vector3.up * climbHeightOffset;

        float time = 0;

        while (time < climbDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / climbDuration);

            time += Time.deltaTime;
            yield return null;
        }

       transform.position = targetPos;

        rb.isKinematic = false;
        pm.enabled = true;

        climbing = false;
    }

/*
    private void StartClimbing()
    {
        climbing = true;

        animator.SetBool("Climing", true);

        //Camera fov change
    }
    private void ClimbingMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);

        //Sound effect
    }

    private void StopClimbing()
    {
        climbing = false;

        animator.SetBool("Climing", false);

        //particle effect
    }
    */
    private void OnDrawGizmos()
    {
        if (orientaion == null) return;

        Gizmos.color = wallFront ? Color.green : Color.red;

        // 開始位置
        Vector3 start = transform.position + Vector3.up * wallCheckHeight;

        // 終点
        Vector3 end = start + orientaion.forward * detectionLength;

        // スタートとゴールに球
        Gizmos.DrawWireSphere(start, sphereCastRadius);
        Gizmos.DrawWireSphere(end, sphereCastRadius);

        // 線
        Gizmos.DrawLine(start, end);
    }
}
