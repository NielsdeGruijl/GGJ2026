using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Chest : MonoBehaviour
{
    [SerializeField] TMP_Text pricePopup;
    [SerializeField] private Mask maskWorldItemPrefab;

    public ChestSO data;
    
    [HideInInspector] public int price;

    public UnityEvent OnOpen;
    
    private void OnEnable()
    {
        price = (int)data.basePrice;

        pricePopup.gameObject.SetActive(false);
        
        pricePopup.text = $"E (${price})";
    }

    public void Open(float playerLuck)
    {
        int itemCount = 1;
        itemCount += Mathf.FloorToInt(playerLuck / 100);
        
        if (Random.Range(0f, 100) < playerLuck % 100)
            itemCount++;
        
        for (int i = 0; i < itemCount; i++)
        {
            int randomIndex = Random.Range(0, data.masks.Count);
        
            Mask maskObject = Instantiate(maskWorldItemPrefab, transform.position, Quaternion.identity);
            maskObject.Initialize(data.masks[randomIndex]);
        }

        OnOpen.Invoke();
        
        Destroy(gameObject);
    }

    public void IncreasePrice(int priceIncreaseMagnitude)
    {
        price = (int)((data.basePrice + data.flatPriceIncrease * priceIncreaseMagnitude) * Mathf.Pow(data.multPriceIncrease, priceIncreaseMagnitude));
        pricePopup.text = $"E (${price})";
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pricePopup.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pricePopup.gameObject.SetActive(false);
        }
    }
}
