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
    private WaitForSeconds waitForAuraCooldown;
    private bool canDamageAura = true;

    public virtual void Activate(PlayerMaskData pPlayerMaskData)
    {
        playerMaskData = pPlayerMaskData;
        
        playerMaskData.OnCooldownChanged.AddListener(UpdateCooldown);
        
        moveSpeed = baseMoveSpeed + (ringCapacity * bonusMoveSpeed);
        
        waitForCooldown = new WaitForSeconds(maskData.cooldown);
        waitForAuraCooldown = new WaitForSeconds(3);
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

    private void UpdateCooldown()
    {
        waitForCooldown = new WaitForSeconds(maskData.cooldown * playerMaskData.cooldownMult);
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

    private IEnumerator DamagingAuraCooldownCo()
    {
        canDamageAura = false;
        yield return waitForAuraCooldown;
        canDamageAura = true;
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (playerMaskData.maskCollisionDamage <= 0)
            return;
     
        if (other.TryGetComponent(out HealthManager enemy))
        { 
            enemy.ApplyDamage(playerMaskData.maskCollisionDamage);
            OnAuraDamage.Invoke(playerMaskData.maskCollisionDamage / playerMaskData.sortedMasks["DamagingAura"].Count);
            enemy.GetComponent<Enemy>().ApplyKnockback((enemy.transform.position - transform.position).normalized * 10);
            
            //StartCoroutine(DamagingAuraCooldownCo());
        }
    }
}
