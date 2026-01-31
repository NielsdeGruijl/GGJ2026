using System;
using System.Collections;
using UnityEngine;

public class InventoryMask :  MonoBehaviour
{
    [SerializeField] protected float baseMoveSpeed;
    [SerializeField] protected float bonusMoveSpeed;
    
    [HideInInspector] public float cooldown;
    [HideInInspector] public int numInRing;
    [HideInInspector] public int ringCapacity;
    [HideInInspector] public float targetRadius;

    private float moveSpeed;
    
    public virtual void Activate()
    {
        moveSpeed = baseMoveSpeed + (ringCapacity * bonusMoveSpeed);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.localPosition = GetMovePos();
    }
    
    private Vector3 GetMovePos()
    {
        float fract = (((float)numInRing * (Mathf.PI * 2)) / (float)ringCapacity);
        float xPos = Mathf.Cos((Time.time * moveSpeed) + fract);
        float yPos = Mathf.Sin((Time.time * moveSpeed) + fract);
        return new Vector3(xPos, yPos, 0) * targetRadius;
    }
}
