using System;
using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Transform sprite;
    
    [HideInInspector] public float range;
    [HideInInspector] public float damage;
    [HideInInspector] public float fuzeTimer;

    public void Initialize(float pRange, float pDamage, float pFuzeTimer)
    {
        range = pRange;
        damage = pDamage;
        fuzeTimer = pFuzeTimer;

        sprite.localScale *= range * 2;
        
        StartCoroutine(StartTimerCo());
    }

    private IEnumerator StartTimerCo()
    {
        yield return new WaitForSeconds(fuzeTimer);
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out HealthManager enemy))
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
