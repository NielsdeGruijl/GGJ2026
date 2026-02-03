using UnityEngine;

[CreateAssetMenu(menuName = "Masks/SpeedMask")] 
public class SpeedMaskSO : MaskSO
{
    [SerializeField] private float speedMult = 1;

    public override InventoryMask MakeMask(Transform pPlayer)
    {
        SpeedMask mask = Instantiate(maskItem, pPlayer.position, Quaternion.identity) as SpeedMask;
        mask.speedMult = speedMult;
        
        return mask;
    }
}
