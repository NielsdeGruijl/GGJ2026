using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Masks/HomingMissile")]
public class HomingMissileSO : MaskSO
{
    [SerializeField] private ProjectileSO projectile;
    
    public override void Equip(Player pPlayer)
    {
        base.Equip(pPlayer);
    }

    public override void Unequip(Player pPlayer)
    {
        base.Unequip(pPlayer);
    }
}
