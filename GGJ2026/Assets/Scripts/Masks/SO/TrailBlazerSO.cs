using UnityEngine;

[CreateAssetMenu(menuName = "Masks/TrailBlazer")]
public class TrailBlazerSO : MaskSO
{
    [SerializeField] private GameObject grenade;
    [SerializeField] private Explosion explosionPrefab;
    [SerializeField] private float explosionCount;
    [SerializeField] private float explosionRange;
    [SerializeField] private float explosionDamage;

    public override InventoryMask MakeMask(Player pPlayer)
    {
        TrailBlazerMask mask = Instantiate(maskItem, pPlayer.transform.position, Quaternion.identity) as TrailBlazerMask;

        mask.cooldown = cooldown;
        mask.grenade = grenade;
        mask.explosionPrefab = explosionPrefab;
        mask.explosionCount = explosionCount;
        mask.explosionRange = explosionRange;
        mask.explosionDamage = explosionDamage;
        
        return mask;
    }
}
