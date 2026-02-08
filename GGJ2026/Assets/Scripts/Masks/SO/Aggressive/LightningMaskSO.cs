using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Aggressive/Lightning")]
public class LightningMaskSO : MaskSO
{
    public Lightning lightningPrefab;
    public int totalBounces;
    public float damagePerBounce;
    
    public override InventoryMask MakeMask(Transform pPlayer)
    {
        LightningMask mask = base.MakeMask(pPlayer) as LightningMask;

        if (!mask)
            return null;

        mask.maskData = this;

        return mask;
    }
}
