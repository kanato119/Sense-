using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWarp : MonoBehaviour
{
    [Header("生成するプレハブ")]
    public GameObject prefab;

    [Header("生成位置")]
    public Transform spawnPoint;

    [Header("押すキー")]
    public KeyCode triggerKey = KeyCode.Z;

    // Start is called before the first frame update
    void Update()
    {
        // 指定キーが押された瞬間に実行
        if (Input.GetKeyDown(triggerKey))
        {
        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
            //SpawnObject();


            //Destroy(this.prefab);
        }
    }


    void SpawnObject()
    {
        if (prefab == null)
        {
            Debug.LogWarning("Prefab が設定されていません。");
            return;
        }

        // 生成位置が未設定なら自分の位置を使う
        Quaternion rotation = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;

        Instantiate(prefab, position, rotation);
        Debug.Log($"{triggerKey} が押され、オブジェクトを生成しました。");
    }
}
