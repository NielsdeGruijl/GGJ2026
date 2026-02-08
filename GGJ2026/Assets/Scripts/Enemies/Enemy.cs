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
    
    [Header("Misc")]
    [SerializeField] private float pathFindingTickSpeed;

    private HealthManager healthManager;
    
    private Rigidbody2D rigidBody;

    private Vector2 baseVelocity;
    private float velocityMult = 1;

    private bool canAttack;

    private Transform target;
    private HealthManager targetHealth;

    private WaitForSeconds waitForPathFinding;
    private WaitForSeconds waitForAttack;
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
        
        healthManager = GetComponent<HealthManager>();
        
        healthManager.OnDeath.AddListener(Die);

        if (DifficultyManager.instance)
            DifficultyManager.instance.OnDifficultyChanged.AddListener(UpdateVelocity);

        waitForAttack = new WaitForSeconds(attackCooldown);
        waitForPathFinding = new WaitForSeconds(pathFindingTickSpeed);
        
        StartCoroutine(FindPathToPlayer());
    }

    public void Initialize(Transform pTarget)
    {
        target = pTarget;
        targetHealth = target.GetComponent<HealthManager>();
        
        UpdateVelocity();
        
        StartCoroutine(TryAttackPlayerCo());
        StartCoroutine(FindPathToPlayer());
    }

    public void ApplyKnockback(Vector2 force)
    {
        rigidBody.AddForce(force, ForceMode2D.Impulse);
    }
    
    private void Die()
    {
        GameObject obj = ObjectPool.instance.Get(ObjectTypes.Coins);
        Coin coinObject = obj.GetComponent<Coin>();

        coinObject.transform.position = transform.position;
        coinObject.value = coinValue;

        StartCoroutine(PoolObjectCo());
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(baseVelocity * velocityMult);
    }

    private void UpdateVelocity()
    {
        velocityMult = DifficultyManager.instance.enemyMoveSpeedMult;
    }

    private IEnumerator PoolObjectCo()
    {
        yield return null;
        
        ObjectPool.instance.PoolObject(ObjectTypes.Enemies, gameObject);
    }
    
    private IEnumerator TryAttackPlayerCo()
    {
        while (target)
        {
            if (!gameObject.activeSelf)
                break;
            
            if ((target.transform.position.ToVector2() - transform.position.ToVector2()).magnitude < attackRange)
            {
                if (targetHealth)
                    targetHealth.ApplyDamage(damage * DifficultyManager.instance.enemyDamageMult);
            }
            
            yield return waitForAttack;
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
            baseVelocity = moveDirection * moveSpeed;

            if (baseVelocity.x < 0)
                animator.SetFloat("WalkDir", -1);
            else
                animator.SetFloat("WalkDir", 1);

            yield return waitForPathFinding;
        }
    }
}
