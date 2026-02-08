using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LingeringArea : MonoBehaviour
{
    [SerializeField] private Transform areaSprite;
    [SerializeField] private DOTDebuffSO baseDebuff;
    
    private List<EntityDebuffManager> targets = new();
    
    private float radius;
    private float duration;

    private BoggedSO legendaryDebuff;
    private float debuffProcChance;

    private Vector2 baseScale = new Vector2(1, 1);

    private void OnDisable()
    {
        legendaryDebuff = null;
        debuffProcChance = 0;
        
        targets.Clear();
    }

    public void Initialize(float areaRadius, float duration)
    {
        radius = areaRadius;
        this.duration = duration;

        transform.localScale = baseScale * (radius * 2);

        StartCoroutine(LifeTimeCo());
    }

    public void SetDebuffData(BoggedSO debuff, float debuffProcChance)
    {
        legendaryDebuff = debuff;
        this.debuffProcChance = debuffProcChance;
    }

    private IEnumerator LifeTimeCo()
    {
        yield return new WaitForSeconds(duration);
        
        ObjectPool.instance.PoolObject(ObjectTypes.LingeringAreas, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        if (other.TryGetComponent(out EntityDebuffManager debuffManager))
        {
            targets.Add(debuffManager);
            debuffManager.ApplyDebuff(baseDebuff);
            
            if (legendaryDebuff && Random.Range(0f, 100) < debuffProcChance)
            {
                debuffManager.ApplyDebuff(legendaryDebuff);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out EntityDebuffManager manager))
        {
            if (targets.Contains(manager))
            {
                targets.Remove(manager);
            }
        }
    }
}
