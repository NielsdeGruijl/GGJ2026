using UnityEngine;

[CreateAssetMenu(menuName = "Masks/SpeedMask")] 
public class SpeedMaskSO : MaskSO
{
    [SerializeField] private float speedMult = 1;

    public override InventoryMask MakeMask(Player pPlayer)
    {
        SpeedMask mask = Instantiate(maskItem, pPlayer.transform.position, Quaternion.identity) as SpeedMask;
        mask.speedMult = speedMult;
        
        return mask;
    }
}
