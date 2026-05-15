using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PouseManager : MonoBehaviour
{

    [SerializeField]public GameObject pauseMenuUI;
    [SerializeField] private MonoBehaviour CameraController;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
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

        GameObject PoseObj =
        GameObject.Find("Pause");

        if (PoseObj != null)
        {
            pauseMenuUI = PoseObj;

            Debug.Log("PauseUI取得成功");
        }
        else
        {
            Debug.Log("PauseUIが見つからない");
        
        }


    }

    // ポーズするときの処理
    public void PauseGame()
    {

        Debug.Log(pauseMenuUI);
        // ゲームを止める
        Time.timeScale = 0f;
        isPaused = true;
        // パネルを表示
        pauseMenuUI.SetActive(true);

        // カーソルを表示させてカーソルロックを解除する
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // カメラの操作を停止させる
        CameraController.enabled = false;

        Debug.Log("ポーズ");

    }

    // ポーズを解除するときの処理
   public void ResumeGame()
    {
        // ゲームを動かす
        Time.timeScale = 1.0f;
        isPaused = false;
    　　// パネルを非表示にさせる
        pauseMenuUI.SetActive(false);

        // カーソルの非表示にしてカーソルをロックする
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        CameraController.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        // ESCキーを押したらポーズ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ポーズでないときポーズする
            if (!isPaused)
            {

                PauseGame();

            }

        }

    }
}
