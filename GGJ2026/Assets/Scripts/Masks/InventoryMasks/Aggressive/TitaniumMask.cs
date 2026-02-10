using System.Collections;
using UnityEngine;

public class TitaniumMask : InventoryMask
{
    [HideInInspector] public float damagePerStack;

    private DamagingMasksSO newData;

    private bool canDamageEnemy = true;
    
    protected override void ActivateMask()
    {
        base.ActivateMask();
        newData = maskData as DamagingMasksSO;

        OnAuraDamage.AddListener(UpdateDamageDealt);
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!canDamageEnemy)
            return;

        if (other.TryGetComponent(out HealthManager enemy))
        {
            Vector2 knockbackForce = (enemy.transform.position - transform.position).normalized * 10;
            enemy.ApplyDamage(new HitInfo(newData.collisionDamagePerStack, knockbackForce));
            UpdateDamageDealt(newData.collisionDamagePerStack);
            

            StartCoroutine(CollisionCooldownCo());
        }
    }

    private IEnumerator CollisionCooldownCo()
    {
        canDamageEnemy = false;
        yield return waitForCooldown;
        canDamageEnemy = true;
    }
}
