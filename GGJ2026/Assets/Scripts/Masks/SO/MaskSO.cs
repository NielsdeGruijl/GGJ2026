using UnityEngine;

public class MaskSO : ScriptableObject
{
    [Header("Base mask values")]
    public string maskName;
    public InventoryMask maskItem;
    public GameObject maskPrefab;
    public Sprite maskSprite;
    
    [Header("Mask stats")]
    public float cooldown;

    public virtual InventoryMask MakeMask(Transform pPlayer)
    {
        InventoryMask mask = Instantiate(maskItem, pPlayer.transform.position, Quaternion.identity);
        mask.maskData = this;
        return mask;
    }
}
