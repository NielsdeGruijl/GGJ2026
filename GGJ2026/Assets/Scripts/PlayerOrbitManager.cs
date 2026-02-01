using System.Collections.Generic;
using UnityEngine;

public class PlayerOrbitManager : MonoBehaviour
{
    public List<InventoryMask> masks;

    [SerializeField] private int currentRingIndex = 0;
    [SerializeField] private int currentRingCapacity = 5;
    [SerializeField] private int RingCapacityIncrement = 2;
    [SerializeField] private float ringRadius = 1;
    [SerializeField] private float ringRadiusIncrement = 1;

    private int currentRingItemNum = 0;

    private float maskCollisionDamage = 0;
    
    public void AddMask(InventoryMask pMask)
    {
        if (!pMask)
            return;
        
        pMask.numInRing = currentRingItemNum;
        pMask.ringCapacity = currentRingCapacity;
        pMask.targetRadius = ringRadius;
        
        pMask.transform.SetParent(transform);
        
        pMask.Activate();
        masks.Add(pMask);
        
        currentRingItemNum++;
        
        if (pMask is DamagingMasks)
        {
            DamagingMasks newMask = pMask as DamagingMasks;
            IncreaseCollisionDamage(newMask.damagePerStack);
        }
        
        if (currentRingItemNum >= currentRingCapacity)
            CreateNewOrbitRing();
    }

    private void CreateNewOrbitRing()
    {
        currentRingIndex++;
        ringRadius += ringRadiusIncrement;
        currentRingCapacity += RingCapacityIncrement;
        currentRingItemNum = 0;
    }

    private void IncreaseCollisionDamage(float damageIncrease)
    {
        foreach (InventoryMask mask in masks)
        {
            if(!mask.collisionDamageEnabled)
                mask.collisionDamageEnabled = true;
            
            maskCollisionDamage += damageIncrease;
            
            mask.collisionDamage = maskCollisionDamage;
        }
    }
}