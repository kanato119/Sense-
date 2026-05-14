using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    // [SerializeField] TimeManager timer;
    [SerializeField] int StageNumber;

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
        PlayerPrefs.SetInt("StageIndex", StageNumber);
        TimeManager.Instance.StopTimer();
        SceneManager.LoadScene("ResultScene");
 
    }
}
