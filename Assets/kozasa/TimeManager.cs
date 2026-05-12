using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{

    public float currentTime = 0f; // 現在のタイム
    public float resultTime= 0f ;    // ゴールした時のタイム
    public bool isTimer = true;    // タイマーのオンオフ
    public TextMeshProUGUI currentTimeText; // 現在のタイムの表示UI
    public TextMeshProUGUI bestTimeText;    // ベストタイム表示UI
    public static TimeManager Instance {  get; private set; }


    // Start is called before the first frame update
    void Awake()
    {

        if (Instance == null)
        {

            Instance = this;

            DontDestroyOnLoad(gameObject);



        }
        else {

            Destroy(gameObject);

        }

        /*/if (Instance != null && Instance != this)
        //{
        //
        //    Destroy(gameObject);
        //    return;
        //
        //}
        //
        //
        // ベストタイムを画面に表示する
        // DisplayBestTime();
        //
        // インスタンスを登録
        //Instance = this;
        //
        //DontDestroyOnLoad(this.gameObject);
        //
        //if (currentTimeText == null)
        //{
        //    currentTimeText=GetComponent<TextMeshProUGUI>();
        //
        /}*/

        GameObject obj = GameObject.Find("Time");

        Debug.Log(obj);


    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        GameObject currentObj =
        GameObject.Find("Time");

        if (currentObj != null)
        {
            currentTimeText =
                currentObj.GetComponentInChildren<TextMeshProUGUI>();

            Debug.Log(currentTimeText);
        }



        //Debug.Log(currentObj);

        // Debug.Log(currentObj.GetComponentInChildren<TextMeshProUGUI>());

        GameObject BestObj =
        GameObject.Find("BestTime");

        if (BestObj != null)
        {
            bestTimeText =
                BestObj.GetComponentInChildren<TextMeshProUGUI>();

            Debug.Log(currentTimeText);
        }

        if (bestTimeText != null)
        {
            DisplayBestTime();
        }

        if (scene.name.StartsWith("Stage"))
        {

            currentTime = 0f;
            resultTime = 0f;
            isTimer = true;

            if (currentTimeText != null)
            {
                currentTimeText.text = "Time : 0.00s";
            }
        }
        else
        {
            isTimer = false;
        }

        /*/currentTimeText =
        //    GameObject.Find("Time")
        //    .GetComponentInChildren<TextMeshProUGUI>();

        //bestTimeText =
        //   GameObject.Find("BestTime")
        //   .GetComponentInChildren<TextMeshProUGUI>();

        //DisplayBestTime();*/
    }
    // Update is called once per frame
    void Update()
    {


        // タイマーを進める
        if (isTimer)
        {

            currentTime += Time.deltaTime;
            
            currentTimeText.text = "Time : " + currentTime.ToString("F2") + "s";

        }

    }

    public void StopTimer()
    {
        // falseになったらタイマーを止める
        isTimer = false;

        if (!isTimer)
        {
            // 現在のタイムを代入
            resultTime = currentTime;

        }

        Debug.Log(resultTime);

        SaveTime();
        SaveRanking();

    }

    /*/private void OnTriggerEnter(Collider other)
    //{

    //    if (other.CompareTag("Player"))
    //    {

    //        Debug.Log("Goal");

    //        StopTimer();

    //    }

    //}*/

    

public float SaveTime()
    {
        // ベストタイム
        float bestTime = PlayerPrefs.GetFloat("BestTime", Mathf.Infinity);

        if (resultTime < bestTime)
        {

            PlayerPrefs.SetFloat("BestTime", resultTime);

        }

        DisplayBestTime();


        return bestTime;
    }

    private void DisplayBestTime()
    {
        // 画面に表示

        if (bestTimeText == null)
        {
            Debug.Log("bestTimeText が null");
            return;
        }

        float bestTime =
            PlayerPrefs.GetFloat("BestTime", 0f);

        if (bestTime == Mathf.Infinity) return;

        bestTimeText.text =
            "Best : " + bestTime.ToString("F2") + "s";
    }

    void SaveRanking()
    {

        int stage = PlayerPrefs.GetInt("StageIndex", 1);

        List<float>ranking=new List<float>();

           // TextMeshProUGUI currentTime = GameObject.Find("CurrentTime").GetComponent<TextMeshProUGUI>();

           // TextMeshProUGUI bestTime = GameObject.Find("BestTime").GetComponent<TextMeshProUGUI>();




        for (int i = 0; i < 5; i++)
        {

            float time = PlayerPrefs.GetFloat("Stage" + stage + "_Rank" + i, Mathf.Infinity);
            ranking.Add(time);
        }

        // 今回のタイム追加
        ranking.Add(resultTime);

        // 小さい順に並び替え（速いほど上）
        ranking.Sort();

        // 上位5つ保存
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat("Stage" + stage + "_Rank" + i, ranking[i]);

            Debug.Log("保存"+ranking[i]);

        }

        Debug.Log(stage);


        PlayerPrefs.Save();

    }

}
