using UnityEngine;

public class FireRateMask : InventoryMask
{
    private FireRateMaskSO newData;
    
    public override void Activate(PlayerMaskData pPlayerMaskData)
    {
        base.Activate(pPlayerMaskData);
        
        newData = maskData as FireRateMaskSO;

        playerMaskData.cooldownMult -= (newData.cooldownReduction * 0.01f) / playerMaskData.sortedMasks[newData.maskName].Count;
        playerMaskData.OnCooldownChanged.Invoke();
    }
}
