using UnityEngine;

public class MaskSO : ScriptableObject
{
    [Header("Base mask values")]
    public string maskName;
    public InventoryMask maskItem;
    public Sprite maskSprite;
    
    [Header("Mask stats")]
    public float cooldown;

    protected bool isActive = false;

    public virtual InventoryMask MakeMask(Transform pPlayer)
    {
        InventoryMask mask = Instantiate(maskItem, pPlayer.transform.position, Quaternion.identity);
        mask.maskData = this;
        return mask;
    }
    
    public virtual void Equip(Player pPlayer)
    {
        isActive = true;
    }
}
