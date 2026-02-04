using UnityEngine;

[CreateAssetMenu(menuName = "Masks/HealSucc")]
public class HealSuckSO : MaskSO
{
    public float lifeSteal;
    public float healPercent;
    public float succRange;
    public float succDuration;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        HealSuckMask mask = base.MakeMask(pPlayer) as HealSuckMask;

        if (!mask)
            return null;
        
        /*mask.damage = lifeSteal;
        mask.healPrecent = healPercent;
        mask.succRange = succRange;
        mask.succDuration = succDuration;*/
        
        return mask;
    }
}
