using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 15;
    [SerializeField] private float rotateInterval;
    [SerializeField] private ContactFilter2D filter;

    private Collider2D activeTarget;

    private Rigidbody2D rigidBody;

    private const float trackingRadius = 200;

    private WaitForSeconds waitTrackTarget;

    private float moveSpeed;

    private WaitForSeconds waitForRotateInterval;

    private Collider2D[] targets = new Collider2D[16];

    private float damage;

    public DamageEvent OnHit;

    private void Start()
    {
        waitTrackTarget = new WaitForSeconds(0.5f);
        waitForRotateInterval = new WaitForSeconds(rotateInterval);

        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 moveDirection, float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        StartCoroutine(RotateToTargetCo());
        StartCoroutine(TrackTargetCo());
    }

    private IEnumerator TrackTargetCo()
    {
        Collider2D target;
        while (true)
        {
            if (!activeTarget || !activeTarget.isActiveAndEnabled)
            {
                if (target = FindTarget())
                    activeTarget = target;
            }

            yield return waitTrackTarget;
        }
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private Collider2D FindTarget()
    {
        Physics2D.OverlapCircle(transform.position.ToVector2(), trackingRadius, filter, targets);

        float distance = Mathf.Infinity;
        Collider2D closestCollider = null;
        
        foreach (Collider2D target in targets)
        {
            if (!target)
                continue;
            
            if (!target.CompareTag("Enemy"))
                continue;
            
            float tempDistance = (target.transform.position.ToVector2() -  transform.position.ToVector2()).magnitude;
            if (tempDistance < distance)
            {
                closestCollider = target;
                distance = tempDistance;
            }
        }

        return closestCollider;
    }

    private void RotateToTarget()
    {
        if (!activeTarget || !activeTarget.isActiveAndEnabled)
            activeTarget = FindTarget();

        if (!activeTarget)
            return;
        
        Vector2 targetDirection = (activeTarget.transform.position.ToVector2() - transform.position.ToVector2()).normalized;
        float diffAngle = Vector2.Angle(transform.up, targetDirection);
        
        float dirNormal = 0;

        if (IsLeft(transform.up, targetDirection))
            dirNormal = 1;
        else
            dirNormal = -1;
        
        float angleStep = dirNormal * turnSpeed;
        
        if(diffAngle < turnSpeed)
            angleStep = diffAngle * dirNormal;
        
        transform.Rotate(0, 0, angleStep);
    }

    private IEnumerator RotateToTargetCo()
    {
        while (true)
        {
            RotateToTarget();
            
            yield return waitForRotateInterval;
        }
    }
    
    bool IsLeft(Vector2 A, Vector2 B)
    {
        return (-A.x * B.y + A.y * B.x) < 0;
    }
    
    private void FixedUpdate()
    {
        rigidBody.AddForce(transform.up * (moveSpeed * Time.fixedDeltaTime));
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthManager enemy))
        {
            enemy.ApplyDamage(damage);
            OnHit.Invoke(damage);
            OnHit.RemoveAllListeners();
            
            ObjectPool.instance.PoolObject(ObjectTypes.Missiles, gameObject);
        }
    }
}