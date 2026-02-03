using UnityEngine;

[CreateAssetMenu(menuName = "Masks/HealSucc")]
public class HealSuckSO : MaskSO
{
    [SerializeField] private float lifeSteal;
    [SerializeField] private float healPercent;
    [SerializeField] private float succRange;
    [SerializeField] private float succDuration;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        HealSuckMask mask = Instantiate(maskItem, pPlayer.position, Quaternion.identity) as HealSuckMask;

        mask.cooldown = cooldown;
        mask.damage = lifeSteal;
        mask.healPrecent = healPercent;
        mask.succRange = succRange;
        mask.succDuration = succDuration;
        
        return mask;
    }
}
