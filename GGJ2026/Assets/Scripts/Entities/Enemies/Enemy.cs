using System.Collections;
using UnityEngine;

public interface IKnockbackable
{
    void ApplyKnockback(HitInfo hitInfo);
}

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Entity, IKnockbackable
{
    [SerializeField] private Animator animator;
    
    [Header("Currency")]
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private int coinValue;

    [Header("Misc")]
    [SerializeField] private float pathFindingTickSpeed;

    private Vector2 velocity;

    private HealthManager target;

    private WaitForSeconds waitForPathFinding;
    private WaitForSeconds waitForAttack;
    
    protected override void Awake()
    {
        base.Awake();
        
        healthManager.OnDamage.AddListener(ApplyKnockback);
        
        stats.GetStatEvent(StatType.AttackSpeed).AddListener(UpdateAttackSpeed);
        
        waitForAttack = new WaitForSeconds(stats.GetStatValue(StatType.AttackSpeed));
        waitForPathFinding = new WaitForSeconds(pathFindingTickSpeed);
        
        StartCoroutine(FindPathToPlayer());
    }

    private void OnEnable()
    {
        healthManager.UpdateMaxHealth(stats.GetStatValue(StatType.Health));
    }

    public void Initialize(HealthManager pTarget)
    {
        target = pTarget;
        
        StartCoroutine(TryAttackPlayerCo());
        StartCoroutine(FindPathToPlayer());
    }

    public void ApplyKnockback(HitInfo hitInfo)
    {
        if (!hitInfo.dealsKnockback)
            return;
        
        rigidBody.AddForce(hitInfo.knockbackForce, ForceMode2D.Impulse);
    }

    protected override void Die()
    {
        base.Die();
        
        StopAllCoroutines();
        
        velocity = Vector2.zero;
        
        SpawnCoin();

        StartCoroutine(PoolObjectCo());
    }

    private void SpawnCoin()
    {
        GameObject obj = ObjectPool.instance.Get(ObjectTypes.Coins);
        Coin coinObject = obj.GetComponent<Coin>();

        coinObject.transform.position = transform.position;
        coinObject.value = coinValue;
    }

    private void UpdateAttackSpeed(float newAttackSpeed)
    {
        waitForAttack = new WaitForSeconds(newAttackSpeed);
    }
    
    
    private void FixedUpdate()
    {
        rigidBody.AddForce(velocity);
    }

    private IEnumerator PoolObjectCo()
    {
        yield return new WaitForSeconds(0.2f);

        
        ObjectPool.instance.PoolObject(ObjectTypes.Enemies, gameObject);
    }
    
    private IEnumerator TryAttackPlayerCo()
    {
        while (target)
        {
            if (!gameObject.activeSelf)
                break;
            
            if ((target.transform.position.ToVector2() - transform.position.ToVector2()).magnitude < stats.GetStatValue(StatType.AttackRange))
            {
                if (target)
                    target.ApplyDamage(new HitInfo(stats.GetStatValue(StatType.AttackDamage)));
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
            velocity = moveDirection * stats.GetStatValue(StatType.MoveSpeed);

            if (velocity.x < 0)
                animator.SetFloat("WalkDir", -1);
            else
                animator.SetFloat("WalkDir", 1);

            yield return waitForPathFinding;
        }
    }
}
