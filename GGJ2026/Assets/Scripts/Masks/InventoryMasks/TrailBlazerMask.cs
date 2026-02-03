using System.Collections;
using UnityEngine;

public class TrailBlazerMask : InventoryMask
{
    [HideInInspector] public GameObject grenade;
    [HideInInspector] public Explosion explosionPrefab;
    [HideInInspector] public float explosionCount;
    [HideInInspector] public float explosionDelay;
    [HideInInspector] public float explosionRange;
    [HideInInspector] public float explosionDamage;
    [HideInInspector] public float fuzeTimer;
    [HideInInspector] public float grenadeSpeed;

    public override void Activate()
    {
        base.Activate();
        StartCoroutine(SpawnExplosionsCo());
    }

    private IEnumerator SpawnExplosionsCo()
    {
        yield return null;
        
        while (true)
        {
            GameObject go = Instantiate(grenade, transform.position, transform.rotation);
            Vector2 moveDirection = Vector2.up;
            
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, 20))
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    moveDirection = collider.transform.position - transform.position;
                    moveDirection.Normalize();
                }
            }
            
            go.GetComponent<Rigidbody2D>().AddForce(moveDirection * grenadeSpeed, ForceMode2D.Impulse);
        
            int i = 0;
            while (i < explosionCount)
            {
                yield return new WaitForSeconds(explosionDelay);

                Explosion explosionObject = Instantiate(explosionPrefab, go.transform.position, Quaternion.identity);
                explosionObject.Initialize(explosionRange, explosionDamage, fuzeTimer);

                i++;
            }
        
            Destroy(go);

            yield return new WaitForSeconds(cooldown);
        }
    }
}
