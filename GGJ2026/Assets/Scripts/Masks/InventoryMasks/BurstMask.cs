using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BurstMask : InventoryMask
{
    [HideInInspector] public Projectile bulletPrefab;
    [HideInInspector] public float damage;
    [HideInInspector] public int bulletCount;

    private Camera cam;
    
    public override void Activate()
    {
        base.Activate();
        cam = Camera.main;
        
        StartCoroutine(FireBurstCo());
    }

    private IEnumerator FireBurstCo()
    {
        while (true)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 shootDirection = cam.ScreenToWorldPoint(mousePosition) - transform.position;
            shootDirection.Normalize();

            for (int i = 0; i < bulletCount; i++)
            {
                Projectile bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bulletObject.velocity = shootDirection;
                bulletObject.SetDamage(damage);
                
                if(i != bulletCount - 1)
                    yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(cooldown);
        }
    }
}
