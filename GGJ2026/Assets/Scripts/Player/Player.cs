using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [SerializeField] private Animator animator;
    
    [SerializeField] private LayerMask coinPullMask;
    [SerializeField] private float pullSpeed;
    [SerializeField] private float succRadius;

    [HideInInspector] public float speedMult = 1;
    
    // Components
    private CurrencyManager currencyManager;
    private HealthManager healthManager;
    
    private Rigidbody2D rigidBody;
    private PlayerInput playerInput;
    
    // private variables
    private List<Chest> chests = new List<Chest>();
    
    private Vector2 moveDirection;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        currencyManager = GetComponent<CurrencyManager>();
        healthManager = GetComponent<HealthManager>();

        playerInput.actions["Move"].performed += MovePlayer;
        playerInput.actions["Move"].canceled += MovePlayer;

        playerInput.actions["Purchase"].started += PurchaseChest;
        
        healthManager.OnDeath.AddListener(Die);
        
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
    }

    void MovePlayer(InputAction.CallbackContext pContext)
    {
        moveDirection = pContext.ReadValue<Vector2>();
        
        // Animations
        
        animator.SetBool("Walking", true);
        
        if(moveDirection == Vector2.zero)
            animator.SetBool("Walking", false);
        
        if(moveDirection.x < 0)
            animator.SetFloat("WalkDir", -1);
        else
            animator.SetFloat("WalkDir", 1);
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(moveDirection * (moveSpeed * PlayerLevelManager.instance.playerSpeedMult * speedMult));

        
        // Move to coin script!!
        foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), succRadius, coinPullMask))
        {
            Vector2 moveDir = col.transform.position.ToVector2() - transform.position.ToVector2();
            
            col.transform.position = Vector2.MoveTowards(
                col.transform.position, 
                transform.position, 
                (pullSpeed / moveDir.magnitude) * Time.fixedDeltaTime);
        }
    }

    private void PurchaseChest(InputAction.CallbackContext pContext)
    {
        if (chests.Count > 0)
        {
            if (currencyManager.Purchase(chests[0].price))
                chests[0].Open();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(2);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
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
}