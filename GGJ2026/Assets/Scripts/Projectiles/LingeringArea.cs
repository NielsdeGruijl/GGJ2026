using System.Collections;
using UnityEngine;

public class LingeringArea : MonoBehaviour
{
    [SerializeField] private Transform areaSprite;

    private float dps;
    private float radius;

    private float duration;

    public void Initialize(float damagePerSecond, float areaRadius, float effectDuration)
    {
        dps = damagePerSecond;
        radius = areaRadius;
        duration = effectDuration;

        areaSprite.localScale *= radius * 2;

        StartCoroutine(DealDamageCo());
    }

    private IEnumerator DealDamageCo()
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            foreach (Collider2D target in Physics2D.OverlapCircleAll(transform.position, radius))
            {
                if (!target.CompareTag("Player") && target.TryGetComponent(out HealthManager manager))
                {
                    manager.TakeDamage(dps * Time.deltaTime, true);
                }
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
