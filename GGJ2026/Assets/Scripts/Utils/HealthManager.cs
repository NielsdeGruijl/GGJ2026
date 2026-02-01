using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    
    [SerializeField] private float maxHealth;
    
    public UnityEvent OnDeath;
    
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        
        UpdateHealthBar();
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            OnDeath.Invoke();
            Destroy(gameObject);
        }

        UpdateHealthBar();
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        Debug.Log("Health: " + amount);

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (!healthbar)
            return;
        
        healthbar.value = currentHealth / maxHealth;
    }
}
