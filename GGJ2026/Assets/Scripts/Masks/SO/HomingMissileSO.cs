using UnityEngine;

[CreateAssetMenu(menuName = "Masks/HomingMissile")]
public class HomingMissileSO : MaskSO
{
    [SerializeField] protected float damage;
    [SerializeField] protected float damageMultiplier;

    public override InventoryMask MakeMask(Player pPlayer)
    {
        MissileMask mask = Instantiate(maskItem, pPlayer.transform.position, Quaternion.identity) as MissileMask;

        if (!mask)
            return null;
        
        mask.cooldown = cooldown;
        mask.damage = damage * damageMultiplier;

        return mask;
    }
    
    public override void Equip(Player pPlayer)
    {
        base.Equip(pPlayer);
    }
}
