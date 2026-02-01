using System.Collections;
using UnityEngine;

public class MissileMask : InventoryMask
{
    [SerializeField] private ProjectileSO projectile;
    
    [HideInInspector] public float damage;

    public override void Activate()
    {
        base.Activate();
        StartCoroutine(ShootMissileCo());
    }
    
    private IEnumerator ShootMissileCo()
    {
        while (true)
        {
            Projectile projectileObject = Instantiate(projectile.projectilePrefab, transform.position, Quaternion.identity);
            projectileObject.SetSpeed(projectile.Speed);
            projectileObject.SetDamage(projectile.damage + damage);
            
            yield return new WaitForSeconds(cooldown);
        }
    }
}