using System.Collections.Generic;
using UnityEngine;

public class PlayerOrbitManager : MonoBehaviour
{
    public List<InventoryMask> masks;

    private int currentRingIndex = 0;
    private int currentRingCapacity = 5;

    private float ringRadius = 1.5f;

    private int currentRingItemNum;
    
    public void AddMask(InventoryMask pMask)
    {
        InventoryMask mask = Instantiate(pMask, transform.position, Quaternion.identity);
        
        mask.numInRing = currentRingItemNum;
        mask.ringCapacity = currentRingCapacity;
        mask.targetRadius = ringRadius;
        
        mask.transform.SetParent(transform);
        
        mask.Activate();
        masks.Add(mask);
        
        currentRingItemNum++;
    }
}