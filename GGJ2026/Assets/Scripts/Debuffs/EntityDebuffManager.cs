using System.Collections.Generic;
using UnityEngine;

public class EntityDebuffManager : MonoBehaviour
{
    private EntityDebuffData debuffValues;

    private List<BaseDebuff> activeDebuffs = new();
    
    public void ApplyDebuff(DebuffSO debuffToApply)
    {
        BaseDebuff debuff = debuffToApply.MakeDebuff(transform);
        debuff.Apply(debuffValues);
        activeDebuffs.Add(debuff);
    }

    public bool HasDebuff(DebuffSO debuffToCheck)
    {
        int count = 0;
        foreach (BaseDebuff debuff in activeDebuffs)
        {
            if (debuff.debuffData == debuffToCheck)
                return true;
        }

        return false;
    }
    
    public bool HasDebuff(DebuffSO debuffToCheck, out int stacks)
    {
        int count = 0;
        foreach (BaseDebuff debuff in activeDebuffs)
        {
            if (debuff.debuffData == debuffToCheck)
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
