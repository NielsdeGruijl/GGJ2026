using UnityEngine;

public class MaskSO : ScriptableObject
{
    public string maskName;
    public InventoryMask maskItem;
    public Sprite maskSprite;
    
    [SerializeField] protected float cooldown;

    protected bool isActive = false;

    public virtual InventoryMask MakeMask(Transform pPlayer)
    {
        return Instantiate(maskItem);
    }
    
    public virtual void Equip(Player pPlayer)
    {
        isActive = true;
    }
}
