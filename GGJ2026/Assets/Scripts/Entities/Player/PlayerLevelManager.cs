using UnityEngine;
using UnityEngine.Events;

public class PlayerLevelManager : MonoBehaviour
{
    [SerializeField] private float baseXP;
    [SerializeField] private float XPScaling = 1.2f;
    [SerializeField] private float XPIncrement = 3f;

    public UnityEvent OnLevelUp;
    
    private float currentXPMult = 1;
    private float currentXP;
    private float currentLevelXP;

    private void Awake()
    {
        Enemy.OnDeath.AddListener(AddXP);
    }

    private void LevelUp()
    {
        currentLevelXP = (currentLevelXP + XPIncrement) * XPScaling;
        currentXP = 0;

        OnLevelUp.Invoke();
        
        Debug.Log("level up! new xp: " + currentLevelXP);
    }

    public void AddXP(float amount)
    {
        currentXP += amount;

        Debug.Log("XP added: " + amount);
        
        if (currentXP >= currentLevelXP)
            LevelUp();
    }
}
