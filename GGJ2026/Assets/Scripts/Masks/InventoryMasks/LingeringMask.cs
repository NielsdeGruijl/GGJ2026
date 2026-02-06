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
            LingeringArea area = Instantiate(newMaskData.areaPrefab, transform.position, Quaternion.identity);
            
            if(newMaskData.debuff)
                area.SetDebuffData(newMaskData.debuff, playerMaskData.lingeringProcChance);
            
            area.Initialize(newMaskData.damagePerSecond, newMaskData.areaRadius, newMaskData.effectDuration);
            
            yield return new WaitForSeconds(newMaskData.cooldown);
        }
    }
}
