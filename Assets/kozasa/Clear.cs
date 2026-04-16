using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            Goal();

        }

    }

   private void Goal()
    {


        // カーソルの非表示
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("ゴール");
        Time.timeScale = 0f;

    }
}
