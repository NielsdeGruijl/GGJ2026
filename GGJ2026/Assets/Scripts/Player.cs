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
    
    private PlayerOrbitManager orbitManager;
    private CurrencyManager currencyManager;
    private HealthManager healthManager;
    
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
        healthManager = GetComponent<HealthManager>();

        playerInput.actions["Move"].performed += MovePlayer;
        playerInput.actions["Move"].canceled += MovePlayer;

        playerInput.actions["Purchase"].started += PurchaseChest;
        
        healthManager.OnDeath.AddListener(Die);
        
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;

        speedMult = 1;
    }

    void MovePlayer(InputAction.CallbackContext pContext)
    {
        animator.SetBool("Walking", true);
        
        moveDirection = pContext.ReadValue<Vector2>();
        
        if(moveDirection == Vector2.zero)
            animator.SetBool("Walking", false);
        
        
        if(moveDirection.x < 0)
            animator.SetFloat("WalkDir", -1);
        else
            animator.SetFloat("WalkDir", 1);
        
        velocity = moveDirection * moveSpeed;
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(moveDirection * (moveSpeed * PlayerLevelManager.instance.playerSpeedMult * speedMult));

        foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), succRadius, coinPullMask))
        {
            Vector2 moveDir = col.transform.position.ToVector2() - transform.position.ToVector2();
            
            //Debug.Log(moveDir * (moveSpeed * Time.fixedDeltaTime));
            
            col.transform.position = Vector2.MoveTowards(
                col.transform.position, 
                transform.position, 
                (pullSpeed / moveDir.magnitude) * Time.fixedDeltaTime);
            
            //col.transform.Translate(((moveDir.normalized * pullSpeed / moveDir.magnitude) * Time.fixedDeltaTime));
        }
    }

    private void EquipMask(MaskSO mask)
    {
        mask.Equip(this);
        
        orbitManager.AddMask(mask.MakeMask(this));
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
        if (collision.TryGetComponent<Mask>(out Mask mask))
        {
            EquipMask(mask.maskSO);
            
            Destroy(collision.gameObject);
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
}