using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{

    public static FadeManager instance;

    [SerializeField] private Image fadeImage;
    [SerializeField] private float speed = 2f;

    void Awake()
    {

        instance = this;

    }

    public IEnumerator FadeOut()
    {

        float a = 0;
        while ( a < 1 )
        {

            a+= Time.deltaTime*speed;
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;

        }

    }

    public IEnumerator FadeIn()
    {

        float a = 1;
        while ( a > 0 )
        {

            a -= Time.deltaTime * speed;
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
