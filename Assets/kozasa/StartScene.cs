using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1.0f;

        // カーソルを表示させる
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
