using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public struct HitInfo
{
    public HitInfo(float damage, bool isContinuous = false)
    {
        this.damage = damage;
        knockbackForce = Vector2.zero;
        dealsKnockback = false;
        this.isContinuous = isContinuous;
    }

    public HitInfo(Vector2 knockbackForce, bool isContinuous = false)
    {
        damage = 0;
        this.knockbackForce = knockbackForce;
        dealsKnockback = true;
        this.isContinuous = isContinuous;
    }
    
    public HitInfo(float damage, Vector2 knockbackForce, bool isContinuous = false)
    {
        this.damage = damage;
        this.knockbackForce = knockbackForce;
        dealsKnockback = true;
        this.isContinuous = isContinuous;
    }

    public float damage;
    public Vector2 knockbackForce;
    public bool dealsKnockback;
    public bool isContinuous;
}

public class ReadOnlyAttribute : PropertyAttribute
{
}

[System.Serializable]
public class HitEvent : UnityEvent<HitInfo>
{
}

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    
    [SerializeField] private AudioClip damageSound;

    public UnityEvent OnDeath = new();
    public HitEvent OnDamage = new();
    
    public float maxHealth { get; private set; }
    [ReadOnly] public float currentHealth;

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
        
        currentHealth -= hitInfo.damage;
        
        if(damageSound)
            AudioManager.instance.PlaySFX(damageSound);
        
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
        float gainedHealth = newMaxHealth - maxHealth;
        maxHealth = newMaxHealth;
        
        if (gainedHealth == newMaxHealth)
            currentHealth = newMaxHealth;
        else
            AddHealth(gainedHealth);
        
        UpdateHealthBar();
    }

    // ===== UI =====
    
    private void UpdateHealthBar()
    {
        if (!healthbar)
            return;
        
        healthbar.value = currentHealth / maxHealth;
    }


}
