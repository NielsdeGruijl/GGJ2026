using UnityEngine;

public class IncreaseDamageMask : InventoryMask
{
    private IncreaseDamageMaskSO newData;

    protected override void ActivateMask()
    {
        base.ActivateMask();
        
        newData = maskData as IncreaseDamageMaskSO;
        
        float increaseAmount = newData.damageMultIncrement / playerMaskData.sortedMasks[newData.maskName].Count;

        EntityStatModifier statModifier =
            new EntityStatModifier(StatType.AttackDamage, StatModificationType.multiplier, increaseAmount);
        
        manager.playerData.ApplyStatModifier(statModifier);
        
        Debug.Log("Increased damage");
    }
}
