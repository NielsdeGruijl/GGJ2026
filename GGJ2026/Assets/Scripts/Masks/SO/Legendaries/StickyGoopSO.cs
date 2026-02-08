using UnityEngine;

[CreateAssetMenu(menuName = "Masks/Legendaries/StickyGoop")]
public class StickyGoopSO : MaskSO
{
    public float procChance;
    public BaseDebuffSO BoggedDebuff;

    public override InventoryMask MakeMask(Transform player)
    {
        StickyGoop mask = base.MakeMask(player) as StickyGoop;

        if (!mask)
            return null;

        return mask;
    }
}
