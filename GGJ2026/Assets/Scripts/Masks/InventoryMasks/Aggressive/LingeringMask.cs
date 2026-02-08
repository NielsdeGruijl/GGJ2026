using System.Collections;
using UnityEngine;

public class LingeringMask : InventoryMask
{
    private LingeringMaskSO newData;
    
    public override void Activate(PlayerMaskData pPlayerMaskData)
    {
        base.Activate(pPlayerMaskData);
        
        newData = maskData as LingeringMaskSO;
        
        StartCoroutine(SpawnLingeringAreaCo());
    }

    private IEnumerator SpawnLingeringAreaCo()
    {
        while (true)
        {
            LingeringArea area = ObjectPool.instance.Get(ObjectTypes.LingeringAreas).GetComponent<LingeringArea>();
            area.transform.position = transform.position;
            
            if(playerMaskData.LingeringBaseDebuff)
                area.SetDebuffData(playerMaskData.LingeringBaseDebuff as BoggedSO, playerMaskData.lingeringProcChance);
            
            area.Initialize(newData.areaRadius, newData.effectDuration);
            
            yield return waitForCooldown;
        }
    }
}
