using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaskManager : MonoBehaviour
{
    public List<InventoryMask> masks;

    [SerializeField] private int currentRingCapacity = 5;
    [SerializeField] private int RingCapacityIncrement = 2;
    [SerializeField] private float ringRadius = 1;
    [SerializeField] private float ringRadiusIncrement = 1;

    private int currentRingIndex = 0;
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

        if (pMask is SpeedMask)
        {
            SpeedMask newMask = pMask as SpeedMask;
            GetComponent<Player>().speedMult *= newMask.speedMult;
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
        maskCollisionDamage += damageIncrease;
        
        foreach (InventoryMask mask in masks)
        {
            if(!mask.collisionDamageEnabled)
                mask.collisionDamageEnabled = true;
            
            mask.collisionDamage = maskCollisionDamage;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Mask mask))
        {
            AddMask(mask.maskSO.MakeMask(transform));
            
            Destroy(other.gameObject);
        }
    }
}