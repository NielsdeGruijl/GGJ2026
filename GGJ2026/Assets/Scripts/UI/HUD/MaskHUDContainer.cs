using System.Collections.Generic;
using UnityEngine;

public class MaskHUDContainer : MonoBehaviour
{
    [SerializeField] private MaskHUD maskHUDPrefab;
    
    private Dictionary<string, MaskHUD> maskElements = new();
    
    public void AddMask(MaskSO maskData)
    {
        string tag = maskData.maskName;
        if (maskElements.ContainsKey(tag))
        {
            maskElements[tag].AddMask();
            return;
        }
        
        maskElements.Add(tag, Instantiate(maskHUDPrefab, transform));
        MaskHUD element = maskElements[tag];
        element.transform.SetAsFirstSibling();
        element.Initialize(maskData);
    }
}
