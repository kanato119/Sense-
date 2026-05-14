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

        // まだInstanceが存在しない場合
        if (Instance == null)
        {

            // 登録
            Instance = this;

            // シーンを切り替えても消えないようにする
            DontDestroyOnLoad(gameObject);

        }
        else {

            // すでに存在していたら重複しないようにする
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

        // "Time"という名前のオブジェクトを探す
        GameObject obj = GameObject.Find("Time");

        // 見つかったか確認
        Debug.Log(obj);


    }

    private void OnEnable()
    {

        // シーン読み込み
        SceneManager.sceneLoaded += OnSceneLoaded;

    }


    private void OnDisable()
    {

        // イベント解除
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    // シーン読み込み後に呼ぶ
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        // "Time"オブジェクトを取得
        GameObject currentObj =
        GameObject.Find("Time");

        // 見つかったら
        if (currentObj != null)
        {

            // 子オブジェクトのTextMeshProUGUIを取得
            currentTimeText =
                currentObj.GetComponentInChildren<TextMeshProUGUI>();


            Debug.Log(currentTimeText);

        }

        // "BestTime"オブジェクト取得
        GameObject BestObj =
        GameObject.Find("BestTime");

        // 見つかったら
        if (BestObj != null)
        {

            // 子オブジェクトのTextMeshProUGUIを取得
            bestTimeText =
                BestObj.GetComponentInChildren<TextMeshProUGUI>();


            Debug.Log(currentTimeText);
        }

        // 見つかったら
        if (bestTimeText != null)
        {
            DisplayBestTime();
        }

        if (scene.name.StartsWith("Stage"))
        {

            // タイマー初期化
            currentTime = 0f;
            resultTime = 0f;

            // タイマー開始
            isTimer = true;

            // UIが存在するなら表示リセット
            if (currentTimeText != null)
            {

                currentTimeText.text = "Time : 0.00s";

            }
        }
        else
        {

            // ステージ以外ではタイマー停止
            isTimer = false;

        }

    }

    // Update is called once per frame
    void Update()
    {

        // タイマーが動いている場合
        if (isTimer)
        {

            // 経過時間を加算
            currentTime += Time.deltaTime;

            // UI更新
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

        // ランキングとタイムの保存
        SaveTime();
        SaveRanking();

    }

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

        // UIが存在しない場合
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

        // 現在のステージ番号取得
        int stage = PlayerPrefs.GetInt("StageIndex", 1);

        List<float>ranking=new List<float>();

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
