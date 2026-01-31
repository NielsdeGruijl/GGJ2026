using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public Vector2 velocity;

    protected float moveSpeed = 0;

    protected Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
    }

    protected virtual void Start()
    {
        rigidBody.AddForce(velocity, ForceMode2D.Impulse);
    }
    
    public virtual void SetSpeed(float pSpeed)
    {
        moveSpeed = pSpeed;
    }
}
