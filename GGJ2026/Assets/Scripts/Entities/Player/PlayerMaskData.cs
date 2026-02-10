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

    public BaseDebuffSO LingeringBaseDebuff;
    public float lingeringProcChance = 0;
    
    public List<string> maskKeys = new();
    public Dictionary<string, List<InventoryMask>> sortedMasks = new();
    public Dictionary<string, float> maskTypeDamageDealt = new();
}
