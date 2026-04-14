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

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (isPaused)
            {

                Resumegame();


            }
            else
            {

                PauseGame();

            }

        }

        if (isPaused&&Input.GetKeyDown(KeyCode.Space))
        {

            ChengeSceneB changescene = GetComponent<ChengeSceneB>();

            changescene.ChengeScene2();

        }

    }
}
