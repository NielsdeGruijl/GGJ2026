using UnityEngine;

public class SpeedMask : InventoryMask
{
    private SpeedMaskSO newData;
    
    protected override void ActivateMask()
    {
        base.ActivateMask();        
        
        newData = maskData as SpeedMaskSO;
        
        float increaseAmount = newData.speedMult;

        EntityStatModifier statModifier =
            new EntityStatModifier(StatType.MoveSpeed, StatModificationType.multiplier, increaseAmount);
        
        manager.playerData.ApplyStatModifier(statModifier);
    }
}
