using System;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public static PlayerLevelManager instance;

    [SerializeField] private float baseXP;
    [SerializeField] private float XPScaling = 1.2f;
    [SerializeField] private float XPIncrement = 3f;
    [SerializeField] private float playerDamgeScaling = 1;
    [SerializeField] private float playerSpeedScaling = 1;

    [HideInInspector] public float playerDamageMult = 1;
    [HideInInspector] public float playerSpeedMult = 1;
    
    private float currentXPMult = 1.2f;
    private float currentXP = 0;
    private float currentLevelXP = 0;
    
    private void Awake()
    {
        if (instance && instance != this)
            Destroy(this);
        else
            instance = this;
        
        currentLevelXP = baseXP;
    }

    private void LevelUp()
    {
        playerDamageMult *= playerDamgeScaling; 
        playerSpeedMult *= playerSpeedScaling;
        
        currentXPMult *= XPScaling;
        
        currentLevelXP = (currentLevelXP + XPIncrement) * currentXPMult;
        currentXP = 0;
        
        Debug.Log("level up! new xp: " + currentLevelXP);
    }

    public void AddXP(float amount)
    {
        currentXP += amount;

        if (currentXP >= currentLevelXP)
            LevelUp();
    }
}
