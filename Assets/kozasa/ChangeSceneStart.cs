using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChengeScene2()
    {
        // 読み込むシーン
        //Loadingmanager.nextScene = "StartScene";
        SceneManager.LoadScene("StartScene");
        Debug.Log("スタートシーン");

    }
}
