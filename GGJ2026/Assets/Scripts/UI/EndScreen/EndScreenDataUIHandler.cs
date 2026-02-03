using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenDataUIHandler : MonoBehaviour
{
    [SerializeField] private Transform maskDataUIContainer;
    [SerializeField] private MaskDataUIElement maskDataUIPrefab;

    [SerializeField] private TMP_Text timeSurvivedText;

    private void Start()
    {
        if (!SessionData.instance)
            return;

        SessionData.instance.maskData.Sort((x, y) => y.damageDealt.CompareTo(x.damageDealt));
        
        foreach (MaskData data in SessionData.instance.maskData)
        {
            MaskDataUIElement maskUI = Instantiate(maskDataUIPrefab, maskDataUIContainer);
            maskUI.Initialize(data.damageDealt.ToString("0.0"), data.maskCount.ToString(), data.maskSprite);
        }

        timeSurvivedText.text = $"Time survived: {SessionData.instance.timeSurvived}";
    }
}
