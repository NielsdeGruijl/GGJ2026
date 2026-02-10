using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(HealthManager))]
public class Entity : MonoBehaviour
{
    [SerializeField] private BaseEntityDataSO baseStats;

    public EntityData stats;

    protected Rigidbody2D rigidBody;
    protected HealthManager healthManager;
    
    protected virtual void Awake()
    {
        InitializeStats();
        
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;

        healthManager = GetComponent<HealthManager>();
        healthManager.OnDeath.AddListener(Die);
        healthManager.UpdateMaxHealth(stats.GetStatValue(StatType.Health));
    }
    
    protected virtual void InitializeStats()
    {
        stats = new EntityData(baseStats);
    }

    protected virtual void Die()
    {
        
    }
}
