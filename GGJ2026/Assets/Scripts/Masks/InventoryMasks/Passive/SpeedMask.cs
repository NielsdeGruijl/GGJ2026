using UnityEngine;

public class SpeedMask : InventoryMask
{
    private SpeedMaskSO newData;
    
    public override void Activate(PlayerMaskData playerMaskData)
    {
        base.Activate(playerMaskData);
        
        newData = maskData as SpeedMaskSO;
        
        playerMaskData.playerMoveSpeedMult += newData.speedMult * 0.01f;
    }
}
