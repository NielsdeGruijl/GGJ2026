using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    MoveSpeed,
    AttackDamage,
    Custom
}

public enum StatModificationType
{
    flat,
    multiplier
}

public class DebuffSO : ScriptableObject
{
    public BaseDebuff debuffPrefab;
    
    [Header("Debuff stats")]
    public List<StatType> affectedStats;
    public StatModificationType statModificationType;
    public float modificationValue;
    public float duration;

    public virtual BaseDebuff MakeDebuff(Transform parent)
    {
        BaseDebuff debuff = Instantiate(debuffPrefab, parent);
        debuff.debuffData = this;
        return debuff;
    }
}
