using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BurstMask : InventoryMask
{
    private Camera cam;

    private BurstMaskSO newMaskData;

    private WaitForSeconds waitBulletDelay;
    
    public override void Activate(PlayerMaskData playerMaskData)
    {
        base.Activate(playerMaskData);
        cam = Camera.main;
        
        newMaskData = maskData as BurstMaskSO;

        waitBulletDelay = new WaitForSeconds(0.1f);
        
        StartCoroutine(FireBurstCo());
    }

    private IEnumerator FireBurstCo()
    {
        while (true)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 shootDirection = cam.ScreenToWorldPoint(mousePosition) - transform.position;
            shootDirection.Normalize();

            for (int i = 0; i < newMaskData.bulletsPerBurst; i++)
            {
                Projectile bulletObject = ObjectPool.instance.Get(ObjectTypes.Projectiles).GetComponent<Projectile>();
                bulletObject.transform.position = transform.position;
                bulletObject.Initialize(shootDirection, newMaskData.bulletSpeed);
                bulletObject.SetDamage(newMaskData.damagePerBullet);
                
                bulletObject.OnHit.AddListener(UpdateDamageDealt);
                
                if(i != newMaskData.bulletsPerBurst - 1)
                    yield return waitBulletDelay;
            }

            yield return waitForCooldown;
        }
    }
}
