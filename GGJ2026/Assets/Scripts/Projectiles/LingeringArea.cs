using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LingeringArea : MonoBehaviour
{
    [SerializeField] private Transform areaSprite;

    [Space(10)]
    [SerializeField] private float interval = 0.2f;
    
    private List<HealthManager> targets = new();
    
    private float dps;
    private float radius;
    private float duration;

    private BoggedSO debuff;
    private float debuffProcChance;

    
    public void Initialize(float damagePerSecond, float areaRadius, float effectDuration)
    {
        dps = damagePerSecond;
        radius = areaRadius;
        duration = effectDuration;

        areaSprite.localScale = new Vector2(1, 1) * (radius * 2);

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
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage(dps * interval, true);
                    
                if (debuff && Random.Range(0f, 1f) < debuffProcChance)
                {
                    if (targets[i].TryGetComponent(out EntityDebuffManager debuffManager))
                    {
                        if(!debuffManager.HasDebuff(debuff))
                            debuffManager.ApplyDebuff(debuff);
                    }
                }
            }

            yield return new WaitForSeconds(interval);
            timeElapsed += interval;
        }
        
        ObjectPool.instance.PoolObject(ObjectTypes.LingeringAreas, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;
        
        if(other.TryGetComponent(out HealthManager manager))
            targets.Add(manager);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthManager manager))
        {
            if (targets.Contains(manager))
            {
                targets.Remove(manager);
            }
        }
    }
}
