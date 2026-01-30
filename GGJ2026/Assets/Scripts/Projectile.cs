using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public Vector2 velocity;
    
    private Rigidbody2D rigidBody;
    
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
        rigidBody.AddForce(velocity, ForceMode2D.Impulse);
    }
}
