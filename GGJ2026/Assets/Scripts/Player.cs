using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private PlayerOrbitManager orbitManager;
    private CurrencyManager currencyManager;
    
    private PlayerInput playerInput;

    private List<Chest> chests = new List<Chest>();
    
    private Vector2 moveDirection;

    private Rigidbody2D rigidBody;

    private Vector2 velocity;

    public bool canPurchaseChest = false;
    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        orbitManager = GetComponent<PlayerOrbitManager>();
        currencyManager = GetComponent<CurrencyManager>();

        playerInput.actions["Move"].performed += MovePlayer;
        playerInput.actions["Move"].canceled += MovePlayer;

        playerInput.actions["Purchase"].started += PurchaseChest;
        
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
        rigidBody.AddForce(moveDirection * moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Mask mask;
        if (collision.TryGetComponent<Mask>(out mask))
        {
            EquipMask(mask.maskSO);
            
            //Destroy(collision.gameObject);
        }

        if (collision.TryGetComponent<Chest>(out Chest chest))
        {
            chests.Add(chest);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Chest>(out Chest chest))
        {
            if (chests.Contains(chest))
                chests.Remove(chest);
        }
    }
    
    private void EquipMask(MaskSO mask)
    {
        // Call equip function on mask (essentially void Awake)
        mask.Equip(this);
        
        orbitManager.AddMask(mask.MakeMask(this));
    }

    private void PurchaseChest(InputAction.CallbackContext pContext)
    {
        if (chests.Count > 0)
        {
            if (currencyManager.Purchase(chests[0].price))
            {
                chests[0].Open();
                chests.RemoveAt(0);
            }
        }
    }
}