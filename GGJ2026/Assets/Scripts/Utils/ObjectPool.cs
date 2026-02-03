using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int baseSize;
}

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<Pool> pools;    
    
    private Dictionary<string, Queue<GameObject>> objectPools;
    public static ObjectPool instance;
    
    private void Awake()
    {
        if (instance && instance != this)
            Destroy(this);
        else
            instance = this;

        objectPools = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();
            
            for (int i = 0; i < pool.baseSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }
            
            objectPools.Add(pool.tag, objectQueue);
        }
    }

    public GameObject Get(string poolTag)
    {
        Queue<GameObject> currentPool = objectPools[poolTag];

        if (currentPool.Count <= 0)
            GrowPool(poolTag);

        GameObject obj = currentPool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void PoolObject(string tag, GameObject objectToPool)
    {
        objectToPool.SetActive(false);
        objectPools[tag].Enqueue(objectToPool);
    }

    private void GrowPool(string tag)
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
