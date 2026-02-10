using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MissileMask : InventoryMask
{
    
    private HomingMissileSO newData;
    
    protected override void ActivateMask()
    {
        base.ActivateMask();
        
        newData = maskData as HomingMissileSO;
        
        StartCoroutine(ShootMissileCo());
    }

    private IEnumerator ShootMissileCo()
    {
        while (true)
        {
            HomingMissile projectileObject = ObjectPool.instance.Get(ObjectTypes.Missiles).GetComponent<HomingMissile>();
            projectileObject.transform.position = transform.position;
            projectileObject.Initialize(Vector2.up, newData.missileSpeed);
            projectileObject.SetDamage(manager.playerData.GetModifiedDamage(newData.damage));
            projectileObject.OnHit.AddListener(UpdateDamageDealt);
            
            yield return waitForCooldown;
        }
    }
}