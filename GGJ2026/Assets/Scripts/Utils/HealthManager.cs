using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    
    [SerializeField] private DamagePopup popup;
    
    public float maxHealth;

    [SerializeField] private bool isEnemy;

    [SerializeField] private bool canDie = true;

    private bool isDead = false;
    
    public UnityEvent OnDeath;

    private float currentHealth;

    private float damagePopupValue = 0;

    private bool canStartPopup = true;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        
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
        }
        
    }

    public void TakeDamage(float damage, bool continuous = false)
    {
        if (isEnemy)
            damage *= PlayerLevelManager.instance.playerDamageMult;
        
        currentHealth -= damage;

        if (currentHealth <= 0 && canDie && !isDead)
        {
            StopAllCoroutines();
            isDead = true;
            OnDeath.Invoke();
        }

        if (!continuous)
        {
            CreateDamagePopup(damage);
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

    private IEnumerator ShowPopup()
    {
        canStartPopup = false;
        yield return new WaitForSeconds(0.2f);
        
        CreateDamagePopup(damagePopupValue);
        
        damagePopupValue = 0;
        canStartPopup = true;
    }

    private void CreateDamagePopup(float damage)
    {
        if (!isEnemy)
        {
            PopupManager.instance.CreatePopup(transform.position, damage, PopupType.Player);
            return;
        }
        
        PopupManager.instance.CreatePopup(transform.position, damage);
    }
}
