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

    public static TimeManager Instance { get; private set; }

    // [SerializeField] GameObject CameraObject;
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


    public void PauseGame()
    {

        Debug.Log(pauseMenuUI);
        // パネルを表示させてゲームを止める
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuUI.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        CameraController.enabled = false;

        Debug.Log("ポーズ");

    }

   public void ResumeGame()
    {
        // パネルを非表示にさせてゲームを動かす
        Time.timeScale = 1.0f;
        isPaused = false;
        pauseMenuUI.SetActive(false);

        // カーソルの非表示
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
            // ポーズでないとき
            if (!isPaused)
            {

                PauseGame();

            }

        }

    }
}
