using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouseManager : MonoBehaviour
{

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] private MonoBehaviour camera;

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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // CameraObject.SetActive(false);

        camera.enabled = false;
    }

   public void Resumegame()
    {
        // パネルを非表示にさせてゲームを動かす
        Time.timeScale = 1.0f;
        isPaused = false;
        pauseMenuUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //CameraObject.SetActive(true);

        camera.enabled = true;

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
