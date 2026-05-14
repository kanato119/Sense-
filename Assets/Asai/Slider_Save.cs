using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slider_Save : MonoBehaviour
{
    private Slider slider;

    [SerializeField] private PlayerCamera player;

    [SerializeField] private TMP_InputField inputField;

    string sliderKey = "SavedSliderValue";
    // Start is called before the first frame update
    void Start()
    {

        slider = GetComponent<Slider>();

        float savedValue = PlayerPrefs.GetFloat(sliderKey, slider.value);
        slider.value = savedValue;

        inputField.text = slider.value.ToString("0.00");

        slider.onValueChanged.AddListener((value) =>
        {
            inputField.text = value.ToString("0.00");
            SaveSliderValue(value);
            player.SetSensi(value);
        });


        inputField.onEndEdit.AddListener((text) =>
        {
            float value;

            if (float.TryParse(text, out value))
            {
                value = Mathf.Clamp(value, slider.minValue, slider.maxValue);

                slider.value = value;
            }
            else
            {
                inputField.text = slider.value.ToString("0.00");
            }

        });

        player.SetSensi(slider.value);


    }

    // Update is called once per frame
    void Update()
    {

    }

    void SaveSliderValue(float value)
    {
        PlayerPrefs.SetFloat(sliderKey, value);
        PlayerPrefs.Save();
    }
}
