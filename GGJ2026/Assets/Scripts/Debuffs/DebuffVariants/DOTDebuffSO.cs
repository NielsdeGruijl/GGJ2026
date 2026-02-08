using UnityEngine;

[CreateAssetMenu(menuName = "Debuffs/DOT")]
public class DOTDebuffSO : BaseDebuffSO
{
    [Header("custom stats")] 
    public float damagePerSecond;

    public override BaseDebuff MakeDebuff()
    {
        return new DOTDebuff(debuffTag, duration, damagePerSecond);
    }
}
