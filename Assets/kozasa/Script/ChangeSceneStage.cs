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

        SceneManager.LoadScene(SceneNames[index]);
       // StartCoroutine(Change(index));

    }

    //IEnumerator Change(int index)
  //  {
        //Debug.Log("① Change開始");

        //yield return StartCoroutine(FadeManager.instance.FadeOut());

        //Debug.Log("② Fade終了");

        //// シーン名チェック
        //if (SceneNames == null || SceneNames.Length <= index || string.IsNullOrEmpty(SceneNames[index]))
        //{

        //    Debug.LogError("シーン名が設定されていない");
        //    yield break;

        //}

        //    Loadingmanager.nextScene = SceneNames[index];
        //// 読み込むシーン

        // Debug.Log("③ nextSceneセット: " + SceneNames[index]);

        //SceneManager.LoadScene(SceneNames[index]);


   // }
}
