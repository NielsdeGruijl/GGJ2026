using System;
using System.Collections;
using UnityEngine;

public class DamagePopupHandler : MonoBehaviour
{
    private WaitForSeconds waitForPopupGeneration;
    
    private bool isCollectingPopupInfo = false;

    private float totalDamageDealt = 0;



    private void Awake()
    {
        waitForPopupGeneration = new WaitForSeconds(0.5f);
        
        if(TryGetComponent(out HealthManager manager))
            manager.OnDamage.AddListener(CreatePopup);
    }
    
    private void OnDisable()
    {
        if(isCollectingPopupInfo)
            CreateDamagePopup(totalDamageDealt);
        
        StopAllCoroutines();
        totalDamageDealt = 0;
        isCollectingPopupInfo = false;
    }

    public void CreatePopup(HitInfo hitInfo)
    {
        if (!hitInfo.isContinuous)
        {
            CreateDamagePopup(hitInfo.damage);
            return;
        }
        
        if (hitInfo.isContinuous && !isCollectingPopupInfo)
            StartCoroutine(CollectPopupInfoCo());

        if (isCollectingPopupInfo)
            totalDamageDealt += hitInfo.damage;

    }

    private IEnumerator CollectPopupInfoCo()
    {
        isCollectingPopupInfo = true;

        yield return waitForPopupGeneration;

        CreateDamagePopup(totalDamageDealt);
        totalDamageDealt = 0;
        isCollectingPopupInfo = false;
    }
    
    private void CreateDamagePopup(float value)
    {
        if(PopupManager.instance)
            PopupManager.instance.CreateWorldPopup(transform.position, value);
    }
}
