using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private LineRenderer line;

    private List<Collider2D> previousTargets;
    
    private int targetBounces;
    private float damage;
    
    public void Initialize(int bounces, float damagePerBounce)
    {
        previousTargets = new List<Collider2D>();    
        
        targetBounces = bounces;
        damage = damagePerBounce;
        
        line.endWidth = 0.4f;
        line.startWidth = 0.4f;

        StartCoroutine(ChainLightning());
    }

    private IEnumerator ChainLightning()
    {
        int targetsHit = 0;

        Vector2 position = transform.position;
        
        Collider2D currentTarget;
        
        while (targetsHit < targetBounces)
        {
            currentTarget = FindClosestTarget(position);

            if (!currentTarget)
                break;

            if(targetsHit == 0)
                line.SetPosition(0, transform.position.ToVector2());
            else
                line.positionCount++;
            
            position = currentTarget.transform.position.ToVector2();
            line.SetPosition(targetsHit + 1, position);

            if (currentTarget.TryGetComponent(out HealthManager health))
            {
                health.TakeDamage(damage);
            }

            previousTargets.Add(currentTarget);
            
            targetsHit++;
            
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private Collider2D FindClosestTarget(Vector2 position)
    {
        float radius = 10;
        
        float closestDistance = Mathf.Infinity;
        Collider2D closestTarget = null;
        
        foreach (Collider2D target in Physics2D.OverlapCircleAll(position, radius))
        {
            if (!target.CompareTag("Enemy"))
                continue;

            if (previousTargets.Contains(target))
                continue;

            float newClosest = (position - target.transform.position.ToVector2()).magnitude;
            if (newClosest < closestDistance)
            {
                closestDistance = newClosest;
                closestTarget = target;
            }
        }

        return closestTarget;
    }
}
