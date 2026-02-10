using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Entity
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private LayerMask coinPullMask;
    [SerializeField] private float pullSpeed;
    [SerializeField] private float succRadius;

    [SerializeField] private float knockbackRadius;
    [SerializeField] private float knockbackForce;

    // Components
    private CurrencyManager currencyManager;
    private PlayerMaskData playerMaskData;

    private PlayerInput playerInput;

    // private variables
    private List<Chest> chests = new List<Chest>();

    private Vector2 moveDirection;

    protected override void Awake()
    {
        base.Awake();

        GetComponent<PlayerMaskManager>().playerData = stats;
        playerInput = GetComponent<PlayerInput>();
        currencyManager = GetComponent<CurrencyManager>();
        
        stats.GetStatEvent(StatType.Health).AddListener(healthManager.UpdateMaxHealth);
    }

    void Start()
    {
        playerInput.actions["Move"].performed += MovePlayer;
        playerInput.actions["Move"].canceled += MovePlayer;

        playerInput.actions["Purchase"].started += PurchaseChest;

        playerInput.actions["Die"].started += TestDie;
    }

    public void SetMaskData(PlayerMaskData maskData)
    {
        playerMaskData = maskData;
    }
    
    void MovePlayer(InputAction.CallbackContext pContext)
    {
        moveDirection = pContext.ReadValue<Vector2>();
        
        // Animations
        
        animator.SetBool("Walking", true);
        
        if(moveDirection == Vector2.zero)
            animator.SetBool("Walking", false);

        if(moveDirection.x < 0)
            spriteRenderer.flipX = true;
        if (moveDirection.x > 0)
            spriteRenderer.flipX = false;
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(moveDirection * (stats.GetStatValue(StatType.MoveSpeed)));
        
        animator.SetFloat("WalkSpeed", PlayerLevelManager.instance.playerSpeedMult * playerMaskData.playerMoveSpeedMult);

        // Move to coin script!!
        foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), succRadius + playerMaskData.playerMagnetRange, coinPullMask))
        {
            Vector2 moveDir = col.transform.position.ToVector2() - transform.position.ToVector2();
            
            col.transform.position = Vector2.MoveTowards(
                col.transform.position, 
                transform.position, 
                ((pullSpeed + playerMaskData.playerMagnetForce) / moveDir.magnitude) * Time.fixedDeltaTime);
        }
    }

    private void PurchaseChest(InputAction.CallbackContext pContext)
    {
        if (chests.Count > 0)
        {
            if (currencyManager.Purchase(chests[0].price))
            {
                foreach (Collider2D collider in Physics2D.OverlapCircleAll(chests[0].transform.position, knockbackRadius))
                {
                    if (collider.TryGetComponent(out Enemy enemy))
                    {
                        enemy.ApplyKnockback((enemy.transform.position - chests[0].transform.position).normalized * knockbackForce);
                    }
                }
                
                chests[0].Open(playerMaskData.luck);
            }
        }
    }

    private void TestDie(InputAction.CallbackContext ctx)
    {
        Die();
    }
    
    protected override void Die()
    {
        base.Die();
        
        UpdateSessionData();
        StartCoroutine(DieCo());
    }

    private IEnumerator DieCo()
    {
        yield return null;
        SceneManager.LoadScene(2);
    }

    private void UpdateSessionData()
    {
        foreach (string key in playerMaskData.maskKeys)
        {
            MaskData sessionMaskData = new MaskData();
            sessionMaskData.damageDealt = playerMaskData.maskTypeDamageDealt[key];

            if (sessionMaskData.damageDealt <= 0)
                continue;
            
            sessionMaskData.maskName = playerMaskData.sortedMasks[key][0].maskData.maskName;
            sessionMaskData.maskSprite = playerMaskData.sortedMasks[key][0].maskData.maskSprite;
            sessionMaskData.maskCount = playerMaskData.sortedMasks[key].Count;
            
            SessionData.instance.maskData.Add(sessionMaskData);
        }
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