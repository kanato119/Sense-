using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerAdjustment : MonoBehaviour
{
    public float barLength = 4f;//長さ
    public float barThickness = 0.5f;//太さ


    // Start is called before the first frame update
    void Start()
    {
        AdjustBar();
    }

    // Update is called once per frame
    void Update()
    {
        AdjustBar();
    }

    private void AdjustBar()
    {

        transform.localScale = new Vector3 (barThickness, barLength, barThickness);
        transform.localPosition = new Vector3(0f, -barLength / 2f, 0f);


    }

}
