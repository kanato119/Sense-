using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouseManager : MonoBehaviour
{

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] private MonoBehaviour CameraController;

    // [SerializeField] GameObject CameraObject;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {


    }

    public void PauseGame()
    {
        // パネルを表示させてゲームを止める
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuUI.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // CameraObject.SetActive(false);

        CameraController.enabled = false;

        Debug.Log("ポーズ");

    }

   public void Resumegame()
    {
        // パネルを非表示にさせてゲームを動かす
        Time.timeScale = 1.0f;
        isPaused = false;
        pauseMenuUI.SetActive(false);

        // カーソルの非表示
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        //CameraObject.SetActive(true);

        CameraController.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        // ESCキーを押したらポーズ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // すでにポーズだったら
            if (isPaused)
            {

                Resumegame();


            }
            // ポーズでないとき
            else
            {

                PauseGame();

            }

        }

        // スペースを押したらタイトル画面に戻る(仮)
        if (isPaused&&Input.GetKeyDown(KeyCode.Space))
        {

            ChengeSceneB changescene = GetComponent<ChengeSceneB>();

            changescene.ChengeScene2();

        }

        Debug.Log(Time.timeScale);

    }
}
