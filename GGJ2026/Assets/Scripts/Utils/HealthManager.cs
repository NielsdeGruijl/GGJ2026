using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    
    [SerializeField] private DamagePopup popup;

    [SerializeField] private GameObject whiteFlash;
    [SerializeField] private bool showDamageVFX;
    
    public float maxHealth;
    public UnityEvent OnDeath;

    [SerializeField] private bool isEnemy;
    [SerializeField] private bool canDie = true;

    private bool isDead = false;
    
    private float currentHealth;

    private float damagePopupValue = 0;
    private bool canStartPopup = true;

    private WaitForSeconds waitFlash = new WaitForSeconds(0.1f);
    private WaitForSeconds waitPopup = new WaitForSeconds(0.2f);

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

        canStartPopup = true;
    }

    public void TakeDamage(float damage, bool continuous = false)
    {
        if (isEnemy)
            damage *= PlayerLevelManager.instance.playerDamageMult;
        
        currentHealth -= damage;

        if (currentHealth <= 0 && canDie && !isDead)
        {
            isDead = true;
            StopAllCoroutines();
            OnDeath.Invoke();
        }

        if (!continuous)
        {
            CreateDamagePopup(damage);
            if(showDamageVFX)
                ShowWhiteFlash();
        }
        else
        {
            damagePopupValue += damage;
            
            if(canStartPopup && gameObject.activeSelf)
                StartCoroutine(ShowPopup());
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

    private void UpdateHealthBar()
    {
        if (!healthbar)
            return;
        
        healthbar.value = currentHealth / maxHealth;
    }

    private void ShowWhiteFlash()
    {
        if(gameObject.activeSelf)
            StartCoroutine(ShowFlashCo());
    }

    private IEnumerator ShowFlashCo()
    {
        whiteFlash.SetActive(true);
        yield return waitFlash;
        whiteFlash.SetActive(false);
    }
    
    private IEnumerator ShowPopup()
    {
        canStartPopup = false;
        yield return waitPopup;
        
        CreateDamagePopup(damagePopupValue);
        
        if(showDamageVFX)
            ShowWhiteFlash();
        
        damagePopupValue = 0;
        canStartPopup = true;
    }

    private void CreateDamagePopup(float damage)
    {
        /*if (!isEnemy)
        {
            PopupManager.instance.CreatePopup(transform.position, damage, PopupType.Player);
            return;
        }
        
        PopupManager.instance.CreatePopup(transform.position, damage);*/
    }
}
