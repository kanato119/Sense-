using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{

    public TextMeshProUGUI[] rankTexts; // 5個用意

    void Start()
    {

        DisplayRanking();

    }

    void DisplayRanking()
    {
        for (int i = 0; i < 5; i++)
        {
            float time = PlayerPrefs.GetFloat("Rank" + i, Mathf.Infinity);

            if (time == Mathf.Infinity)
            {
                rankTexts[i].text = (i + 1) + "位 : ---";
            }
            else
            {
                rankTexts[i].text = (i + 1) + "位 : " + time.ToString("F2") + "s";
            }
        }
    }
    

    public void OnClickReset()
    {

        ResetRanking();
        DisplayRanking();

    }

    void ResetRanking()
    {

        for (int i = 0; i < 5; i++)
        {

            PlayerPrefs.SetFloat("Rank" + i, Mathf.Infinity);

        }

    }
}

