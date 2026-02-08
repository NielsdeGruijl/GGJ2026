using System.Collections;
using UnityEngine;

public class LightningMask : InventoryMask
{
    private LightningMaskSO newData;
    
    public override void Activate(PlayerMaskData pPlayerMaskData)
    {
        base.Activate(pPlayerMaskData);
        
        newData = maskData as LightningMaskSO;

        StartCoroutine(ShootLightningCo());
    }

    private IEnumerator ShootLightningCo()
    {
        while (true)
        {
            Lightning lightning = Instantiate(newData.lightningPrefab, transform.position, Quaternion.identity);
            lightning.Initialize(newData.totalBounces, newData.damagePerBounce * playerMaskData.playerDamageMult);
            yield return waitForCooldown;
        }
    }
}
