using System.Collections;
using UnityEngine;

public class InventoryMask :  MonoBehaviour
{
    [SerializeField] protected float baseOrbitMoveSpeed = 1;

    [HideInInspector] public MaskSO maskData;
    public DamageEvent OnAuraDamage;

    protected PlayerMaskManager manager;
    protected PlayerMaskData playerMaskData;
    protected WaitForSeconds waitForCooldown;
    
    private WaitForSeconds waitForAuraCooldown;

    private int numInRing;
    private int ringCapacity;
    private float targetRadius;
    
    private const float bonusOrbitMoveSpeed = 0.25f;
    private float orbitMoveSpeed;

    private bool canDamageAura = true;

    public void Initialize(int pNumInRing, int pRingCapacity, float pTargetRadius)
    {
        numInRing = pNumInRing;
        ringCapacity = pRingCapacity;
        targetRadius = pTargetRadius;
    }    
    
    public void Activate(PlayerMaskData pPlayerMaskData, PlayerMaskManager maskManager)
    {
        playerMaskData = pPlayerMaskData;

        manager = maskManager;
        
        manager.playerData.GetStatEvent(StatType.AttackSpeed).AddListener(UpdateCooldown);
        
        orbitMoveSpeed = baseOrbitMoveSpeed + (ringCapacity * bonusOrbitMoveSpeed);
        
        waitForCooldown = new WaitForSeconds(manager.playerData.GetModifiedValue(StatType.AttackSpeed, maskData.cooldown));
        waitForAuraCooldown = new WaitForSeconds(3);

        ActivateMask();
    }
    
    protected virtual void ActivateMask()
    {
    }

    private void UpdateCooldown(float newCooldown)
    {
        waitForCooldown = new WaitForSeconds(manager.playerData.GetModifiedValue(StatType.AttackSpeed, maskData.cooldown));
    }

    private void Update()
    {
        Move();
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
        float xPosition = Mathf.Cos((Time.time * orbitMoveSpeed) + fract);
        float yPosition = Mathf.Sin((Time.time * orbitMoveSpeed) + fract);
        return new Vector3(xPosition, yPosition, 0) * targetRadius;
    }

    private IEnumerator DamagingAuraCooldownCo()
    {
        canDamageAura = false;
        yield return waitForAuraCooldown;
        canDamageAura = true;
    }
    

}
