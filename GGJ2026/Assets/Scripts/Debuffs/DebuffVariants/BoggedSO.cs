using UnityEngine;

[CreateAssetMenu(menuName = "Debuffs/Bogged")]
public class BoggedSO : BaseDebuffSO
{
    [Header("Custom stats")] 
    public float areaRadius;
    public float areaDuration;

    public override BaseDebuff MakeDebuff()
    {
        return new BoggedDebuff(debuffTag, duration, areaRadius, areaDuration);
    }
}
