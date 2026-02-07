using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public ObjectTypes tag;
    public GameObject prefab;
    public int baseSize;
}

public enum ObjectTypes
{
    Enemies,
    Coins,
    LingeringAreas,
    DamagePopups,
    Projectiles,
    Explosions,
    Missiles
}


public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<Pool> pools;    
    
    private Dictionary<ObjectTypes, Queue<GameObject>> objectPools;
    public static ObjectPool instance;
    
    private void Awake()
    {
        if (instance && instance != this)
            Destroy(this);
        else
            instance = this;

        objectPools = new Dictionary<ObjectTypes, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();
            
            for (int i = 0; i < pool.baseSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }
            
            objectPools.Add(pool.tag, objectQueue);
        }
    }
   
    public GameObject Get(ObjectTypes poolTag)
    {
        Queue<GameObject> currentPool = objectPools[poolTag];

        if (currentPool.Count <= 0)
            GrowPool(poolTag);

        GameObject obj = currentPool.Dequeue();
        
        if(!obj)
            Debug.LogError("Pooled object is non-existent");
        
        obj.SetActive(true);
        
        return obj;
    }

    public void PoolObject(ObjectTypes tag, GameObject objectToPool)
    {
        if (!objectPools.ContainsKey(tag))
        {
            Debug.LogError("Tag not found!");
            return;
        }
        
        objectToPool.SetActive(false);
        objectToPool.transform.SetParent(transform);
        objectPools[tag].Enqueue(objectToPool);
    }

    private void GrowPool(ObjectTypes tag)
    {
        Pool poolToGrow = null;
        foreach (Pool pool in pools)
        {
            if (pool.tag != tag)
                continue;
            
            poolToGrow = pool;
        }
        
        if (poolToGrow != null)
        {
            objectPools[tag].Enqueue(Instantiate(poolToGrow.prefab));
        }
    }
}
