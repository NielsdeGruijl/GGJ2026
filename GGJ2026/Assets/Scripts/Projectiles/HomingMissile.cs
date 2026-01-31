using System.Collections;
using UnityEngine;

public class HomingMissile : Projectile
{
    [SerializeField] private float turnspeed = 15;
    
    private float trackingRadius;
    
    protected override void Start()
    {
        StartCoroutine(TrackTargetCo());
    }

    private IEnumerator TrackTargetCo()
    {
        Collider target;
        while (true)
        {
            if (target = FindTarget())
            {
                MoveToTarget(target);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Collider FindTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, trackingRadius);

        float distance = Mathf.Infinity;
        
        Collider closestCollider = null;
        
        foreach (Collider target in targets)
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

    private void MoveToTarget(Collider target)
    {
        Vector2 targetDirection = (target.transform.position.ToVector2() - transform.position.ToVector2()).normalized;
        
        float diffAngle = Vector2.Angle(transform.forward, targetDirection);

        StartCoroutine(RotateMissileCo(diffAngle));
    }

    private IEnumerator RotateMissileCo(float angle)
    {
        float tempAngle = Mathf.Abs(angle);
        while (tempAngle > 0)
        {
            Mathf.Lerp(transform.rotation.z, transform.rotation.z + angle, turnspeed * Time.deltaTime);
            
            tempAngle -= turnspeed * Time.deltaTime;
            yield return null;
        }
    }
}
