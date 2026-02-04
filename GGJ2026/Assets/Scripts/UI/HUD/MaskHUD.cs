using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaskHUD : MonoBehaviour
{
    [SerializeField] private Image maskImage;
    [SerializeField] private TMP_Text maskCountText;

    private MaskSO maskData;

    private int maskCount;

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
}
