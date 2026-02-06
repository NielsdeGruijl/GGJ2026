using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager instance { get; private set; }

    [SerializeField] private float enemyHealthScaling = 1;
    [SerializeField] private float enemySpeedScaling = 1;
    [SerializeField] private float chestCostScaling = 1;
    [SerializeField] private float enemySpawnCountScaling = 1;
    [SerializeField] private float enemySpawnIntervalScaling = 1;
    [SerializeField] private float enemyDamageScaling = 1;
    
    [SerializeField] private float scalingInterval;
    
    [HideInInspector] public float enemyHealthMult = 1;
    [HideInInspector] public float enemyMoveSpeedMult = 1;
    [HideInInspector] public float chestCostMult = 1;
    [HideInInspector] public float enemySpawnCountMult = 1;
    [HideInInspector] public float enemySpawnIntervalMult = 1;
    [HideInInspector] public float enemyDamageMult = 1;
    
    public UnityEvent OnDifficultyChanged;

    private WaitForSeconds waitInterval;
    
    private void Awake()
    {
        if(instance && instance != this)
            Destroy(this);
        else
            instance = this;

        StartCoroutine(ScaleCo());

        waitInterval = new WaitForSeconds(scalingInterval);
    }

    private IEnumerator ScaleCo()
    {
        while (true)
        {
            yield return waitInterval;

            enemyHealthMult *= enemyHealthScaling;

            enemyMoveSpeedMult *= enemySpeedScaling;

            chestCostMult *= chestCostScaling;
            
            enemySpawnCountMult *= enemySpawnCountScaling;

            enemyDamageMult *= enemyDamageScaling;

            enemySpawnIntervalMult *= enemySpawnIntervalScaling;
            
            OnDifficultyChanged.Invoke();
        }
    }
}
