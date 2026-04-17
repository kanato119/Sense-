using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public Transform Player;

    public float speed = 5.0f;  //追従アイテムの移動速度
    public float KeepDistance = 2f; //プレイヤーとキーアイテムとの距離

    bool PlayerLockON = false; //追尾
    private Collider keyCollider;  //鍵の当たり判定

    bool PlayerKeyHold = false;

    private void Start()
    {
        keyCollider = GetComponent<Collider>();
    }


    private void Update()
    {
      if(Player == null)
        {
            return;
        }

        if (!PlayerLockON)
        {
            return;
        }


      float distance = Vector3.Distance(transform.position, Player.position);
        if(distance > KeepDistance )
        {

           transform.position = Vector3.MoveTowards(transform.position,
                                                    Player.position,
                                                    speed * Time.deltaTime);


        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            PlayerLockON = true;

            if(keyCollider != null)
            {
                keyCollider.enabled = false;
            }


        }
    }


}
