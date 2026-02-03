using UnityEngine;

[CreateAssetMenu(menuName = "Masks/TrailBlazer")]
public class TrailBlazerSO : MaskSO
{
    [SerializeField] private GameObject grenade;
    [SerializeField] private Explosion explosionPrefab;
    [SerializeField] private float grenadeSpeed;
    [SerializeField] private float explosionDelay;
    [SerializeField] private float explosionCount;
    [SerializeField] private float explosionRange;
    [SerializeField] private float explosionDamage;
    [SerializeField] private float fuzeTimer;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        TrailBlazerMask mask = Instantiate(maskItem, pPlayer.position, Quaternion.identity) as TrailBlazerMask;

        mask.cooldown = cooldown;
        mask.grenade = grenade;
        mask.explosionPrefab = explosionPrefab;
        mask.explosionCount = explosionCount;
        mask.explosionRange = explosionRange;
        mask.explosionDamage = explosionDamage;
        mask.fuzeTimer = fuzeTimer;
        mask.grenadeSpeed = grenadeSpeed;
        mask.explosionDelay = explosionDelay;
        
        return mask;
    }
}
