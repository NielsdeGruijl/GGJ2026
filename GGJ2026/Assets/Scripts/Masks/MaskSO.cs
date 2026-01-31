using UnityEngine;

public class MaskSO : ScriptableObject
{
    [SerializeField] public InventoryMask maskItem;
    
    [SerializeField] protected float cooldown;

    protected bool isActive = false;

    public virtual void Equip(Player pPlayer)
    {
        isActive = true;
    }

    public virtual void Unequip(Player pPlayer)
    {
        isActive = false;
    }
}
