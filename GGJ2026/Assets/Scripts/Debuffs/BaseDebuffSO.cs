using UnityEngine;

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
