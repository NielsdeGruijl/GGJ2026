using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MaskDataUIElement : MonoBehaviour
{
    [SerializeField] private TMP_Text maskDamageText;
    [SerializeField] private TMP_Text maskCountText;
    [SerializeField] private Image maskImage;

    public void Initialize(string maskDamage, string maskCount, Sprite maskSprite)
    {
        maskDamageText.text = maskDamage;
        maskCountText.text = maskCount;
     
        maskImage.sprite = maskSprite;
    }

}
