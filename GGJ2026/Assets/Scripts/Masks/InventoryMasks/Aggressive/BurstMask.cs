using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BurstMask : InventoryMask
{
    private Camera cam;

    private BurstMaskSO newData;

    private WaitForSeconds waitBulletDelay;
    
    public override void Activate(PlayerMaskData playerMaskData)
    {
        base.Activate(playerMaskData);
        cam = Camera.main;
        
        newData = maskData as BurstMaskSO;

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

            for (int i = 0; i < newData.bulletsPerBurst; i++)
            {
                Projectile bulletObject = ObjectPool.instance.Get(ObjectTypes.Projectiles).GetComponent<Projectile>();
                bulletObject.transform.position = transform.position;
                bulletObject.Initialize(shootDirection, newData.bulletSpeed);
                bulletObject.SetDamage(newData.damagePerBullet * playerMaskData.playerDamageMult);
                
                bulletObject.OnHit.AddListener(UpdateDamageDealt);
                
                if(i != newData.bulletsPerBurst - 1)
                    yield return waitBulletDelay;
            }

            yield return waitForCooldown;
        }
    }
}
