using System;
using System.Collections;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [Header("Currency")]
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private int coinValue;

    [Header("Stats")] 
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;

    private HealthManager healthManager;
    
    private Rigidbody2D rigidBody;

    private Vector2 velocity;

    private float timeElapsed;

    private bool canAttack;

    private Transform target;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
        
        healthManager = GetComponent<HealthManager>();
        
        healthManager.OnDeath.AddListener(DropCoins);
        
        StartCoroutine(FindPathToPlayer());
    }

    private void Update()
    {
        if ((target.transform.position.ToVector2() - transform.position.ToVector2()).magnitude < attackRange && canAttack)
        {
            if (target.TryGetComponent(out HealthManager targetHealth))
            {
                targetHealth.TakeDamage(damage * DifficultyManager.instance.enemyDamageMult);
            }
            canAttack = false;
            timeElapsed = 0;
        }
        
        if(!canAttack)
            timeElapsed += Time.deltaTime;

        if (timeElapsed > attackCooldown)
            canAttack = true;
    }

    public void Initialize(Transform pTarget)
    {
        target = pTarget;
        StartCoroutine(FindPathToPlayer());
    }

    public void ApplyKnockback(Vector2 force)
    {
        rigidBody.AddForce(force, ForceMode2D.Impulse);
    }
    
    private void DropCoins()
    {
        Coin coinObject = ObjectPool.instance.Get("Coins").GetComponent<Coin>();
        coinObject.transform.position = transform.position;
        coinObject.value = coinValue;
        
        ObjectPool.instance.PoolObject("Enemies", gameObject);
    }

    private void FixedUpdate()
    {
        if(DifficultyManager.instance)
            rigidBody.AddForce(velocity * DifficultyManager.instance.enemyMoveSpeedMult);
        else
        {
            rigidBody.AddForce(velocity);
        }
    }

    private IEnumerator FindPathToPlayer()
    {
        while (target)
        {
            if (!gameObject.activeSelf)
                break;
            
            Vector2 moveDirection = target.transform.position.ToVector2() - transform.position.ToVector2();
            moveDirection.Normalize();
            velocity = moveDirection * moveSpeed;

            if (velocity.x < 0)
                animator.SetFloat("WalkDir", -1);
            else
                animator.SetFloat("WalkDir", 1);

            yield return new WaitForSeconds(1);
        }
    }
}
