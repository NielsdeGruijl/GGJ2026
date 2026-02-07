using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MissileMask : InventoryMask
{
    
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
            HomingMissile projectileObject = ObjectPool.instance.Get(ObjectTypes.Missiles).GetComponent<HomingMissile>();
            projectileObject.transform.position = transform.position;
            projectileObject.Initialize(Vector2.up, newMaskData.missileSpeed);
            projectileObject.SetDamage(newMaskData.damage);
            projectileObject.OnHit.AddListener(UpdateDamageDealt);
            
            yield return waitForCooldown;
        }
    }
}