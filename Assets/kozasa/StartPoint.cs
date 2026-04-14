using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;
    private static bool spawned = false;

    private void Awake()
    {

        spawned = false;

    }

    // Start is called before the first frame update
    void Start()
    {

        if(!spawned)
        {

            Instantiate(playerPrefab,transform.position,Quaternion.identity);
            spawned = true;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
