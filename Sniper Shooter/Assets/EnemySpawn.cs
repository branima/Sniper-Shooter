using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public string prefabName;
    public float spawnRatePerSec;
    public int numberOfEnemies;

    float lastSpawnTime;

    public Transform enemyMovementTarget;
    Transform instance;

    void Start()
    {
        lastSpawnTime = float.MinValue;
    }
    void Update()
    {
        if (Time.time - lastSpawnTime > 1f / spawnRatePerSec)
        {
            instance = ObjectPooler.Instance.SpawnFromPool(prefabName).transform;
            instance.LookAt(enemyMovementTarget);
            instance.GetComponent<EnemyMovement>().SetTarget(enemyMovementTarget);
            lastSpawnTime = Time.time;
        }
    }
}
