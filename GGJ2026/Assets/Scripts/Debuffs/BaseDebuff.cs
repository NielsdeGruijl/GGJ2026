using UnityEngine;

public class BaseDebuff
{
    [HideInInspector] public StatModifierSO StatModifierData;

    public void Apply(EntityDebuffData entityDebuffValues)
    {
        foreach (StatType affectedStat in StatModifierData.affectedStats)
        {

        }
    }
}
