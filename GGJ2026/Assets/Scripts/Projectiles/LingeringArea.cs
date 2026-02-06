using System.Collections;
using UnityEngine;

public class LingeringArea : MonoBehaviour
{
    [SerializeField] private Transform areaSprite;
    
    private float dps;
    private float radius;

    private float duration;

    private float interval = 0.2f;
    
    private BoggedSO debuff;
    private float debuffProcChance;

    public void Initialize(float damagePerSecond, float areaRadius, float effectDuration)
    {
        dps = damagePerSecond;
        radius = areaRadius;
        duration = effectDuration;

        areaSprite.localScale *= radius * 2;

        StartCoroutine(DealDamageCo());
    }

    public void SetDebuffData(BoggedSO debuff, float debuffProcChance)
    {
        this.debuff = debuff;
        this.debuffProcChance = debuffProcChance;
    }

    private IEnumerator DealDamageCo()
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            foreach (Collider2D target in Physics2D.OverlapCircleAll(transform.position, radius))
            {
                if (target.CompareTag("Player"))
                    continue;
                
                if (target.TryGetComponent(out HealthManager manager))
                {
                    manager.TakeDamage(dps * interval, true);
                    
                    if (debuff && Random.Range(0f, 1f) < debuffProcChance)
                    {
                        if (target.TryGetComponent(out EntityDebuffManager debuffManager))
                        {
                            if(!debuffManager.HasDebuff(debuff))
                                debuffManager.ApplyDebuff(debuff);
                        }
                    }
                }
            }

            timeElapsed += interval;
            yield return new WaitForSeconds(interval);
        }
        
        Destroy(gameObject);
    }
}
