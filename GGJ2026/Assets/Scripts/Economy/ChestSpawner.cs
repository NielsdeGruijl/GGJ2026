using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[Serializable]
public class ValueEvent : UnityEvent<float>
{
}

[Serializable]
public struct ChestData
{
    public Chest chestPrefab;
    public float basePrice;
    public float flatPriceIncrease;
    public float multPriceIncrease;
}

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private List<Chest> chestPrefabs;
    [SerializeField] private LayerMask layerMask;
    
    [SerializeField] private float chestSpawnRadius = 20;
    [SerializeField] private int minChestCountInRadius = 5;
    
    private List<Chest> chests;

    private WaitForSeconds waitForSpawnInterval;
    private IntValueEvent OnPricesIncreased = new();

    private int chestsOpened = 0;
    
    private float totalWeight;

    private void Awake()
    {
        waitForSpawnInterval = new WaitForSeconds(10);

        foreach (Chest chest in chestPrefabs)
        {
            totalWeight += chest.data.spawnChance;
        }
        
        StartCoroutine(SpawnChestsCo());
    }

    private void IncreasePrices()
    {
        chestsOpened++;
        OnPricesIncreased.Invoke(chestsOpened);
    }
    
    private void SpawnChests(int chestCountToSpawn)
    {
        for (int i = 0; i < chestCountToSpawn; i++)
        {
            float x = Random.Range(-chestSpawnRadius, chestSpawnRadius);
            float y = Random.Range(-chestSpawnRadius, chestSpawnRadius);

            int chestIndex = 0;

            float currentWeight = 0;

            float randomWeight = Random.Range(0, totalWeight);
            
            for (int j = 0; j < chestPrefabs.Count; j++)
            {
                currentWeight += chestPrefabs[j].data.spawnChance;
                if (randomWeight < currentWeight)
                {
                    chestIndex = j;
                    break;
                }
            }
            
            Chest chestObject = Instantiate(chestPrefabs[chestIndex], player.transform.position + new Vector3(x, y, 0), Quaternion.identity);
            
            // Update chest price to current price
            if(chestsOpened > 0)
                chestObject.IncreasePrice(chestsOpened);
            
            chestObject.OnOpen.AddListener(IncreasePrices);
            OnPricesIncreased.AddListener(chestObject.IncreasePrice);
        }
    }
    
    private IEnumerator SpawnChestsCo()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, chestSpawnRadius, layerMask);

            if (colliders.Length < minChestCountInRadius)
            {
                int diff = minChestCountInRadius - colliders.Length;
                SpawnChests(Random.Range(diff - 5, diff + 5));
            }
            
            yield return waitForSpawnInterval;
        }
    }
}
