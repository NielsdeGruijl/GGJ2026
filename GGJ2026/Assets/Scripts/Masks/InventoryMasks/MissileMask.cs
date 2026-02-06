using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MissileMask : InventoryMask
{
    [SerializeField] private ProjectileSO projectile;
    
    private HomingMissileSO newMaskData;
    
    public override void Activate(PlayerMaskData playerMaskData)
    {
        base.Activate(playerMaskData);
        
        newMaskData = maskData as HomingMissileSO;
        
        StartCoroutine(ShootMissileCo());
    }
    
    private IEnumerator ShootMissileCo()
    {
        while (true)
        {
            Projectile projectileObject = Instantiate(projectile.projectilePrefab, transform.position, Quaternion.identity);
            projectileObject.SetSpeed(projectile.Speed);
            projectileObject.SetDamage(newMaskData.damage);
            projectileObject.OnHit.AddListener(UpdateDamageDealt);
            
            yield return waitForCooldown;
        }
    }
}