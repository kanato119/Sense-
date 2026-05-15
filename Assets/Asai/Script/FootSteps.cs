using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [Header("足音")]
    [SerializeField] AudioSource f1;
    [SerializeField] AudioSource f2;
    [SerializeField] AudioSource f3;
    [SerializeField] AudioSource f4;

    [Header("足音状態")]
    [SerializeField] bool isStepping;

    [Header("再生する音番号")]    
    [SerializeField] int soundNumber;

    [Header("Player")]
    [SerializeField] private PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //WASD入力判定
        bool isMove = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        
        //移動中かつ地面にいる場合
        if (isMove && player.grounded)
        {
            //足音再生中でなければ
            if(isStepping == false)
            {
                isStepping = true;

                //ランダムで音選択
                soundNumber = Random.Range(1,5);

                //足音再生開始
                StartCoroutine(Footstep());
            }
        }
    }

    IEnumerator Footstep()
    {
        //1番の音
        if(soundNumber ==1)
        {
            f1.Play();
        }

        //2番の音
        if (soundNumber == 2)
        {
            f2.Play();
        }

        //3番の音
        if (soundNumber == 3)
        {
            f3.Play();
        }

        //4番の音
        if (soundNumber == 4)
        {
            f4.Play();
        }

        //LeftShift中は走り状態
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //足音感覚短め
            yield return new WaitForSeconds(0.35f);
        }
        else
        {
            //通常移動時
            yield return new WaitForSeconds(0.48f);
        }

        //次の足音を再生可能にする
        isStepping = false;
    }
}
