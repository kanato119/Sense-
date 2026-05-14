using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BuletPrefab : MonoBehaviour
{ 
    [SerializeField] float KnockBackForce;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();

            if (rb != null)
            {
                //このオブジェクトとプレイヤーのあたった角度
                Vector3 dir = rb.transform.position - transform.position;
                dir.y = 0;
                dir = dir.normalized;

                //吹っ飛ばす力の計算
                Vector3 force = dir * KnockBackForce;
                force.y = 10.0f;

                rb.AddForce(force, ForceMode.Impulse);
                //プレイヤーに当たっても消す
                Destroy(gameObject);
            }
        }
        //何かにあたったらこのオブジェクトはけす
        else if (!other.CompareTag("ArrowTag"))
        {
            Destroy(gameObject);
        }
    }
}