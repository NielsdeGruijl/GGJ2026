using UnityEngine;

public class IncreaseDamageMask : InventoryMask
{
    private IncreaseDamageMaskSO newData;
    public override void Activate(PlayerMaskData pPlayerMaskData)
    {
        base.Activate(pPlayerMaskData);
        
        newData = maskData as IncreaseDamageMaskSO;

        playerMaskData.playerDamageMult += newData.damageMultIncrement * 0.01f / playerMaskData.sortedMasks[newData.maskName].Count;
    }
}
