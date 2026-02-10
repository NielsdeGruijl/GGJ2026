using UnityEngine;

public class MagnetMask : InventoryMask
{
    private MagnetMaskSO newData;
    
    protected override void ActivateMask()
    {
        base.ActivateMask();
        
        newData = maskData as MagnetMaskSO;

        if (!newData)
        {
            Debug.LogError("New data is not of type MagnetMaskSO");
            return;
        }

        EntityStatModifier rangeModifier = 
            new EntityStatModifier(StatType.MagnetRange, StatModificationType.flat, newData.magnetRangeIncrease);
        EntityStatModifier pullModifier =
            new EntityStatModifier(StatType.MagnetPull, StatModificationType.flat, newData.magnetForceIncrease);
        
        manager.playerData.ApplyStatModifier(rangeModifier);
        manager.playerData.ApplyStatModifier(pullModifier);
    }
}
