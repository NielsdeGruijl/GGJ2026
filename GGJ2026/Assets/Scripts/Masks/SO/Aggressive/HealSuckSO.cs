using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Aggressive/HealSucc")]
public class HealSuckSO : MaskSO
{
    public float lifeSteal;
    public float healPercent;
    public float succRange;
    public float succDuration;
    public float succInterval;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        HealSuckMask mask = base.MakeMask(pPlayer) as HealSuckMask;

        if (!mask)
            return null;
        
        return mask;
    }
}
