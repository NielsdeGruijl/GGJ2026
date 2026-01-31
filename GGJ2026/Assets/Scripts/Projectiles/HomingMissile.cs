using System;
using System.Collections;
using UnityEngine;

public class HomingMissile : Projectile
{
    [SerializeField] private float turnspeed = 15;
    
    private Collider2D activeTarget;
    
    private float trackingRadius = 200;

    private bool canRotate = true;
    
    protected override void Start()
    {
        StartCoroutine(TrackTargetCo());
    }

    private IEnumerator TrackTargetCo()
    {
        Collider2D target;
        while (true)
        {
            if (target = FindTarget())
            {
                activeTarget = target;
                //MoveToTarget(target);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Collider2D FindTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position.ToVector2(), trackingRadius);

        float distance = Mathf.Infinity;
        
        Collider2D closestCollider = null;
        
        foreach (Collider2D target in targets)
        {
            if (target.GetComponent<Enemy>())
            {
                float tempDistance = (target.transform.position.ToVector2() -  transform.position.ToVector2()).magnitude;
                if (tempDistance < distance)
                {
                    closestCollider = target;
                    distance = tempDistance;
                }
            }
        }

        return closestCollider;
    }

    private void MoveToTarget(Collider2D target)
    {
        Vector2 targetDirection = (target.transform.position.ToVector2() - transform.position.ToVector2()).normalized;
        
        float diffAngle = Vector2.Angle(transform.up, targetDirection);

        //StartCoroutine(RotateMissileCo(diffAngle));
    }

    private void RotateToTarget()
    {
        canRotate = false;
        float rotationAmount = 0;
        float startRotation = transform.eulerAngles.z;

        float dirNormal = 0;
        
        Vector2 targetDirection = (activeTarget.transform.position.ToVector2() - transform.position.ToVector2()).normalized;
        
        float diffAngle = Vector2.Angle(transform.up, targetDirection);

        if (IsLeft(transform.up, targetDirection))
            dirNormal = 1;
        else
            dirNormal = -1;
        
        float angleStep = dirNormal * turnspeed;
        transform.Rotate(0, 0, angleStep);
    }
    
    bool IsLeft(Vector2 A, Vector2 B)
    {
        return (-A.x * B.y + A.y * B.x) < 0;
    }
    
    private void FixedUpdate()
    {
        rigidBody.AddForce(transform.up * (moveSpeed * Time.deltaTime));

        RotateToTarget();
        
        if (canRotate)
        {
            
        }
    }
}