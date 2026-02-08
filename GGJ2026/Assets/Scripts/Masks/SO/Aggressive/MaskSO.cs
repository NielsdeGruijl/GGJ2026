using UnityEngine;

public class MaskSO : ScriptableObject
{
    [Header("Base mask values")]
    public string maskName;
    public string maskDescription;
    public InventoryMask maskItem;
    public Sprite maskSprite;
    
    [Header("Mask stats")]
    public float cooldown;

    public virtual InventoryMask MakeMask(Transform pPlayer)
    {
        InventoryMask mask = Instantiate(maskItem, pPlayer.position, Quaternion.identity);
        mask.maskData = this;
        return mask;
    }
}
