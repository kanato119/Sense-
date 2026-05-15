using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI StageText;

    // Start is called before the first frame update
    void Start()
    {
        int stage = PlayerPrefs.GetInt("StageIndex", -1);
        string number;

        if (stage == -1)
        {
            // 保存されてない → シーン名から取得
            string SceneName = SceneManager.GetActiveScene().name;
            // シーンの名前の番号を取り出す
            number = SceneName.Replace("Stage", "");
        }
        else
        {

           number=stage.ToString();

        }

        StageText.text = " Stage " + number;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
