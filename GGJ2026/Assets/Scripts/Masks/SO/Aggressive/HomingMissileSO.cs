using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Aggressive/HomingMissile")]
public class HomingMissileSO : MaskSO
{
    public HomingMissile projectilePrefab;
    
    public float damage;
    public float missileSpeed;

    public override InventoryMask MakeMask(Transform pPlayer)
    {
        MissileMask mask = base.MakeMask(pPlayer) as MissileMask;

        if (!mask)
            return null;
        
        return mask;
    }
}
