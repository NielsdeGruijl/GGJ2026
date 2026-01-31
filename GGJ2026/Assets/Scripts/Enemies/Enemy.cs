using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [Header("Currency")]
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private int coinValue;
    
    [Header("Stats")]
    [SerializeField] private float moveSpeed;
    
    private HealthManager healthManager;
    
    private Rigidbody2D rigidBody;

    private Vector2 velocity;

    [HideInInspector] public Player target;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
        
        healthManager = GetComponent<HealthManager>();
        
        healthManager.OnDeath.AddListener(DropCoins);
    }

    public void Initialize(Player pTarget)
    {
        target = pTarget;
        StartCoroutine(FindPathToPlayer());
    }
    
    private void DropCoins()
    {
        Coin coinObject = Instantiate(coinPrefab, transform.position, transform.rotation);
        coinObject.value = coinValue;
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(velocity);
    }

    private IEnumerator FindPathToPlayer()
    {
        while (target)
        {
            Vector2 moveDirection = target.transform.position.ToVector2() - transform.position.ToVector2();
            moveDirection.Normalize();
            velocity = moveDirection * moveSpeed;

            yield return new WaitForSeconds(1);
        }
    }
}
