using UnityEngine;

[CreateAssetMenu(menuName = "Masks/BurstMask")]
public class BurstMaskSO : MaskSO
{
    [SerializeField] private Projectile bulletPrefab;
    [SerializeField] private float damagePerBullet;
    [SerializeField] private int bulletsPerBurst;
    
    public override InventoryMask MakeMask(Player pPlayer)
    {
        BurstMask mask = Instantiate(maskItem, pPlayer.transform.position, Quaternion.identity) as BurstMask;
        mask.bulletPrefab = bulletPrefab;
        mask.cooldown = cooldown;
        mask.damage = damagePerBullet;
        mask.bulletCount = bulletsPerBurst;
        return mask;
    }
}
