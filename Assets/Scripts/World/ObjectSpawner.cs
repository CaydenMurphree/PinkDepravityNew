using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn1;
    public GameObject objectToSpawn2;
    public float spawnRate = 2.0f;
    public Vector3 spawnArea = new Vector3(10, 0, 10);
    public int minObjects = 2; // Minimum number of objects in the spawn area
    public int maxObjects = 10; // Maximum number of objects in the spawn area
    public float minDistanceToPlayer = 5.0f; // Minimum distance to spawn objects from the player

    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Check the number of spawned objects and spawn if necessary
            if (CountSpawnedObjects() < maxObjects)
            {
                SpawnRandomObject();
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }

    int CountSpawnedObjects()
    {
        return GameObject.FindGameObjectsWithTag(objectToSpawn1.tag).Length + GameObject.FindGameObjectsWithTag(objectToSpawn2.tag).Length;
    }

    void SpawnRandomObject()
    {
        Vector3 randomSpawnPosition;
        do
        {
            randomSpawnPosition = transform.position + new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 0, Random.Range(-spawnArea.z, spawnArea.z));
        } while (Vector3.Distance(randomSpawnPosition, player.position) < minDistanceToPlayer);

        GameObject spawnedObject;

        // Randomly choose between objectToSpawn1 and objectToSpawn2
        float randomValue = Random.value;
        if (randomValue < 0.5f)
        {
            spawnedObject = Instantiate(objectToSpawn1, randomSpawnPosition, Quaternion.identity);
        }
        else
        {
            spawnedObject = Instantiate(objectToSpawn2, randomSpawnPosition, Quaternion.identity);
        }
    }
}


