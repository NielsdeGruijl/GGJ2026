using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MaskHUD : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image maskImage;
    [SerializeField] private TMP_Text maskCountText;

    private MaskSO maskData;

    private int maskCount;

    private Tooltip tooltip;

    public void Initialize(MaskSO pMaskData)
    {
        maskData = pMaskData;

        maskImage.sprite = maskData.maskSprite;
        AddMask();
    }

    public void AddMask()
    {
        maskCount++;
        maskCountText.text = maskCount.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip = ToolTipManager.instance.CreateUITooltip(transform.position, maskData.maskName, maskData.maskDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(tooltip.gameObject);
    }
}
