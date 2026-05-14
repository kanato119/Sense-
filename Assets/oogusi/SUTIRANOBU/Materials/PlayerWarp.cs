using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : MonoBehaviour
{
    public Transform startPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.position = startPoint.position;
        }
    }
}