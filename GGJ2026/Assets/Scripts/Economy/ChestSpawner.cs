using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private Player player;
    
    [SerializeField] private Chest chestPrefab;

    [SerializeField] private LayerMask mask;
    
    [SerializeField] private float chestSpawnRadius = 20;

    [SerializeField] private int minChestCountInRadius = 5;
    
    private List<Chest> chests;

    private void Awake()
    {
        StartCoroutine(SpawnChestsCo());
        
        
    }

    private void SpawnChests(int chestCountToSpawn)
    {
        for (int i = 0; i < chestCountToSpawn; i++)
        {
            float x = Random.Range(-chestSpawnRadius, chestSpawnRadius);
            float y = Random.Range(-chestSpawnRadius, chestSpawnRadius);
            
            Chest chestObject = Instantiate(chestPrefab, player.transform.position + new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    private IEnumerator SpawnChestsCo()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, chestSpawnRadius, mask);

            if (colliders.Length < minChestCountInRadius)
            {
                int diff = minChestCountInRadius - colliders.Length;
                SpawnChests(Random.Range(diff - 5, diff + 5));
            }
            
            yield return new WaitForSeconds(10);
        }
    }
}
