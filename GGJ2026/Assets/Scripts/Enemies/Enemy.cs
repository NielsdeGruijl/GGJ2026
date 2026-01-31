using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private int coinValue;
    
    HealthManager healthManager;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        
        healthManager.OnDeath.AddListener(DropCoins);
    }

    private void DropCoins()
    {
        Coin coinObject = Instantiate(coinPrefab, transform.position, transform.rotation);
        coinObject.value = coinValue;
    }

}
