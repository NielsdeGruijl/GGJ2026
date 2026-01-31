using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MissileMask : InventoryMask
{
    [SerializeField] private ProjectileSO projectile;

    public override void Activate()
    {
        StartCoroutine(ShootMissileCo());
    }
    
    private IEnumerator ShootMissileCo()
    {
        while (true)
        {
            Projectile projectileObject = Instantiate(projectile.projectilePrefab, transform.position, Quaternion.identity);
            projectileObject.SetSpeed(projectile.Speed);
            
            yield return new WaitForSeconds(cooldown);
        }
    }
}