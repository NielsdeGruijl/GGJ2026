using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] TMP_Text chestName;

    [SerializeField] private List<MaskSO> masks;

    [SerializeField] private Mask maskWorldItemPrefab;
    
    public int price;
    
    private void Awake()
    {
        chestName.gameObject.SetActive(false);

        chestName.text = $"E (${price})";
    }

    public void Open()
    {
        int randomIndex = Random.Range(0, masks.Count);
        
        Mask maskObject = Instantiate(maskWorldItemPrefab, transform.position, Quaternion.identity);
        maskObject.maskSO = masks[randomIndex];
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.canPurchaseChest = true;
            chestName.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.canPurchaseChest = true;
            chestName.gameObject.SetActive(false);
        }
    }
}
