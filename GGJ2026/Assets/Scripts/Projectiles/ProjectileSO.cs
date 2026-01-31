using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Projectile")]
public class ProjectileSO : ScriptableObject
{
    public Projectile projectilePrefab;

    public int projectileCount;

    public float Speed;
    public float damage;
    
    public float delay;
    public float spread;
}
