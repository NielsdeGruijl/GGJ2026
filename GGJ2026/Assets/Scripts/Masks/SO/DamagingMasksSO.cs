using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Damaing Masks")]
public class DamagingMasksSO : MaskSO
{
    public float collisionDamagePerStack;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        DamagingMask mask = base.MakeMask(pPlayer) as DamagingMask;

        if (!mask)
            return null;
        
        return mask;
    }
}
