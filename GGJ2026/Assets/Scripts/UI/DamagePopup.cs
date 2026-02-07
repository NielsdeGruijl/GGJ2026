using System;
using System.Collections;
using TMPro;
using UnityEngine;

public enum PopupType
{
    Default,
    Player,
    Crit
}

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private float duration;

    private float timeElapsed = 0;

    private float updateInterval = 0.01f;
    
    private void OnEnable()
    {
        text.alpha = 1;
        timeElapsed = 0;
    }

    public void Initialize(float value, PopupType type)
    {
        switch (type)
        {
            case PopupType.Default:
                text.color = Color.white;
                break;
            case PopupType.Player:
                text.color = Color.red;
                break;
            case PopupType.Crit:
                text.color = Color.yellow;
                break;
            default:
                text.color = Color.white;
                break;
        }
        
        text.text = ((int)value).ToString();

        StartCoroutine(UpdatePopupCo());
    }

    private IEnumerator UpdatePopupCo()
    {
        while (timeElapsed <= duration)
        {
            transform.Translate(Vector2.up * (100 * updateInterval));
 
            text.alpha = Mathf.Lerp(1, 0, timeElapsed / duration);
            timeElapsed += updateInterval;
  
            yield return new WaitForSeconds(updateInterval);
        }
        
        ObjectPool.instance.PoolObject(ObjectTypes.DamagePopups, gameObject);
    }
}
