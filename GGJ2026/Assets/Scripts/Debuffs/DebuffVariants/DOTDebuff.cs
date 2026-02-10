using UnityEngine;

public class DOTDebuff : BaseDebuff
{
    private float damagePerSecond;

    private HealthManager targetHealth;
    
    public DOTDebuff(string debuffTag, float duration, float damagePerSecond)
        : base(debuffTag, duration)
    {
        this.damagePerSecond = damagePerSecond;
    }

    public override void Apply(EntityDebuffManager debuffTarget)
    {
        base.Apply(debuffTarget);
        targetHealth = debuffTarget.GetComponent<HealthManager>();
    }

    public override void Tick(float interval)
    {
        base.Tick(interval);
        
        targetHealth.ApplyDamage(new HitInfo(damagePerSecond * interval));
    }
}
