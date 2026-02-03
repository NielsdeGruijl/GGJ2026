using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] TMP_Text chestName;

    [SerializeField] private List<MaskSO> masks;

    [SerializeField] private Mask maskWorldItemPrefab;

    [SerializeField] private float basePrice;
    
    [HideInInspector] public int price;
    
    
    private void Awake()
    {
        price = (int)(basePrice * DifficultyManager.instance.chestCostMult);

        chestName.gameObject.SetActive(false);
        
        DifficultyManager.instance.OnDifficultyChanged.AddListener(IncreasePrice);

        chestName.text = $"E (${price})";
    }

    public void Open()
    {
        int randomIndex = Random.Range(0, masks.Count);
        
        Mask maskObject = Instantiate(maskWorldItemPrefab, transform.position, Quaternion.identity);
        maskObject.Initialize(masks[randomIndex]);
        
        
        Destroy(gameObject);
    }

    private void IncreasePrice()
    {
        price = (int)(basePrice * DifficultyManager.instance.chestCostMult);
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
