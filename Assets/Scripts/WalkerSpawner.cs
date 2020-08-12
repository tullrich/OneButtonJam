using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerSpawner : MonoBehaviour
{
    public Walker walkerPrefab;
    public float spawnRate;
    public int maxSpawnCount;
    public float spawnHalfExtents;
    public float killXDistance;
    public bool moveLeft;
    public float speedVariance;
    float spawnTimer;
    int spawnCount;

    public void OnGameplayStart()
    {
        spawnCount = maxSpawnCount;
    }

    void Update()
    {
        if (spawnCount > 0)
        {
            spawnTimer += Time.deltaTime;
            while (spawnTimer >= spawnRate)
            {
                spawnTimer -= spawnRate;
                spawnCount--;
                SpawnWalker();
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            transform.position + new Vector3(0, -spawnHalfExtents, 0),
            transform.position + new Vector3(0, spawnHalfExtents, 0)
        );
        float dir = moveLeft ? -1 : 1;
        Gizmos.DrawLine(
           transform.position,
           transform.position + new Vector3(dir * killXDistance, 0, 0)
       );
        Gizmos.DrawLine(
           transform.position + new Vector3(dir * killXDistance, -spawnHalfExtents, 0),
           transform.position + new Vector3(dir * killXDistance, spawnHalfExtents, 0)
       );
    }

    void SpawnWalker()
    {
        Walker walker = Instantiate(walkerPrefab);
        walker.MoveLeft = moveLeft;
        walker.killX = transform.position.x + (moveLeft ? -1 : 1) * killXDistance;
        walker.speed += Random.Range(-speedVariance, speedVariance);

        float yPos = Random.Range(-spawnHalfExtents, spawnHalfExtents);
        walker.transform.position = transform.position + new Vector3(0, yPos, 0);
    }
}
