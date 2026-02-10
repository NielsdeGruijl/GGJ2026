using UnityEngine;

public class StickyGoop : InventoryMask
{
    private StickyGoopSO newData;

    protected override void ActivateMask()
    {
        base.ActivateMask();
        newData = maskData as StickyGoopSO;

        playerMaskData.lingeringProcChance += newData.procChance / playerMaskData.sortedMasks[newData.maskName].Count;
        playerMaskData.LingeringBaseDebuff = newData.BoggedDebuff;
    }
}
