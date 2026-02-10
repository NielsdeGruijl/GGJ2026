using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Aggressive/Damaing Masks")]
public class DamagingMasksSO : MaskSO
{
    public float collisionDamagePerStack;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        TitaniumMask mask = base.MakeMask(pPlayer) as TitaniumMask;

        if (!mask)
            return null;
        
        return mask;
    }
}
