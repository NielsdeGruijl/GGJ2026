using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Aggressive/TrailBlazer")]
public class TrailBlazerSO : MaskSO
{
    public GameObject grenade;
    public Explosion explosionPrefab;
    public float grenadeSpeed;
    public float explosionDelay;
    public float explosionCount;
    public float explosionRange;
    public float explosionDamage;
    public float fuzeTimer;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        TrailBlazerMask mask = base.MakeMask(pPlayer) as TrailBlazerMask;

        if (!mask)
            return null;
        
        /*mask.grenade = grenade;
        mask.explosionPrefab = explosionPrefab;
        mask.explosionCount = explosionCount;
        mask.explosionRange = explosionRange;
        mask.explosionDamage = explosionDamage;
        mask.fuzeTimer = fuzeTimer;
        mask.grenadeSpeed = grenadeSpeed;
        mask.explosionDelay = explosionDelay;*/
        
        return mask;
    }
}
