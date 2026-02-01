using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public Vector2 velocity;

    public float moveSpeed = 0;

    protected Rigidbody2D rigidBody;

    protected float damage;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
    }

    protected virtual void Start()
    {
        rigidBody.AddForce(velocity * moveSpeed, ForceMode2D.Impulse);
    }
    
    public virtual void SetSpeed(float pSpeed)
    {
        moveSpeed = pSpeed;
    }

    public virtual void SetDamage(float pDamage)
    {
        damage = pDamage;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthManager enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
