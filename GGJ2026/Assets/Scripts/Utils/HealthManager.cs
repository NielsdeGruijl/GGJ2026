using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum DamageType
{
    Impact,
    Continuous,
    None
}

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Slider healthbar;

    public UnityEvent OnDeath = new();
    public DamageEvent OnDamage = new();
    
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

    public void ApplyDamage(float damage, DamageType damageType = DamageType.None)
    {
        currentHealth -= damage;

        damagePopupValue += damage;

        OnDamage.Invoke(damage);
        CreateDamagePopup(); 
        
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
