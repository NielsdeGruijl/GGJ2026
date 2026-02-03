using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BurstMask : InventoryMask
{
    private Camera cam;

    private BurstMaskSO newMaskData;
    
    public override void Activate(PlayerMaskData playerMaskData)
    {
        base.Activate(playerMaskData);
        cam = Camera.main;
        
        newMaskData = maskData as BurstMaskSO;
        
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
                Projectile bulletObject = Instantiate(newMaskData.bulletPrefab, transform.position, Quaternion.identity);
                bulletObject.velocity = shootDirection;
                bulletObject.SetDamage(newMaskData.damagePerBullet);
                
                bulletObject.OnHit.AddListener(UpdateDamageDealt);
                
                if(i != newMaskData.bulletsPerBurst - 1)
                    yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(maskData.cooldown);
        }
    }
}
