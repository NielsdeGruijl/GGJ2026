using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Aggressive/HomingMissile")]
public class HomingMissileSO : MaskSO
{
    public float damage;

    public override InventoryMask MakeMask(Transform pPlayer)
    {
        MissileMask mask = base.MakeMask(pPlayer) as MissileMask;

        if (!mask)
            return null;
        
        return mask;
    }
}
