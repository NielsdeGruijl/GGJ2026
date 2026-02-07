using System;
using UnityEngine;

public class Mask : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private BoxCollider2D collider;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    
    public MaskSO maskSO;

    private Vector2 startPosition;
    
    private float timeElapsed;
    
    private void Awake()
    {
        collider.enabled = false;
        startPosition = transform.position;
        
        if (maskSO)
            spriteRenderer.sprite = maskSO.maskSprite;
    }

    public void Initialize(MaskSO pMaskSO)
    {
        maskSO = pMaskSO;
        if (maskSO)
            spriteRenderer.sprite = maskSO.maskSprite;
    }

    private void Update()
    {
        if(curve.keys[curve.length - 1].time > timeElapsed)
            transform.position = startPosition + new Vector2(timeElapsed,  curve.Evaluate(timeElapsed)) * 2;
        else
            collider.enabled = true;

        timeElapsed += Time.deltaTime;
    }
}
