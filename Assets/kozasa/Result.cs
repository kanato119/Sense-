using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{

    public TextMeshProUGUI[] rankTexts; // 5個用意

    void Start()
    {

        DisplayRanking();

    }

    void DisplayRanking()
    {


        for (int i = 0; i < 5; i++)
        {

            int stage = GetStageNumber();

            float time = PlayerPrefs.GetFloat("Stage" + stage+ "_Rank" + i, Mathf.Infinity);

            if (time == Mathf.Infinity)
            {

                rankTexts[i].text = (i + 1) + "位 : ---";

            }
            else
            {

                rankTexts[i].text = (i + 1) + "位 : " + time.ToString("F2") + "s";

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

        for (int i = 0; i < 5; i++)
        {

            int stage = GetStageNumber();

            PlayerPrefs.SetFloat("Stage" + stage + "_Rank" + i, Mathf.Infinity);

        }

    }

    int GetStageNumber()
    {

        int stage = PlayerPrefs.GetInt("StageIndex", 1);

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

