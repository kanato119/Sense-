using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loadingmanager : MonoBehaviour
{

    public static string nextScene;

    [SerializeField] private Slider slider;
    [SerializeField] private GameObject sliderObject;

    void Awake()
    {
        Debug.Log("現在のシーン: " + SceneManager.GetActiveScene().name);
    }

    // Start is called before the first frame update
    void Start()
    {

        if (string.IsNullOrEmpty(nextScene))
        {

            Debug.Log("nextScene未設定");
            return;

        }

        Debug.Log("nextScene = " + nextScene);
        Debug.Log("timeScale: " + Time.timeScale);
        StartCoroutine(LoadNextScene());

      

    }

    IEnumerator LoadNextScene()
    {

        Debug.Log("① LoadNextScene開始");
        Debug.Log("② 非同期ロード開始");


        //yield return StartCoroutine(FadeManager.instance.FadeIn());

        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);
        async.allowSceneActivation = false;

        // 最低表示時間
        float minTime = 2.0f;
        float timer = 0f;

        while (async.progress<0.9f||timer<minTime)
        {

            timer += Time.deltaTime;
            Debug.Log("progress: " +async.progress);

            float progess = Mathf.Clamp01(async.progress / 0.9f);

            if (slider != null)
                slider.value = progess;

            Debug.Log("④ シーン切り替え");

            yield return null; 
        
        }

        if (sliderObject != null)
        {

            sliderObject.SetActive(false);

        }

        Debug.Log("③ フェードアウト前");
        yield return StartCoroutine(FadeManager.instance.FadeIn());

        async.allowSceneActivation = true;

    }

    // Update is called once per frame
    void Update()
    {

      

    }
}
