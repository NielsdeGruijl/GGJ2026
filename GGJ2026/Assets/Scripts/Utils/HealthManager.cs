using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    
    [SerializeField] private GameObject whiteFlash;
    [SerializeField] private bool showDamageVFX;
    
    public float maxHealth;
    public UnityEvent OnDeath;

    [SerializeField] private bool isEnemy;
    [SerializeField] private bool canDie = true;

    private bool isDead = false;
    
    private float currentHealth;

    private float damagePopupValue = 0;
    private bool isGeneratingPopup = false;

    private WaitForSeconds waitForDOTInterval;
    private WaitForSeconds waitForPopup = new WaitForSeconds(1);

    private void Awake()
    {
        currentHealth = maxHealth;
        
        UpdateHealthBar();
    }

    private void OnEnable()
    {
        if (isEnemy)
        {
            if (DifficultyManager.instance)
                maxHealth *= DifficultyManager.instance.enemyHealthMult;
            
            currentHealth = maxHealth;
            
            isDead = false;
            whiteFlash.SetActive(false);
        }

        isGeneratingPopup = false;
    }

    public void ApplyDamage(float damage, bool continuous = false)
    {
        if (isEnemy)
            damage *= PlayerLevelManager.instance.playerDamageMult;
        
        currentHealth -= damage;

        damagePopupValue += damage;
        
        if (currentHealth <= 0 && canDie && !isDead)
        {
            isDead = true;
            
            if(isGeneratingPopup || currentHealth + damage >= maxHealth)
                CreateDamagePopup();
            
            StopAllCoroutines();
            OnDeath.Invoke();
        }
        
        if (!isGeneratingPopup && gameObject.activeSelf)
            StartCoroutine(ShowPopup());

        UpdateHealthBar();
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        UpdateHealthBar();
    }

    // ===== UI =====
    
    private void UpdateHealthBar()
    {
        if (!healthbar)
            return;
        
        healthbar.value = currentHealth / maxHealth;
    }

    private IEnumerator ShowPopup()
    {
        isGeneratingPopup = true;
        yield return waitForPopup;
        
        CreateDamagePopup();
        
        isGeneratingPopup = false;
    }

    private void CreateDamagePopup()
    {
        if (!isEnemy)
        {
            PopupManager.instance.CreateWorldPopup(transform.position, damagePopupValue, PopupType.Player);
            return;
        }
        
        PopupManager.instance.CreateWorldPopup(transform.position, damagePopupValue);
        
        damagePopupValue = 0;
    }
}
