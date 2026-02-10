using UnityEngine;

public class DamagingMask : InventoryMask
{
    [HideInInspector] public float damagePerStack;

    private DamagingMasksSO newData;
    
    protected override void ActivateMask()
    {
        base.ActivateMask();
        newData = maskData as DamagingMasksSO;

        OnAuraDamage.AddListener(UpdateDamageDealt);
        
        //playerMaskData.maskCollisionDamage += newData.collisionDamagePerStack;
    }
}
