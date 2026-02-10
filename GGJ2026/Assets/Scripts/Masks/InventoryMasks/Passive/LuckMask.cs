using UnityEngine;

public class LuckMask : InventoryMask
{
    private LuckMaskSO newData;
    
    protected override void ActivateMask()
    {
        base.ActivateMask();
        
        newData = maskData as LuckMaskSO;

        // Apply luck bonus with diminishing returns
        float increaseAmount = newData.luckIncrease / playerMaskData.sortedMasks[newData.maskName].Count;

        EntityStatModifier statModifier =
            new EntityStatModifier(StatType.Luck, StatModificationType.flat, increaseAmount);
        
        manager.playerData.ApplyStatModifier(statModifier);
    }
}
