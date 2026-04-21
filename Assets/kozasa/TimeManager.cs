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


    // Start is called before the first frame update
    void Start()
    {

        // ベストタイムを画面に表示する
       // DisplayBestTime();
        
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

        SaveTime();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            StopTimer();

        }

    }

    private void SaveTime()
    {
        // ベストタイム
        float bestTime = PlayerPrefs.GetFloat("BestTime", Mathf.Infinity);

        if (resultTime < bestTime)
        {

            PlayerPrefs.SetFloat("BestTime", resultTime);

        }

        DisplayBestTime();

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

        List<float>ranking=new List<float>();

        for(int i = 0; i < 5; i++)
        {

            float time = PlayerPrefs.GetFloat("Rank" + i, Mathf.Infinity);
            ranking.Add(time);
        }

        // 今回のタイム追加
        ranking.Add(resultTime);

        // 小さい順に並び替え（速いほど上）
        ranking.Sort();

        // 上位5つ保存
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat("Rank" + i, ranking[i]);
        }

    }

}
