using System;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private float duration;

    private float timeElapsed = 0;
    
    public void Initialize(float value)
    {
        text.text = ((int)value).ToString();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * (100* Time.deltaTime));

        if (timeElapsed <= duration)
        {
            text.alpha = Mathf.Lerp(1, 0, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    } 
}
