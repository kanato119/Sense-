using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneStage : MonoBehaviour
{
    // 読み込むシーンの管理
    [SerializeField] private string[] SceneNames;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChengeScene(int index)
    {
        // 読み込むシーン
        SceneManager.LoadScene(SceneNames[index]);
        //Debug.Log("ゲームスタート");

    }
}
