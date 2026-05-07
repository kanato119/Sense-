using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] AudioSource f1;
    [SerializeField] AudioSource f2;
    [SerializeField] AudioSource f3;
    [SerializeField] AudioSource f4;

    [SerializeField] bool isStepping;

    [SerializeField] int soundNumber;

    [SerializeField] private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isMove = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        if (isMove && player.grounded)
        {
            if(isStepping == false)
            {
                isStepping = true;

                soundNumber = Random.Range(1,5);

                StartCoroutine(Footstep());
            }
        }
    }

    IEnumerator Footstep()
    {
        if(soundNumber ==1)
        {
            f1.Play();
        }

        if (soundNumber == 2)
        {
            f2.Play();
        }
        if (soundNumber == 3)
        {
            f3.Play();
        }
        if (soundNumber == 4)
        {
            f4.Play();
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            yield return new WaitForSeconds(0.35f);
        }
        else
        {
            yield return new WaitForSeconds(0.48f);
        }


        isStepping = false;
    }
}
