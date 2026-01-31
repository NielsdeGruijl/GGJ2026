using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private Enemy enemyPrefab;
    
    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private float spawnCooldown;

    private void Awake()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            foreach (Transform spawnTransform in spawnPoints)
            {
                Enemy enemyObject = Instantiate(enemyPrefab, spawnTransform.position.ToVector2(), Quaternion.identity);
                enemyObject.Initialize(player);
            }
            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}
