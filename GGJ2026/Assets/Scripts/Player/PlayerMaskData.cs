using System.Collections.Generic;
using UnityEngine;

public class PlayerMaskData
{
    public float maskCollisionDamage = 0;
    
    public float playerMoveSpeedMult = 1;

    public StatModifierSO LingeringStatModifier;
    public float lingeringProcChance = 0;
    
    public List<string> maskKeys = new();
    public Dictionary<string, List<InventoryMask>> sortedMasks = new();
    public Dictionary<string, float> maskTypeDamageDealt = new();
}
