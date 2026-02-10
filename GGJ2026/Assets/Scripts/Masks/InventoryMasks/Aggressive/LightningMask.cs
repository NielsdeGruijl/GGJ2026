using System.Collections;
using UnityEngine;

public class LightningMask : InventoryMask
{
    private LightningMaskSO newData;
    
    protected override void ActivateMask()
    {
        base.ActivateMask();
        
        newData = maskData as LightningMaskSO;

        StartCoroutine(ShootLightningCo());
    }

    private IEnumerator ShootLightningCo()
    {
        while (true)
        {
            Lightning lightning = Instantiate(newData.lightningPrefab, transform.position, Quaternion.identity);
            lightning.Initialize(newData.totalBounces, manager.playerData.GetModifiedDamage(newData.damagePerBounce));
            yield return waitForCooldown;
        }
    }
}
