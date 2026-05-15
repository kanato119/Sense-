using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivation : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Transform spawnPoint;

    public float cooldown = 5f;

    private bool canTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!canTrigger) return;

        if (other.CompareTag("Player"))
        {
            canTrigger = false; // ←ここで先にロックする
            StartCoroutine(SpawnAndCooldown());
        }
    }

    IEnumerator SpawnAndCooldown()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(meteorPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(cooldown);

        canTrigger = true;
    }
}