using UnityEngine;

public class SpeedMask : InventoryMask
{
    private SpeedMaskSO newMaskData;
    
    public override void Activate(PlayerMaskData playerMaskData)
    {
        base.Activate(playerMaskData);
        
        newMaskData = maskData as SpeedMaskSO;
        
        playerMaskData.playerMoveSpeedMult *= newMaskData.speedMult;
    }
}
