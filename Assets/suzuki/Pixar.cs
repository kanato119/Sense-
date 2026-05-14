using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixar : MonoBehaviour
{

    [SerializeField] GameObject objects;

    [SerializeField] int Num;

    private bool hoge;

    private void Start()
    {
        Num = 0;

        hoge = false;
    }

    private void Update()
    {

        if (Num >= 4)
        {
            objects.AddComponent<Rigidbody>();
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            //プレイヤーが一定回数触れたら落ちる
            if (hoge) return;

            hoge = true;

            objects.transform.position += Vector3.down * 10 * Time.deltaTime;

            Num++;

            hoge = false;
        }
    }
}
