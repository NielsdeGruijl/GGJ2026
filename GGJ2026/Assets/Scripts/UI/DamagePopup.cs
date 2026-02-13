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
    [SerializeField] private float moveUpSpeed;
    [SerializeField] private AnimationCurve movementCurve;
    
    private float timeElapsed = 0;

    private float updateInterval = 0.01f;

    private float currentvalue;
    
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

        currentvalue = value;
        text.text = ((int)value).ToString();

        StartCoroutine(UpdatePopupCo());
    }

    private IEnumerator UpdatePopupCo()
    {
        Vector2 basePos = transform.position;

        float randomX = Random.Range(-3, 3);
        float randomY = Random.Range(3, 8);
        
        while (timeElapsed <= movementCurve[movementCurve.length - 1].time + 0.05f)
        {
            //transform.Translate(Vector2.up * (moveUpSpeed * updateInterval));
            
            transform.position = basePos + new Vector2(timeElapsed * randomX, movementCurve.Evaluate(timeElapsed) * randomY);
            
            //text.alpha = Mathf.Lerp(1, 0, timeElapsed / duration);
            timeElapsed += updateInterval * 0.8f;
  
            yield return new WaitForSeconds(updateInterval);
        }
        
        ObjectPool.instance.PoolObject(ObjectTypes.DamagePopups, gameObject);
    }
}
