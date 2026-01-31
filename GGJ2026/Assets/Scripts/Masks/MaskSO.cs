using UnityEngine;

public class MaskSO : ScriptableObject
{
    [SerializeField] public InventoryMask maskItem;
    
    [SerializeField] protected float cooldown;

    protected bool isActive = false;

    public virtual InventoryMask MakeMask(Player pPlayer)
    {
        return Instantiate(maskItem);
    }
    
    public virtual void Equip(Player pPlayer)
    {
        isActive = true;
    }
}
