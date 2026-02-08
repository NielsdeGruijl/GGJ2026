using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MissileMask : InventoryMask
{
    
    private HomingMissileSO newData;
    
    public override void Activate(PlayerMaskData playerMaskData)
    {
        base.Activate(playerMaskData);

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
            projectileObject.SetDamage(newData.damage * playerMaskData.playerDamageMult);
            projectileObject.OnHit.AddListener(UpdateDamageDealt);
            
            yield return waitForCooldown;
        }
    }
}