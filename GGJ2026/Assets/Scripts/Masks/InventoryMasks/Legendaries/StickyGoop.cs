using UnityEngine;

public class StickyGoop : InventoryMask
{
    private StickyGoopSO newData;
    public override void Activate(PlayerMaskData pPlayerMaskData)
    {
        base.Activate(pPlayerMaskData);
        
        newData = maskData as StickyGoopSO;

        pPlayerMaskData.lingeringProcChance += newData.procChance / pPlayerMaskData.sortedMasks[newData.maskName].Count;
        pPlayerMaskData.LingeringBaseDebuff = newData.BoggedDebuff;
    }
}
