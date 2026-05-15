using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCamera : MonoBehaviour
{

    [SerializeField] private Camera targetCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // カメラをポーズを押したときの座標で固定
        Vector3 pos = targetCamera.transform.position;
       // Debug.Log(pos);

    }
}
