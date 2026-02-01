using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    
    [SerializeField] private DamagePopup popup;
    
    [SerializeField] private float maxHealth;

    [SerializeField] private bool isEnemy;
    
    public UnityEvent OnDeath;

    private float currentHealth;

    private float damagePopupValue = 0;

    private bool canStartPopup = true;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;

        if (isEnemy)
        {
            if (DifficultyManager.instance)
                maxHealth *= DifficultyManager.instance.enemyHealthMult;
        }
        
        currentHealth = maxHealth;
        
        UpdateHealthBar();
    }

    public void TakeDamage(float damage, bool continuous = false)
    {
        if (isEnemy)
            damage *= PlayerLevelManager.instance.playerDamageMult;
        
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            OnDeath.Invoke();
        }

        if (!continuous)
        {
            PopupManager.instance.CreatePopup(transform.position, damage);
        }
        else
        {
            damagePopupValue += damage;
            
            if(canStartPopup)
                StartCoroutine(ShowPopup());
        }
        
        UpdateHealthBar();
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        //Debug.Log("Health: " + amount);

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
        
        PopupManager.instance.CreatePopup(transform.position, damagePopupValue);
        
        damagePopupValue = 0;
        canStartPopup = true;
    }
}
