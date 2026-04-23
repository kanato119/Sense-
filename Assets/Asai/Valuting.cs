using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuting : MonoBehaviour
{
    private int vaultLayer;
    public Camera cam;
    private float playerHeight = 2f;
    private float playerRadius = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        vaultLayer = LayerMask.NameToLayer("VaultLayer");
        vaultLayer = ~vaultLayer;
    }

    // Update is called once per frame
    void Update()
    {
        Vault();
    }

    private void Vault()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics.Raycast(cam.transform.position,cam.transform.forward,out var firsthit,1f,vaultLayer))
            {
                if(Physics.Raycast(firsthit.point + (cam.transform.forward * playerRadius) + (Vector3.up * 0.6f * playerHeight),Vector3.down,out var secondHit,playerHeight))
                {
                    StartCoroutine(LerpVault(secondHit.point, 0.5f));
                }
            }
        }
    }

    IEnumerator LerpVault(Vector3 targetPosition,float duration)
    {
        float time = 0;
        Vector3 startPosion = transform.position;

        while(time < duration)
        {
            transform.position = Vector3.Lerp(startPosion, targetPosition,time / duration);
            time += Time.deltaTime;

            yield return null;
        }

        transform.position = targetPosition;
    }
}
