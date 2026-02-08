using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Passive/FireRate")]
public class FireRateMaskSO : MaskSO
{
    public float cooldownReduction;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        FireRateMask mask = base.MakeMask(pPlayer) as FireRateMask;

        if (!mask)
            return null;

        return mask;
    }
}
