using System.Collections.Generic;
using UnityEngine;

public class EntityDebuffManager : MonoBehaviour
{
    private EntityDebuffData debuffValues;

    private List<BaseDebuff> activeDebuffs = new();
    
    public void ApplyDebuff(StatModifierSO statModifierToApply)
    {
        BaseDebuff debuff = statModifierToApply.MakeDebuff();
        debuff.Apply(debuffValues);
        activeDebuffs.Add(debuff);
    }

    public bool HasDebuff(StatModifierSO statModifierToCheck)
    {
        int count = 0;
        foreach (BaseDebuff debuff in activeDebuffs)
        {
            if (debuff.StatModifierData == statModifierToCheck)
                return true;
        }

        return false;
    }
    
    public bool HasDebuff(StatModifierSO statModifierToCheck, out int stacks)
    {
        int count = 0;
        foreach (BaseDebuff debuff in activeDebuffs)
        {
            if (debuff.StatModifierData == statModifierToCheck)
            {
                count++;
            }                
        }

        if (count > 0)
        {
            stacks = count;
            return true;
        }

        stacks = 0;
        return false;
    }
}
