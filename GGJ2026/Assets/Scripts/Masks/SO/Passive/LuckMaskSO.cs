using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Passive/Luck")]
public class LuckMaskSO : MaskSO
{
    public float luckIncrease;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        LuckMask mask = base.MakeMask(pPlayer) as LuckMask;

        if (!mask)
            return null;

        return mask;
    }
}
