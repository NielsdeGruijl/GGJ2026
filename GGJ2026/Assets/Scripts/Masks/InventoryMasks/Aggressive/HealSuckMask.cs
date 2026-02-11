using System.Collections;
using UnityEngine;

public class HealSuckMask : InventoryMask
{
    private LineRenderer lineRenderer;
    
    private HealthManager player;

    private HealSuckSO newData;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    protected override void ActivateMask()
    {
        base.ActivateMask();
                
        newData =maskData as HealSuckSO;

        StartCoroutine(StartSuccCo());
    }

    private HealthManager FindTarget()
    {            
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, newData.succRange);

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

            int tickCount = 0;
            
            float actualDamage = manager.playerData.GetModifiedDamage(newData.lifeSteal) * newData.succInterval;
            
            while (timeElapsed < newData.succDuration)
            {
                if (!target || !target.isActiveAndEnabled)
                    target = FindTarget();

                if (!target)
                    break;
                
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, target.transform.position);
                
                // do enemy damage, heal player
                if (target.isActiveAndEnabled && Mathf.FloorToInt(timeElapsed / newData.succInterval) > tickCount)
                {
                    target.ApplyDamage(new HitInfo(actualDamage));
                    player.AddHealth(actualDamage * newData.healPercent);
                    UpdateDamageDealt(actualDamage);
                    tickCount++;
                }
                
                yield return null;
                timeElapsed += Time.deltaTime;
            }
            
            lineRenderer.enabled = false;
            
            yield return waitForCooldown;
        }
    }
}
