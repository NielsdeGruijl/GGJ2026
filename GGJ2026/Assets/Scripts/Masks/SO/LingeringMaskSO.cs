using UnityEngine;

[CreateAssetMenu(menuName = "Masks/LingeringMask")]
public class LingeringMaskSO : MaskSO
{
    public LingeringArea areaPrefab;
    
    public float damagePerSecond;
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
