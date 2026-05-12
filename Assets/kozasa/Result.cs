using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    
    public TextMeshProUGUI[] rankTexts; // 5個用意
    const int RANK_COUNT = 5;

    void Start()
    {

       //TimeManager.Instance.SaveTime();



        DisplayRanking();

    }

    //void SaveResult()
    //{

    //    List<float> times = new List<float>();

    //    for (int i = 0; i < RANK_COUNT; i++)
    //    {

    //    }

    //}


    void DisplayRanking()
    {

        int stage = GetStageNumber();

        for (int i = 0; i < RANK_COUNT; i++)
        {
           

            float time = PlayerPrefs.GetFloat("Stage" + stage + "_Rank" + i, Mathf.Infinity);

            Debug.Log("表示:" + time);

            Debug.Log(stage);

            // time = TimeManager.Instance.SaveTime();

            if (time == Mathf.Infinity)
            {

                rankTexts[i].text = (i + 1) + "  : ---";

            }
            else
            {

                rankTexts[i].text = (i + 1) + "  : " + time.ToString("F2") + "s";

            }
        }


    }
    

    public void OnClickReset()
    {

        ResetRanking();
        DisplayRanking();

    }

    void ResetRanking()
    {

        for (int i = 0; i < RANK_COUNT; i++)
        {

            int stage = GetStageNumber();

            PlayerPrefs.SetFloat("Stage" + stage + "_Rank" + i, Mathf.Infinity);

        }

    }

    int GetStageNumber()
    {

        int stage = PlayerPrefs.GetInt("StageIndex", -1);

        if (stage == -1)
        {

            // PlayerPrefsにない場合シーン名から取得
            string SceneName = SceneManager.GetActiveScene().name;
            string number;

            // シーンの名前の番号を取り出す
            number = SceneName.Replace("Stage", "");

            // 数字でない場合0と表示させる
            int.TryParse(number, out stage);

        }

        return stage;

    }
}

