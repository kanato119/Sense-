using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{

    [SerializeField] GameObject StageSelectUI;
    private bool isStageSelect = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

  public void Select()
    {

        isStageSelect=true;
        StageSelectUI.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void NoSelect()
    {

        isStageSelect = false;
        StageSelectUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

   

    }

    // ゲームを終了させる
    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        
        Application.Quit();
#endif
    }

}

