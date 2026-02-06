using System;
using System.Collections;
using UnityEngine;

public class InventoryMask :  MonoBehaviour
{
    [SerializeField] protected float baseMoveSpeed = 1;
    protected float bonusMoveSpeed = 0.25f;

    [HideInInspector] public MaskSO maskData;

    private int numInRing;
    private int ringCapacity;
    private float targetRadius;
    
    private float moveSpeed;

    protected PlayerMaskData playerMaskData;

    public DamageEvent OnAuraDamage;

    protected WaitForSeconds waitForCooldown;

    private void Awake()
    {
        waitForCooldown = new WaitForSeconds(maskData.cooldown);
    }

    public virtual void Activate(PlayerMaskData pPlayerMaskData)
    {
        playerMaskData = pPlayerMaskData;
        
        moveSpeed = baseMoveSpeed + (ringCapacity * bonusMoveSpeed);
    }

    private void Update()
    {
        Move();
    }

    public void Initialize(int pNumInRing, int pRingCapacity, float pTargetRadius)
    {
        numInRing = pNumInRing;
        ringCapacity = pRingCapacity;
        targetRadius = pTargetRadius;
    }

    private void Move()
    {
        transform.localPosition = GetMovePosition();
    }

    protected void UpdateDamageDealt(float damage)
    {
        playerMaskData.maskTypeDamageDealt[maskData.maskName] += damage;
    }
    
    private Vector3 GetMovePosition()
    {
        float fract = (((float)numInRing * (Mathf.PI * 2)) / (float)ringCapacity);
        float xPosition = Mathf.Cos((Time.time * moveSpeed) + fract);
        float yPosition = Mathf.Sin((Time.time * moveSpeed) + fract);
        return new Vector3(xPosition, yPosition, 0) * targetRadius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerMaskData.maskCollisionDamage <= 0)
            return;
     
        if (other.TryGetComponent(out HealthManager enemy))
        { 
            enemy.TakeDamage(playerMaskData.maskCollisionDamage);
            OnAuraDamage.Invoke(playerMaskData.maskCollisionDamage / playerMaskData.sortedMasks["DamagingAura"].Count);
        }
    }
}
