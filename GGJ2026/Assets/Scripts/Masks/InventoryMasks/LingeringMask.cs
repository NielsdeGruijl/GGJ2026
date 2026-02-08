using System.Collections;
using UnityEngine;

public class LingeringMask : InventoryMask
{
    private LingeringMaskSO newMaskData;
    
    public override void Activate(PlayerMaskData pPlayerMaskData)
    {
        base.Activate(pPlayerMaskData);
        
        newMaskData = maskData as LingeringMaskSO;
        
        StartCoroutine(SpawnLingeringAreaCo());
    }

    private IEnumerator SpawnLingeringAreaCo()
    {
        while (true)
        {
            LingeringArea area = ObjectPool.instance.Get(ObjectTypes.LingeringAreas).GetComponent<LingeringArea>();//Instantiate(newMaskData.areaPrefab, transform.position, Quaternion.identity);
            area.transform.position = transform.position;
            
            if(playerMaskData.LingeringBaseDebuff)
                area.SetDebuffData(playerMaskData.LingeringBaseDebuff as BoggedSO, playerMaskData.lingeringProcChance);
            
            area.Initialize(newMaskData.areaRadius, newMaskData.effectDuration);
            
            yield return waitForCooldown;
        }
    }
}
