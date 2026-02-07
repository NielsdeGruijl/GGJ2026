using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    MoveSpeed,
    AttackDamage,
    Health,
    CritChance,
    CritBonus,
    Luck
}

public enum StatModificationType
{
    flat,
    multiplier,
    DOT,
    Custom
}

public class DebuffSO : ScriptableObject
{
    public BaseDebuff debuffPrefab;
    
    [Header("Debuff stats")]
    public List<StatType> affectedStats;
    public StatModificationType statModificationType;
    public float modificationValue;
    public float duration;

    public virtual BaseDebuff MakeDebuff()
    {
        BaseDebuff debuff = new BaseDebuff();
        debuff.debuffData = this;
        return debuff;
    }
}
