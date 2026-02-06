using UnityEngine;

public class BaseDebuff : MonoBehaviour
{
    [HideInInspector] public DebuffSO debuffData;

    public void Apply(EntityDebuffData entityDebuffValues)
    {
        foreach (StatType affectedStat in debuffData.affectedStats)
        {
            switch (affectedStat)
            {
                case StatType.AttackDamage:
                    ApplyAttackDamageDebuff(entityDebuffValues);
                    break;
                case StatType.MoveSpeed:
                    ApplyMoveSpeedDebuff(entityDebuffValues);
                    break;
                case StatType.Custom:
                    ApplyCustomDebuff(entityDebuffValues);
                    break;
                default:
                    Debug.LogError("Debuff stat type was not found!");
                    break;
            }
        }
    }

    protected virtual void ApplyAttackDamageDebuff(EntityDebuffData entityDebuffValues)
    {
        
    }

    protected virtual void ApplyMoveSpeedDebuff(EntityDebuffData entityDebuffValues)
    {
        
    }

    protected virtual void ApplyCustomDebuff(EntityDebuffData entityDebuffValues)
    {
        
    }
}
