using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Pendulum : MonoBehaviour
{
    public float speed = 3.0f;//振り子の速度
    public float limit = 60f;

    public bool randomStart = false;
    private float random = 0;

    void Awake()
    {
        if (randomStart)
            random = Random.Range(0f, 1f);
    }

    private void Update()
    {
        float angle = limit * Mathf.Sin(Time.time + random * speed);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }


}
