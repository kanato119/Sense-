using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour
{

    public AudioSource audioSource; // オーディオソースをここに設定

    private bool isButtonHovered = false;// カーソルがのってるかのフラグ


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0)&& !isButtonHovered)
        {

            if (!audioSource.isPlaying)
            {

                audioSource.Play();

            }

        }

    }

    // ボタンにマウスが乗った時に呼ばれる
    public void OnPointerEnter()
    {

        isButtonHovered = true;

    }

    // ボタンからマウスが離れた時に呼ばれる
    public void OnPointerExit()
    {

        isButtonHovered = false;

    }

}
