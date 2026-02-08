using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Passive/IncreaseDamage")]
public class IncreaseDamageMaskSO : MaskSO
{
    public float damageMultIncrement;
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        IncreaseDamageMask mask = base.MakeMask(pPlayer) as IncreaseDamageMask;

        if (!mask)
            return null;

        return mask;
    }
}
