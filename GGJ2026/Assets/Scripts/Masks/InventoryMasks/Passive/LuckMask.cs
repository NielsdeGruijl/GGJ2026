using UnityEngine;

public class LuckMask : InventoryMask
{
    private LuckMaskSO newData;
    
    public override void Activate(PlayerMaskData pPlayerMaskData)
    {
        base.Activate(pPlayerMaskData);
        
        newData = maskData as LuckMaskSO;

        // Apply luck bonus with diminishing returns
        pPlayerMaskData.luck += newData.luckIncrease / playerMaskData.sortedMasks[newData.maskName].Count;
    }
}
