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

    [Header("Keybinds")]
    [SerializeField] KeyCode Climb = KeyCode.Space;


    [Header("クライミング時間")]
    [SerializeField] float climbDuration = 0.5f;

    [Header("登る高さ")]
    [SerializeField] float climbHeightOffset = 1.5f;

    [Header("上った前に行く距離")]
    [SerializeField] float climbForwardOffset = 0.5f;

    [Header("SphereCastの高さを変える")]
    [SerializeField] float wallCheckHeight;

    //壁に当たった情報
    private RaycastHit frontWallHit;
    //前に壁があるかどうか
    public bool wallFront;
    //実査の壁との角度
    private float wallLookAngle;

    //上っているかどうか
    private bool climbing;

    private bool canClimb;

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

        if(pm.grounded)
        {
            canClimb = true;
        }

        if(wallFront && canClimb && Input.GetKeyDown(Climb) && wallLookAngle < maxWallLookAngle)
        {
            bool spaceAbove = !Physics.Raycast(transform.position + Vector3.up * climbHeightOffset,
                orientaion.forward,
                detectionLength,
                whatIsWall);

            if(spaceAbove)
            {
                canClimb = false;
                StartCoroutine(ClimbLedge());
            }
        }

        Debug.Log(wallFront);
    }

    
    private void WallCheck()
    {
        Vector3 origin = transform.position + Vector3.up * wallCheckHeight;

        RaycastHit hit;

        wallFront = false;

        if(Physics.SphereCast(origin, sphereCastRadius, orientaion.forward, out hit, detectionLength, whatIsWall))
        {
            wallFront = true;

            frontWallHit = hit;
        }

        else
        {

            Collider[] hits = Physics.OverlapSphere(origin + orientaion.forward * 0.3f, sphereCastRadius, whatIsWall);

            if (hits.Length > 0)
            {
                wallFront = true;

                frontWallHit.point = hits[0].ClosestPoint(origin);
                frontWallHit.normal = (origin - frontWallHit.point).normalized;
            }
        }

        if (wallFront)
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

        Vector3 upPos =
            frontWallHit.point +
            frontWallHit.normal * 0.3f +
            Vector3.up * climbHeightOffset;

        float time = 0;

        while (time < climbDuration)
        {
            transform.position = Vector3.Lerp(startPos, upPos, time / climbDuration);

            time += Time.deltaTime;
            yield return null;
        }

       transform.position = upPos;

        yield return new WaitForSeconds(0.1f);

        Vector3 forwarPos =
                transform.position + 
                orientaion.forward * climbForwardOffset;

         time = 0;

        while (time < climbDuration * 0.5f)
        {
            transform.position = Vector3.Lerp(upPos, forwarPos, time / (climbDuration* 0.5f));

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = forwarPos + Vector3.down * 0.1f;

        rb.isKinematic = false;

        yield return new WaitForSeconds(0.3f);
         
        pm.enabled = true;

        climbing = false;
    }

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
