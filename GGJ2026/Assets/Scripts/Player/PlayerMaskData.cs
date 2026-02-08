using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CollectEvent : UnityEvent<int>
{
}

public class PlayerMaskData
{
    public float maskCollisionDamage = 0;

    public float playerMoveSpeedFlat = 0;
    public float playerMoveSpeedMult = 1;

    public float playerMagnetRange;
    public float playerMagnetForce;

    public float playerDamageMult = 1;

    public float luck = 0;

    public UnityEvent OnCooldownChanged = new();
    public float cooldownMult = 1;

    public BaseDebuffSO LingeringBaseDebuff;
    public float lingeringProcChance = 0;
    
    public List<string> maskKeys = new();
    public Dictionary<string, List<InventoryMask>> sortedMasks = new();
    public Dictionary<string, float> maskTypeDamageDealt = new();
}
