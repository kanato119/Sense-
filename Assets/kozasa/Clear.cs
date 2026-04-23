using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    [SerializeField] TimeManager timer;

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
        timer.StopTimer();
        Time.timeScale = 0f;
        SceneManager.LoadScene("ResultScene");


    }
}
