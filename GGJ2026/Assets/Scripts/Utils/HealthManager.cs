using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public struct HitInfo
{
    public HitInfo(float damage)
    {
        this.damage = damage;
        knockbackForce = Vector2.zero;
        dealsKnockback = false;
    }

    public HitInfo(Vector2 knockbackForce)
    {
        damage = 0;
        this.knockbackForce = knockbackForce;
        dealsKnockback = true;
    }
    
    public HitInfo(float damage, Vector2 knockbackForce)
    {
        this.damage = damage;
        this.knockbackForce = knockbackForce;
        dealsKnockback = true;
    }

    public float damage;
    public Vector2 knockbackForce;
    public bool dealsKnockback;
}

[System.Serializable]
public class HitEvent : UnityEvent<HitInfo>
{
}

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Slider healthbar;

    public UnityEvent OnDeath = new();
    public HitEvent OnDamage = new();
    
    public float maxHealth { get; private set; }
    private float currentHealth;

    private bool isDead = false;
    
    private float damagePopupValue = 0;

    private void Awake()
    {
        currentHealth = maxHealth;
        
        UpdateHealthBar();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
            
        isDead = false;
    }

    public void ApplyDamage(HitInfo hitInfo)
    {
        OnDamage.Invoke(hitInfo);
        damagePopupValue += hitInfo.damage;
        CreateDamagePopup(); 
        
        currentHealth -= hitInfo.damage;
        
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            
            StopAllCoroutines();
            OnDeath.Invoke();
        }

        UpdateHealthBar();
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        UpdateHealthBar();
    }

    public void UpdateMaxHealth(float newMaxHealth)
    {
        if (maxHealth == 0)
            currentHealth = newMaxHealth;
        else
            AddHealth(newMaxHealth - maxHealth);
        
        maxHealth = newMaxHealth;
        
        UpdateHealthBar();
    }

    // ===== UI =====
    
    private void UpdateHealthBar()
    {
        if (!healthbar)
            return;
        
        healthbar.value = currentHealth / maxHealth;
    }

    private void CreateDamagePopup()
    {
        PopupManager.instance.CreateWorldPopup(transform.position, damagePopupValue);
        
        damagePopupValue = 0;
    }
}
