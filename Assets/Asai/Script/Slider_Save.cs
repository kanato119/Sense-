using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slider_Save : MonoBehaviour
{
    //Sliderコンポーネント
    private Slider slider;

    [Header("PlayerCamera")]
    [SerializeField] private PlayerCamera player;

    [Header("感度入力")]
    [SerializeField] private TMP_InputField inputField;

    //保存キー
    string sliderKey = "SavedSliderValue";
    
    // Start is called before the first frame update
    void Start()
    {
        //Slider取得
        slider = GetComponent<Slider>();

        //保存されている値を取得
        float savedValue = PlayerPrefs.GetFloat(sliderKey, slider.value);
        
        //Sliderに反映
        slider.value = savedValue;

        //InputFieldへ表示
        inputField.text = slider.value.ToString("0.00");

        //Sliderの値変更時
        slider.onValueChanged.AddListener((value) =>
        {
            //InputField更新
            inputField.text = value.ToString("0.00");

            //値保存
            SaveSliderValue(value);

            //感度変更
            player.SetSensi(value);
        });

        //InputField編集終了時
        inputField.onEndEdit.AddListener((text) =>
        {
            float value;

            //数値変更できた場合
            if (float.TryParse(text, out value))
            {
                //最小値～最大値に制限
                value = Mathf.Clamp(value, slider.minValue, slider.maxValue);

                //Sliderへ反映
                slider.value = value;
            }
            else
            {
                //数値じゃなかった場合は元に戻す
                inputField.text = slider.value.ToString("0.00");
            }

        });

        //起動時にも感度適用
        player.SetSensi(slider.value);


    }

    // Update is called once per frame
    void Update()
    {

    }

    //Sliderの値を保存
    void SaveSliderValue(float value)
    {
        //値保存
        PlayerPrefs.SetFloat(sliderKey, value);
        
        //即時保存
        PlayerPrefs.Save();
    }
}
