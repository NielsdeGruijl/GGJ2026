using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaskManager : MonoBehaviour
{
    [SerializeField] private int currentRingCapacity = 5;
    [SerializeField] private float RingCapacityMult = 1.5f;
    [SerializeField] private float ringRadius = 1;
    [SerializeField] private float ringRadiusIncrement = 1;

    [SerializeField] private MaskHUDContainer maskHUD;
    
    public PlayerMaskData playerMaskData;
    public EntityData playerData;

    private int currentRingIndex = 0;
    private int currentRingItemNum = 0;

    private void Awake()
    {
        playerMaskData = new PlayerMaskData();
        
        GetComponent<Player>().SetMaskData(playerMaskData);
    }

    public void AddMask(InventoryMask pMask)
    {
        if (!pMask)
            return;
        
        UpdateEquippedMasks(pMask);
        
        pMask.Initialize(currentRingItemNum, currentRingCapacity, ringRadius);
        pMask.transform.SetParent(transform);
        
        pMask.Activate(playerMaskData, this);
        
        currentRingItemNum++;
        
        if (currentRingItemNum >= currentRingCapacity)
            CreateNewOrbitRing();
    }

    private void CreateNewOrbitRing()
    {
        currentRingIndex++;
        ringRadius += ringRadiusIncrement;
        currentRingCapacity = Mathf.FloorToInt(currentRingCapacity * RingCapacityMult);
        currentRingItemNum = 0;
    }
    
    private void UpdateEquippedMasks(InventoryMask newMask)
    {
        maskHUD.AddMask(newMask.maskData);
        
        if (playerMaskData.sortedMasks.ContainsKey(newMask.maskData.maskName))
        {
            playerMaskData.sortedMasks[newMask.maskData.maskName].Add(newMask);
            return;
        }

        playerMaskData.maskKeys.Add(newMask.maskData.maskName);
        playerMaskData.maskTypeDamageDealt.Add(newMask.maskData.maskName, 0);
        playerMaskData.sortedMasks.Add(newMask.maskData.maskName, new List<InventoryMask>());
        playerMaskData.sortedMasks[newMask.maskData.maskName].Add(newMask);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Mask mask))
        {
            Debug.Log("Pickup mask");
            AddMask(mask.maskSO.MakeMask(transform));
            
            Destroy(other.gameObject);
        }
    }
}