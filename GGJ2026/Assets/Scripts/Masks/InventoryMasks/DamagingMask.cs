using UnityEngine;

public class DamagingMask : InventoryMask
{
    [HideInInspector] public float damagePerStack;

    private DamagingMasksSO newMaskData;
    
    public override void Activate(PlayerMaskData playerMaskData)
    {
        base.Activate(playerMaskData);
        
        newMaskData = maskData as DamagingMasksSO;
        
        OnAuraDamage.AddListener(UpdateDamageDealt);
        
        playerMaskData.maskCollisionDamage += newMaskData.collisionDamagePerStack;
    }
}
