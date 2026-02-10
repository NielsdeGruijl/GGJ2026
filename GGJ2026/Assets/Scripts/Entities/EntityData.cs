using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EntityData
{
    private Dictionary<StatType, EntityStat> stats = new();
    private List<EntityStatModifier> LevelUpModifiers;

    public EntityData (BaseEntityDataSO baseData)
    {
        foreach (EntityStat stat in baseData.baseStats)
        {
            stats.Add(stat.statType, stat);
        }
        
        LevelUpModifiers = new List<EntityStatModifier>(baseData.LevelupModifiers);
    }

    public float GetStatValue(StatType type)
    {
        float multiplier = 1 + stats[type].multiplier * 0.01f;
        if (multiplier < 0.01f)
            multiplier = 0.01f;

        float value = stats[type].baseValue * multiplier + stats[type].flatModifier;

        if (value < 0.01)
            return 0.01f;

        return value;
    }

    public float GetModifiedDamage(float InDamage)
    {
        float multiplier = 1 + stats[StatType.AttackDamage].multiplier * 0.01f;
        if (multiplier < 0.01f)
            multiplier = 0.01f;

        float modifiedDamage = InDamage * multiplier + stats[StatType.AttackDamage].flatModifier;

        if (stats[StatType.CritChance] == null)
            return modifiedDamage;

        return modifiedDamage * GetCritDamage();
    }

    private float GetCritDamage()
    {
        float critChance = GetStatValue(StatType.CritChance);
        int critToApply = Mathf.FloorToInt(critChance / 100);
        if (Random.Range(0f, 100) < critChance)
            critToApply++;

        return critToApply * GetStatValue(StatType.CritBonus);
    }

    public void ApplyStatModifier(EntityStatModifier statModifier)
    {
        StatType typeToModify = statModifier.stat;
        
        switch (statModifier.modificationType)
        {
            case StatModificationType.flat:
                stats[typeToModify].flatModifier += statModifier.value;
                break;
            case StatModificationType.multiplier:
                stats[typeToModify].multiplier += statModifier.value;
                break;
            case StatModificationType.Custom:
                Debug.LogError("Custom modification type not implemented");
                break;
        }
        
        stats[typeToModify].OnStatChanged.Invoke(GetStatValue(typeToModify));
    }

    public void LevelUp()
    {
        foreach (EntityStatModifier statModifier in LevelUpModifiers)
        {
            ApplyStatModifier(statModifier);
        }
    }

    public ValueEvent GetStatEvent(StatType type)
    {
        return stats[type].OnStatChanged;
    }
}
