using System.Collections;
using UnityEngine;

public class TrailBlazerMask : InventoryMask
{
    private TrailBlazerSO newData;

    private WaitForSeconds waitForExplosionDelay;
    
    public override void Activate(PlayerMaskData playerMaskData)
    {
        base.Activate(playerMaskData);
        
        newData = maskData as TrailBlazerSO;

        waitForExplosionDelay = new WaitForSeconds(newData.explosionDelay);
        
        StartCoroutine(SpawnExplosionsCo());
    }

    private IEnumerator SpawnExplosionsCo()
    {
        yield return null;
        
        while (true)
        {
            GameObject go = Instantiate(newData.grenade, transform.position, transform.rotation);
            Vector2 moveDirection = Vector2.up;
            
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, 20))
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    moveDirection = collider.transform.position - transform.position;
                    moveDirection.Normalize();
                }
            }
            
            go.GetComponent<Rigidbody2D>().AddForce(moveDirection * newData.grenadeSpeed, ForceMode2D.Impulse);
        
            int i = 0;
            while (i < newData.explosionCount)
            {
                yield return waitForExplosionDelay;

                Explosion explosionObject = ObjectPool.instance.Get(ObjectTypes.Explosions).GetComponent<Explosion>();
                explosionObject.transform.position = go.transform.position;
                explosionObject.Initialize(newData.explosionRange, newData.explosionDamage * playerMaskData.playerDamageMult, newData.fuzeTimer);
                explosionObject.OnHit.AddListener(UpdateDamageDealt);

                i++;
            }
        
            Destroy(go);

            yield return waitForCooldown;
        }
    }
}
