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

        if (isTimer)
        {

            currentTime += Time.deltaTime;

            currentTimeText.text = "Time : " + currentTime.ToString("F2") + "s";

        }

    }

    public void StopTimer()
    {

        isTimer = false;

        if (!isTimer)
        {

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

        float bestTime = PlayerPrefs.GetFloat("BestTime", Mathf.Infinity);

        if (resultTime < bestTime)
        {

            PlayerPrefs.SetFloat("BestTime", resultTime);

        }

        DisplayBestTime();

    }

    private void DisplayBestTime()
    {

        float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);

        if (bestTime == Mathf.Infinity) return;

        bestTimeText.text="Best : "+bestTime.ToString("F2") + "s";

    }
}
