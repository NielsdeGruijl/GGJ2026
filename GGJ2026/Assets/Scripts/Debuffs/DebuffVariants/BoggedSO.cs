using UnityEngine;

[CreateAssetMenu(menuName = "Debuffs/Bogged")]
public class BoggedSO : DebuffSO
{
    public float cooldown;
    
    [Header("Custom stats")] 
    public LingeringArea bogPrefab;
    public float areaRadius;
    public float areaDuration;
    [Space(10)]
    public float damage;

    public override BaseDebuff MakeDebuff(Transform parent)
    {
       BoggedDebuff debuff = base.MakeDebuff(parent) as BoggedDebuff;

       if(!debuff)
           return null;
       
       debuff.NewData = this;

       return debuff;
    }
}
