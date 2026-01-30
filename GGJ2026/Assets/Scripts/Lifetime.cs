using System.Collections;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    [SerializeField] private float duration;
    
    private void Awake()
    {
        StartCoroutine(LifeTimeCo());
    }

    IEnumerator LifeTimeCo()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
