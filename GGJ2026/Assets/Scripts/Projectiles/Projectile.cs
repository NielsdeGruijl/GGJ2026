using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DamageEvent : UnityEvent<float>
{
}

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public Vector2 velocity;

    protected Rigidbody2D rigidBody;

    protected float damage;

    public DamageEvent OnHit;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
    }

    public void Initialize(Vector2 moveDirection, float moveSpeed)
    {
        velocity = moveDirection * moveSpeed;
        rigidBody.AddForce(velocity * moveSpeed, ForceMode2D.Impulse);
    }

    public void SetDamage(float pDamage)
    {
        damage = pDamage;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthManager enemy))
        {
            enemy.ApplyDamage(new HitInfo(damage, velocity.normalized * 10));
            OnHit.Invoke(damage);
            OnHit.RemoveAllListeners();
            ObjectPool.instance.PoolObject(ObjectTypes.Projectiles, gameObject);
        }
    }
}
