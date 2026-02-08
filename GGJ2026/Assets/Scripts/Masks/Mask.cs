using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mask : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private BoxCollider2D collider;
    
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float randomHorizontalMovement;
    [SerializeField] private float randomVerticalMovement;
    
    
    public MaskSO maskSO;

    private Vector2 startPosition;
    
    private float timeElapsed;

    private float horizontal;
    private float vertical;
    
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
        
        horizontal = Random.Range(-randomHorizontalMovement, 0);
        vertical = Random.Range(1, randomVerticalMovement);
    }

    private void Update()
    {

        
        if(curve.keys[curve.length - 1].time > timeElapsed)
            transform.position = startPosition + new Vector2(timeElapsed * horizontal,  curve.Evaluate(timeElapsed) * vertical);
        else
            collider.enabled = true;

        timeElapsed += Time.deltaTime;
    }
}
