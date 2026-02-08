using UnityEngine;

public class DamagingMask : InventoryMask
{
    [HideInInspector] public float damagePerStack;

    private DamagingMasksSO newData;
    
    public override void Activate(PlayerMaskData playerMaskData)
    {
        base.Activate(playerMaskData);
        
        newData = maskData as DamagingMasksSO;

        OnAuraDamage.AddListener(UpdateDamageDealt);
        
        playerMaskData.maskCollisionDamage += newData.collisionDamagePerStack;
    }
}
