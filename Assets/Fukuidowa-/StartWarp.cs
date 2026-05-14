using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWarp : MonoBehaviour
{
    [Header("ワープさせたいもの")]
    public GameObject Warptarget;

    [Header("ワープ先")]
    public Transform warpPoint;

    [Header("押すキー")]
    public KeyCode triggerKey = KeyCode.Z;

    // Start is called before the first frame update
    void Update()
    {
        // 指定キーが押された瞬間に実行
        if (Input.GetKeyDown(triggerKey))
        {
         
            SpawnObject();


        }
    }


    void SpawnObject()
    {
        // ワープさせるものを設定されていない時
        if (Warptarget == null)
        {
           
            return;
        }

        // ワープ先が設定されていない時
        if (warpPoint == null)
        {
            
            return;
        }

        // 位置を移動
        Warptarget.transform.position = warpPoint.position;

       
    }
}
