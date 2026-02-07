using System;
using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Transform sprite;
    
    [SerializeField] private ParticleSystem particles;
    
    [HideInInspector] public float range;
    [HideInInspector] public float damage;
    [HideInInspector] public float fuzeTimer;

    public DamageEvent OnHit;

    private WaitForSeconds waitFuze;
    private WaitForSeconds waitParticle;

    private Vector2 baseScale;
    
    private void Awake()
    {
        waitFuze = new WaitForSeconds(fuzeTimer);
        waitParticle = new WaitForSeconds(particles.main.duration);

        baseScale = new Vector2(1, 1);
    }

    public void Initialize(float pRange, float pDamage, float pFuzeTimer)
    {
        range = pRange;
        damage = pDamage;
        fuzeTimer = pFuzeTimer;
        
        sprite.gameObject.SetActive(true);
        sprite.localScale = baseScale * (range * 2);
        
        StartCoroutine(StartTimerCo());
    }

    private IEnumerator StartTimerCo()
    {
        yield return waitFuze;
        
        sprite.gameObject.SetActive(false);
        
        particles.Play();
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out HealthManager enemy) && !collider.CompareTag("Player"))
            {
                enemy.TakeDamage(damage);
                OnHit.Invoke(damage);
            }
        }

        yield return waitParticle;
        OnHit.RemoveAllListeners();
        ObjectPool.instance.PoolObject(ObjectTypes.Explosions, gameObject);
    }
}
