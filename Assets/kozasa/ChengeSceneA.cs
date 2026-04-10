using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChengeSceneA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChengeScene()
    {
        // 読み込むシーン
        SceneManager.LoadScene("GameScene");
        Debug.Log("ゲームスタート");

    }
}
