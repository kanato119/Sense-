using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{

    [SerializeField] GameObject StageSelectUI;
    [SerializeField] GameObject CheckPanelUI;
    private bool isStageSelect = false;
    private bool isCheck=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 選択画面を開く
  public void Select()
    {

        // ステージ選択状態にする
        isStageSelect=true;
        isCheck = false;
        // パネルを開く
        StageSelectUI.SetActive(true);

        // カーソルを表示させてカーソルロックを解除する
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    // 選択画面を閉じる
    public void NoSelect()
    {

        // 選択状態を解除する
        isStageSelect = false;
       // パネルを閉じる
        StageSelectUI.SetActive(false);

        Debug.Log("false");

    }

    // ゲーム修了確認画面を開く
    public void EndCheck()
    {

        isCheck = true;
        // パネルを表示
        CheckPanelUI.SetActive(true);

    }

    // ゲーム修了確認画面を閉じる
    public void NoEnd()
    {

        isCheck = false;

        // パネルを閉じる
        CheckPanelUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ゲームを終了させる
    public void GameEnd()
    {
#if UNITY_EDITOR

        // Unityエディタ上なら再生停止
        UnityEditor.EditorApplication.isPlaying = false;
#else
        
        // ビルド版ならゲーム終了
        Application.Quit();
#endif
    }

}

