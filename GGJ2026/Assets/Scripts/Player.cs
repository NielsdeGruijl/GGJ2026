using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private PlayerInput playerInput;

    private Vector2 moveDirection;

    private Rigidbody2D rigidBody;

    private Vector2 velocity;
    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Move"].performed += MovePlayer;
        playerInput.actions["Move"].canceled += MovePlayer;
        
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
    }

    void MovePlayer(InputAction.CallbackContext pContext)
    {
        moveDirection = pContext.ReadValue<Vector2>();
        
        velocity = moveDirection * moveSpeed;
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Mask mask;
        // doesn't work, pls fix
        if (collision.TryGetComponent<Mask>(out mask))
        {
            EquipMask(mask.maskSO);
        }
    }
    
    private void EquipMask(MaskSO mask)
    {
        mask.Equip(this);
    }

    private void UnequipMask(MaskSO mask)
    {
        
    }
}