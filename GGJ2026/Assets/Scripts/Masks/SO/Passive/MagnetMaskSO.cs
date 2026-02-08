using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Passive/Magnet")]
public class MagnetMaskSO : MaskSO
{
    public float magnetRangeIncrease;
    public float magnetForceIncrease;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        MagnetMask mask = base.MakeMask(pPlayer) as MagnetMask;

        if (!mask)
            return null;

        return mask;
    }
}
