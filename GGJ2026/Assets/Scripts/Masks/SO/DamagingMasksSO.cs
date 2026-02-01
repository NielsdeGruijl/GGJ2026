using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Damaing Masks")]
public class DamagingMasksSO : MaskSO
{
    [SerializeField] private float collisionDamagePerStack;
    
    public override InventoryMask MakeMask(Player pPlayer)
    {
        DamagingMasks maskObject = Instantiate(maskItem, pPlayer.transform.position, Quaternion.identity) as DamagingMasks;

        if (!maskObject)
            return null;
        
        maskObject.cooldown = cooldown;
        maskObject.damagePerStack = collisionDamagePerStack;
        
        return maskObject;
    }
}
