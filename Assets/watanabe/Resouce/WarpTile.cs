using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTile : MonoBehaviour
{

    public Transform warpPoint; //プレイヤーのワープ先

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = warpPoint.position;
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
