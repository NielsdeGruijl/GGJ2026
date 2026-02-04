using System.Collections;
using UnityEngine;

public class TrailBlazerMask : InventoryMask
{
    private TrailBlazerSO newMaskData;
    
    public override void Activate(PlayerMaskData playerMaskData)
    {
        base.Activate(playerMaskData);
        
        newMaskData = maskData as TrailBlazerSO;
        
        StartCoroutine(SpawnExplosionsCo());
    }

    private IEnumerator SpawnExplosionsCo()
    {
        yield return null;
        
        while (true)
        {
            GameObject go = Instantiate(newMaskData.grenade, transform.position, transform.rotation);
            Vector2 moveDirection = Vector2.up;
            
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, 20))
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    moveDirection = collider.transform.position - transform.position;
                    moveDirection.Normalize();
                }
            }
            
            go.GetComponent<Rigidbody2D>().AddForce(moveDirection * newMaskData.grenadeSpeed, ForceMode2D.Impulse);
        
            int i = 0;
            while (i < newMaskData.explosionCount)
            {
                yield return new WaitForSeconds(newMaskData.explosionDelay);

                Explosion explosionObject = Instantiate(newMaskData.explosionPrefab, go.transform.position, Quaternion.identity);
                explosionObject.Initialize(newMaskData.explosionRange, newMaskData.explosionDamage, newMaskData.fuzeTimer);
                explosionObject.OnHit.AddListener(UpdateDamageDealt);

                i++;
            }
        
            Destroy(go);

            yield return new WaitForSeconds(maskData.cooldown);
        }
    }
}
