using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    MoveSpeed,
    AttackDamage,
    AttackRange,
    AttackSpeed,
    Health,
    CritChance,
    CritBonus,
    Luck
}

public enum StatModificationType
{
    flat,
    multiplier,
    Custom
}

[System.Serializable]
public struct EntityStatModifier
{
    public StatType stat;
    public StatModificationType modificationType;
    public float value;
}

[System.Serializable]
public class EntityStat
{
    public StatType statType;
    public float baseValue;
    [HideInInspector] public float flatModifier;
    [HideInInspector] public float multiplier;
    [HideInInspector] public ValueEvent OnStatChanged = new();
}


[CreateAssetMenu(menuName = "BaseEntityData")]
public class BaseEntityDataSO : ScriptableObject
{
    public List<EntityStat> baseStats;
    
    public List<EntityStatModifier> LevelupModifiers;
}
