using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Transform[] spawnPoints;

    public float interval = 0.1f;
    public int count = 5;
    public float cooldown = 5f;

    private bool canSpawn = true;

    public void StartMeteorRain()
    {
        if (!canSpawn) return;

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        canSpawn = false;

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Instantiate(meteorPrefab, spawnPoints[index].position, spawnPoints[index].rotation);

            yield return new WaitForSeconds(interval);
        }

        yield return new WaitForSeconds(cooldown);

        canSpawn = true;
    }
}