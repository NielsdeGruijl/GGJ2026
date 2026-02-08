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

public enum StackingRule
{
    Refresh,
    Stack,
    Ignore
}

public class BaseDebuffSO : ScriptableObject
{
    [Header("Debuff stats")] 
    public string debuffTag;
    public float duration;

    public StackingRule stackingRule;

    public virtual BaseDebuff MakeDebuff()
    {
        return new BaseDebuff(debuffTag, duration);;
    }
}
