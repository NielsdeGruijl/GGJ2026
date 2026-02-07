using UnityEngine;

[CreateAssetMenu(menuName = "Debuffs/Bogged")]
public class BoggedSO : StatModifierSO
{
    public float cooldown;
    
    [Header("Custom stats")] 
    public LingeringArea bogPrefab;
    public float areaRadius;
    public float areaDuration;
    [Space(10)]
    public float damage;

    public override BaseDebuff MakeDebuff()
    {
        if(base.MakeDebuff() is not BoggedDebuff debuff)
           return null;
        
       debuff.NewData = this;

       return debuff;
    }
}
