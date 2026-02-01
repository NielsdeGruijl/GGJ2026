using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    public UnityEvent OnDeath;
    
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            OnDeath.Invoke();
            Destroy(gameObject);
        }
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        Debug.Log("Health: " + amount);
    }
}
