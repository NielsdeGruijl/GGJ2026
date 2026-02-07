using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Aggressive/BurstMask")]
public class BurstMaskSO : MaskSO
{
    public Projectile bulletPrefab;
    public float damagePerBullet;
    public int bulletsPerBurst;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        BurstMask mask = base.MakeMask(pPlayer) as BurstMask;

        if (!mask)
            return null;
        
        return mask;
    }
}
