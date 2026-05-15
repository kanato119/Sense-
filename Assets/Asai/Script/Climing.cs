using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climing : MonoBehaviour
{
    [Header("プレイヤーのカメラの向き")]
    [SerializeField] Transform orientaion;

    [Header("Player")]
    [SerializeField] Rigidbody rb;
    
    [Header("Playerスクリプト")]
    [SerializeField] PlayerMovement pm;

    //アニメーション
    Animator animator;

    [Header("壁として判定")]
    [SerializeField] LayerMask whatIsWall;

    [Header("壁検出の距離")]
    [SerializeField] float detectionLength;

    [Header("SphereCastの半径")]
    [SerializeField] float sphereCastRadius;

    [Header("壁に足して許容する角度")]
    [SerializeField] float maxWallLookAngle;

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

    //登れるか状態
    private bool canClimb;

    void Start()
    {
        //Rigidbody取得
        rb = GetComponent<Rigidbody>();

        //Animator取得
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        //登っているときの処理
        if (climbing) return;

        //壁の検出
        WallCheck();

        //地面にいるときだけ再度登れる
        if(pm.grounded)
        {
            canClimb = true;
        }

        //条件を満たしたらクライミング開始
        if(wallFront && canClimb && Input.GetKeyDown(Climb) && wallLookAngle < maxWallLookAngle)
        {
            //上にスペースがあるか確認
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
        //判定開始位置
        Vector3 origin = transform.position + Vector3.up * wallCheckHeight;

        RaycastHit hit;

        //初期化
        wallFront = false;

        //SphereCastで壁判定
        if (Physics.SphereCast(origin, sphereCastRadius, orientaion.forward, out hit, detectionLength, whatIsWall))
        {
            wallFront = true;

            frontWallHit = hit;
        }

        else
        {
            //近距離補助判定
            Collider[] hits = Physics.OverlapSphere(origin + orientaion.forward * 0.3f, sphereCastRadius, whatIsWall);

            if (hits.Length > 0)
            {
                wallFront = true;

                //一番近い位置所得
                frontWallHit.point = hits[0].ClosestPoint(origin);
                
                //壁法線を作成
                frontWallHit.normal = (origin - frontWallHit.point).normalized;
            }
        }

        //壁との角度計算
        if (wallFront)
        {
            wallLookAngle = Vector3.Angle(orientaion.forward, -frontWallHit.normal);
        }

    }
    
     IEnumerator ClimbLedge()
    {
        climbing = true;

        //プレイヤー操作停止
        pm.enabled = false;

        //Rigidbody停止
        rb.isKinematic = true;

        //アニメーションを再生
        animator.SetTrigger("Climb");

        //開始位置
        Vector3 startPos = transform.position;

        //上に移動する位置
        Vector3 upPos =
            frontWallHit.point +
            frontWallHit.normal * 0.3f +
            Vector3.up * climbHeightOffset;

        float time = 0;

        //上方向へ移動
        while (time < climbDuration)
        {
            transform.position = Vector3.Lerp(startPos, upPos, time / climbDuration);

            time += Time.deltaTime;
            yield return null;
        }

       transform.position = upPos;

        yield return new WaitForSeconds(0.1f);

        //前方向移動位置
        Vector3 forwarPos =
                transform.position + 
                orientaion.forward * climbForwardOffset;

         time = 0;

        //前へ移動
        while (time < climbDuration * 0.5f)
        {
            transform.position = Vector3.Lerp(upPos, forwarPos, time / (climbDuration* 0.5f));

            time += Time.deltaTime;
            yield return null;
        }

        //少し下げて着地
        transform.position = forwarPos + Vector3.down * 0.1f;

        rb.isKinematic = false;

        yield return new WaitForSeconds(0.3f);
        
        //プレイヤー操作再開
        pm.enabled = true;

        climbing = false;
    }

    private void OnDrawGizmos()
    {
        if (orientaion == null) return;

        //壁があるときは縁、ないときは赤
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
