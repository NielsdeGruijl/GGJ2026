using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    [SerializeField] TMP_Text chestName;

    [SerializeField] private List<MaskSO> masks;

    [SerializeField] private Mask maskWorldItemPrefab;

    [SerializeField] private float basePrice;
    
    [HideInInspector] public int price;

    public float weight;

    public UnityEvent OnOpen;
    
    private void Awake()
    {
        price = (int)(basePrice * DifficultyManager.instance.chestCostMult);

        chestName.gameObject.SetActive(false);
        
        chestName.text = $"E (${price})";
    }

    public void Open(float playerLuck)
    {
        int itemCount = 1;
        itemCount += Mathf.FloorToInt(playerLuck / 100);
        
        Debug.Log("Items: " + itemCount);
        
        if (Random.Range(0f, 100) < playerLuck % 100)
            itemCount++;
        
        Debug.Log("Items: " + itemCount);
        
        for (int i = 0; i < itemCount; i++)
        {
            int randomIndex = Random.Range(0, masks.Count);
        
            Mask maskObject = Instantiate(maskWorldItemPrefab, transform.position, Quaternion.identity);
            maskObject.Initialize(masks[randomIndex]);
        }

        OnOpen.Invoke();
        
        Destroy(gameObject);
    }

    public void IncreasePrice(float newMult)
    {
        price = (int)(basePrice * newMult);
        chestName.text = $"E (${price})";
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            chestName.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            chestName.gameObject.SetActive(false);
        }
    }
}
