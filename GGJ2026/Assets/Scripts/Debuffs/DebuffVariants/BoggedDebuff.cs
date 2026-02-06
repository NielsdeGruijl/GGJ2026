using System.Collections;
using UnityEngine;

public class BoggedDebuff : BaseDebuff
{
    [HideInInspector] public BoggedSO NewData;

    protected override void ApplyCustomDebuff(EntityDebuffData entityDebuffValues)
    {
        base.ApplyCustomDebuff(entityDebuffValues);

        StartCoroutine(SpawnBogCo());
    }

    private IEnumerator SpawnBogCo()
    {
        float timeElapsed = 0;
        while (timeElapsed < debuffData.duration)
        {
            LingeringArea area = ObjectPool.instance.Get(ObjectTypes.LingeringAreas).GetComponent<LingeringArea>();
            area.transform.position = transform.position;
            area.Initialize(NewData.damage, NewData.areaRadius, NewData.areaDuration);
            
            yield return new WaitForSeconds(NewData.cooldown);
            timeElapsed += NewData.cooldown;
        }
        
        Destroy(gameObject);
    }
}
