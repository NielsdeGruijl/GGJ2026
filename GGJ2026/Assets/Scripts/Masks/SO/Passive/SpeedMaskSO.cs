using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Passive/SpeedMask")] 
public class SpeedMaskSO : MaskSO
{
    public float speedMult = 1;

    public override InventoryMask MakeMask(Transform pPlayer)
    {
        SpeedMask mask = base.MakeMask(pPlayer) as SpeedMask;

        if (!mask)
            return null;
        
        //mask.speedMult = speedMult;
        
        return mask;
    }
}
