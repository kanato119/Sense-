using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

        // ベストタイムを画面に表示する
        // DisplayBestTime();

        // インスタンスを登録
        Instance = this;

        DontDestroyOnLoad(this.gameObject);

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

    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.CompareTag("Player"))
    //    {

    //        Debug.Log("Goal");

    //        StopTimer();

    //    }

    //}

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

        float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);

        if (bestTime == Mathf.Infinity) return;

        bestTimeText.text="Best : "+bestTime.ToString("F2") + "s";

    }

    void SaveRanking()
    {

        int stage = PlayerPrefs.GetInt("StageIndex", 1);

        List<float>ranking=new List<float>();

        for(int i = 0; i < 5; i++)
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
