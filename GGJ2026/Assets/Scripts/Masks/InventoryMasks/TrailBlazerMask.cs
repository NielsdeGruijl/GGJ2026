using System.Collections;
using UnityEngine;

public class TrailBlazerMask : InventoryMask
{
    [HideInInspector] public GameObject grenade;
    [HideInInspector] public Explosion explosionPrefab;
    [HideInInspector] public float explosionCount;
    [HideInInspector] public float explosionRange;
    [HideInInspector] public float explosionDamage;

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
            Vector2 moveDirection = transform.position - transform.parent.position;
            moveDirection.Normalize();
            
            //Debug.Log(moveDirection);
        
            go.GetComponent<Rigidbody2D>().AddForce(moveDirection * 5, ForceMode2D.Impulse);
        
            int i = 0;
            while (i < explosionCount)
            {
                yield return new WaitForSeconds(0.5f);

                Explosion explosionObject = Instantiate(explosionPrefab, go.transform.position, Quaternion.identity);
                explosionObject.Initialize(explosionRange, explosionDamage, 2f);
            
                Debug.Log(go.transform.position);
                i++;
            }
        
            Destroy(go);
        }
    }
}
