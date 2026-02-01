using System;
using System.Collections;
using UnityEngine;

public class HealSuckMask : InventoryMask
{
    [HideInInspector] public float damage;
    [HideInInspector] public float healPrecent;
    [HideInInspector] public float succRange;
    [HideInInspector] public float succDuration;
    
    private LineRenderer lineRenderer;
    
    private HealthManager player;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public override void Activate()
    {
        base.Activate();
        StartCoroutine(StartSuccCo());
    }

    private HealthManager FindTarget()
    {            
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, succRange);

        HealthManager target = null;
            
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out HealthManager health))
            {
                if(collider.TryGetComponent(out Enemy enemy) && !target)
                    target = health;

                if (collider.TryGetComponent(out Player pPlayer))
                    player = health;
            }
        }

        return target;
    }

    private IEnumerator StartSuccCo()
    {
        while (true)
        {
            float timeElapsed = 0;

            HealthManager target = FindTarget();
            
            lineRenderer.enabled = true;
            
            while (timeElapsed < succDuration)
            {
                if (!target)
                    target = FindTarget();

                if (!target)
                    break;
                
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, target.transform.position);
                
                float actualDamage = damage * Time.deltaTime;
                
                // do enemy damage, heal player
                target.TakeDamage(actualDamage, true);
                player.AddHealth(actualDamage * healPrecent);
                
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            
            lineRenderer.enabled = false;
            
            yield return new WaitForSeconds(cooldown);
        }
    }
}
