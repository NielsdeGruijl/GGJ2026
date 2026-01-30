using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Projectile")]
public class ProjectileSO : ScriptableObject
{
    public Projectile projectilePrefab;

    public int projectileCount;

    public float damage;
    public float Speed;
}
