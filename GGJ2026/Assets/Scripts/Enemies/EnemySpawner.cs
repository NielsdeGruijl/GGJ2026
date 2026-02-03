using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private Enemy enemyPrefab;
    
    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private float spawnCooldown;

    [SerializeField] private int baseSpawnCount;
    
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int spawnCount = (int)(baseSpawnCount * DifficultyManager.instance.enemySpawnCountMult);
            int i = 0;
            while (i < spawnCount)
            {
                int randomSpawnpoint = Random.Range(0, spawnPoints.Count);
                Enemy enemyObject = ObjectPool.instance.Get("Enemies").GetComponent<Enemy>();
                enemyObject.transform.position = spawnPoints[randomSpawnpoint].position.ToVector2();
                enemyObject.Initialize(player.transform);

                i++;
                yield return null;
            }

            float cooldown = spawnCooldown / DifficultyManager.instance.enemySpawnIntervalMult;

            if (cooldown <= 0.5f)
                cooldown = 0.5f;

            yield return new WaitForSeconds(cooldown);
        }
    }
}
