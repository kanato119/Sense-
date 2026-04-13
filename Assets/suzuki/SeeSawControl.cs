using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSawControl : MonoBehaviour
{


    [SerializeField] Transform SeeSaw;


    [SerializeField] float MaxX = 40;
    [SerializeField] float MinX = -40;

    [SerializeField] float MaxY = 40;
    [SerializeField] float MinY = -40;

    [SerializeField] float MaxZ = 40;
    [SerializeField] float MinZ = -40;


    // Start is called before the first frame update
    void Start()
    {
        SeeSaw = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ////Ⅹの回転
        //if (SeeSaw.rotation.z < MinX)SeeSaw.Rotate(MinX, 0, 0);
        //if (SeeSaw.rotation.z > MaxX) SeeSaw.Rotate(MaxX, 0, 0);

        ////Yの回転
        //if (SeeSaw.rotation.z < MinY) SeeSaw.Rotate(0, MinY, 0);
        //if (SeeSaw.rotation.z > MaxY) SeeSaw.Rotate(0, MaxY, 0);

        //Zの回転
        //if (SeeSaw.rotation.z < MinZ)SeeSaw.Rotate(0, 0, MinZ);
        //if (SeeSaw.rotation.z > MaxZ)SeeSaw.Rotate(0, 0, MaxZ);

        Debug.Log(SeeSaw.localEulerAngles.z);

        float deg = SeeSaw.localEulerAngles.z;

        if(deg < 0.0f) deg += 360.0f;


        //if ( /*Z軸の角度が-45度より小さくなったら*/ SeeSaw.localEulerAngles.z <= MinZ)
        if( deg <180.0f && 360.0f - MinZ < deg  )
        {
            // Z軸の角度を-45度に設定

            SeeSaw.eulerAngles = new Vector3(0, 0, MinZ);
            
        }
        if ( /*Z軸の角度が-45度より小さくなったら*/ SeeSaw.localEulerAngles.z >= MaxZ)
        {
            // Z軸の角度を-45度に設定

            //SeeSaw.eulerAngles = new Vector3(0, 0, MaxZ);

        }

    }
}
