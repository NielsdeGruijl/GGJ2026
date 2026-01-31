using UnityEngine;

public abstract class MaskSO : ScriptableObject
{
    [SerializeField] protected float cooldown;
    
    public abstract void Equip(Player pPlayer);
    public abstract void Unequip(Player pPlayer);
}
