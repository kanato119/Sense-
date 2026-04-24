using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ashi : MonoBehaviour
{
    public GameObject pepperPrefab;
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(pepperPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}