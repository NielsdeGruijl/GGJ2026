using System;
using System.Collections;
using UnityEngine;

public class HealSuckMask : InventoryMask
{
    [HideInInspector] public float damage;
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

    private IEnumerator StartSuccCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);

            float timeElapsed = 0;

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
            
            lineRenderer.enabled = true;
            
            while (timeElapsed < succDuration)
            {
                if (!target)
                    break;
                
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, target.transform.position);
                
                // do enemy damage, heal player
                target.TakeDamage(damage * Time.deltaTime);
                player.AddHealth(damage *  Time.deltaTime);
                
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            
            lineRenderer.enabled = false;
        }
    }
}
