using System;
using System.Collections;
using UnityEngine;

public class HealSuckMask : InventoryMask
{
    private LineRenderer lineRenderer;
    
    private HealthManager player;

    private HealSuckSO newMaskData;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public override void Activate(PlayerMaskData playerMaskData)
    {
        base.Activate(playerMaskData);
        
        newMaskData = maskData as HealSuckSO;
        
        StartCoroutine(StartSuccCo());
    }

    private HealthManager FindTarget()
    {            
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, newMaskData.succRange);

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
            
            while (timeElapsed < newMaskData.succDuration)
            {
                if (!target || !target.isActiveAndEnabled)
                    target = FindTarget();

                if (!target)
                    break;
                
                
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, target.transform.position);
                
                float actualDamage = newMaskData.lifeSteal * Time.deltaTime;
                
                // do enemy damage, heal player
                if (target.isActiveAndEnabled)
                {
                    target.TakeDamage(actualDamage, true);
                    player.AddHealth(actualDamage * newMaskData.healPercent);
                    UpdateDamageDealt(actualDamage);
                }
                
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            
            lineRenderer.enabled = false;
            
            yield return new WaitForSeconds(maskData.cooldown);
        }
    }
}
