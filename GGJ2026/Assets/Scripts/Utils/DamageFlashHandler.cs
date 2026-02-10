using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageFlashHandler : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> spriteRenderers;
    
    [SerializeField] private Color flashColor;
    [SerializeField] private float flashDuration;

    private readonly int flashColorID = Shader.PropertyToID("_FlashColor");
    private readonly int flashValueID = Shader.PropertyToID("_FlashValue");
    
    private Material[] materials;

    private int flashValue;

    private void Awake()
    {
        materials = new Material[spriteRenderers.Count];

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
        }

        if (TryGetComponent(out HealthManager healthManager))
        {
            healthManager.OnDamage.AddListener(ShowFlash);
        }
    }

    private void OnEnable()
    {
        flashValue = 0;
        SetFlashValue();
    }

    public void ShowFlash(HitInfo damage)
    {
        StartCoroutine(ShowFlashCo());
    }

    private IEnumerator ShowFlashCo()
    {
        //float timeElapsed = 0;

        flashValue = 1;
        
        SetFlashColor();
        SetFlashValue();
        
        yield return new WaitForSeconds(flashDuration);
        
        flashValue = 0;
        SetFlashValue();
    }

    private void SetFlashColor()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetColor(flashColorID, flashColor);
        }
    }

    private void SetFlashValue()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat(flashValueID, flashValue);
        }
    }
}
