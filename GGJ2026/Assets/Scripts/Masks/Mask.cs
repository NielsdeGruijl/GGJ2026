using System;
using UnityEngine;

public class Mask : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public MaskSO maskSO;

    private void Awake()
    {
        if (maskSO)
            spriteRenderer.sprite = maskSO.maskSprite;
    }

    public void Initialize(MaskSO pMaskSO)
    {
        maskSO = pMaskSO;
        if (maskSO)
            spriteRenderer.sprite = maskSO.maskSprite;
    }
}
