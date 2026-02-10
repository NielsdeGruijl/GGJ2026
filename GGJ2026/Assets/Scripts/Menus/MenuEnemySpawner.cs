using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MenuEnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform target;
    
    private void Awake()
    {
        StartCoroutine(coroutine());
    }

    private IEnumerator coroutine()
    {
        while (true)
        {
            Enemy enemyObject = Instantiate(enemy, transform.position, Quaternion.identity);
            enemyObject.Initialize(target.GetComponent<HealthManager>());
            
            yield return new WaitForSeconds(5);
        }
    }
}
