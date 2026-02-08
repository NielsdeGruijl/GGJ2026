using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Aggressive/LingeringMask")]
public class LingeringMaskSO : MaskSO
{
    public float areaRadius;
    public float effectDuration;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        LingeringMask mask = base.MakeMask(pPlayer) as LingeringMask;

        if (!mask)
            return null;

        return mask;
    }
}
