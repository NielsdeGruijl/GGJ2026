using UnityEngine;

public class FireRateMask : InventoryMask
{
    private FireRateMaskSO newData;
    
    protected override void ActivateMask()
    {
        base.ActivateMask();
                
        newData = maskData as FireRateMaskSO;

        float cooldownReduction = newData.cooldownReduction / playerMaskData.sortedMasks[newData.maskName].Count;

        EntityStatModifier statModifier =
            new EntityStatModifier(StatType.AttackSpeed, StatModificationType.multiplier, cooldownReduction);
        
        manager.playerData.ApplyStatModifier(statModifier);
    }
}
