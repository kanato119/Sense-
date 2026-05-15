using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    
    public TextMeshProUGUI[] rankTexts; // 5個用意
    const int RANK_COUNT = 5;           // ランキング数

    void Start()
    {

       //TimeManager.Instance.SaveTime();

        // ランキング表示
        DisplayRanking();

    }

    // ランキング表示処理
    void DisplayRanking()
    {

        // 現在のステージ番号を取得
        int stage = GetStageNumber();

        // ランキングの数分だけ繰り返す
        for (int i = 0; i < RANK_COUNT; i++)
        {
            // 保存されたタイムを取得
            float time = PlayerPrefs.GetFloat("Stage" + stage + "_Rank" + i, Mathf.Infinity);

            Debug.Log("表示:" + time);

            Debug.Log(stage);

            // データがない場合
            if (time == Mathf.Infinity)
            {
                // 空用の表示
                rankTexts[i].text = (i + 1) + "  : ---";

            }
            // ある場合
            else
            {
                // タイムを表示
                rankTexts[i].text = (i + 1) + "  : " + time.ToString("F2") + "s";

            }

        }

    }

    // リセットボタンを押されたとき
    public void OnClickReset()
    {
        // ランキングの初期化
        ResetRanking();
        // 表示を更新
        DisplayRanking();

    }

    // ランキングのリセット処理
    void ResetRanking()
    {
        // ランキングの回数分繰り返す
        for (int i = 0; i < RANK_COUNT; i++)
        {
            // 現在のステージ番号を取得
            int stage = GetStageNumber();

            // ランキングの初期化
            PlayerPrefs.SetFloat("Stage" + stage + "_Rank" + i, Mathf.Infinity);

        }

    }

    // ステージ数を取得する
    int GetStageNumber()
    {
        // PlayerPrefsからステージ数を取得
        int stage = PlayerPrefs.GetInt("StageIndex", -1);

        // 保存されていない場合
        if (stage == -1)
        {

            // PlayerPrefsにない場合現在のシーン名から取得
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

