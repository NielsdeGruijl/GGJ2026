using UnityEngine;

public class MagnetMask : InventoryMask
{
    private MagnetMaskSO newData;
    
    public override void Activate(PlayerMaskData pPlayerMaskData)
    {
        base.Activate(pPlayerMaskData);
        
        newData = maskData as MagnetMaskSO;

        if (!newData)
        {
            Debug.LogError("New data is not of type MagnetMaskSO");
            return;
        }
        
        playerMaskData.playerMagnetRange += newData.magnetRangeIncrease;
        playerMaskData.playerMagnetForce += newData.magnetForceIncrease;
    }
}
