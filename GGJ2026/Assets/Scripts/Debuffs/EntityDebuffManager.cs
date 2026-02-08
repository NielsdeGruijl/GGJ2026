using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDebuffManager : MonoBehaviour
{
    [SerializeField] private float debuffTick;
    
    private EntityStatModifiers debuffValues;

    private List<BaseDebuff> activeDebuffs;

    private Coroutine debuffTickCoroutine;
    private WaitForSeconds waitForDebuffTick;

    private void Awake()
    {
        activeDebuffs = new List<BaseDebuff>();
        waitForDebuffTick = new WaitForSeconds(debuffTick);
    }

    private void OnEnable()
    {
        debuffTickCoroutine = StartCoroutine(DebuffTickCo());
    }

    private void OnDisable()
    {
        StopCoroutine(debuffTickCoroutine);

        for (int i = 0; i < activeDebuffs.Count; i++)
        {
            activeDebuffs[i].Remove();
        }

        activeDebuffs.Clear();
    }

    public void ApplyDebuff(BaseDebuffSO debuff)
    {
        switch (debuff.stackingRule)
        {
            case StackingRule.Refresh:
                RefreshDebuff(debuff);
                break;
            case StackingRule.Stack:
                AddDebuff(debuff);
                break;
            case StackingRule.Ignore:
                // Only add a debuff if none of its type exists
                if(FindDebuff(debuff) == null)
                    AddDebuff(debuff);
                break;
        }
    }

    private void AddDebuff(BaseDebuffSO debuff)
    {
        BaseDebuff instance = debuff.MakeDebuff();
        instance.Apply(this);
        instance.Tick(debuffTick);
        activeDebuffs.Add(instance);
    }

    private void RefreshDebuff(BaseDebuffSO debuff)
    {
        BaseDebuff foundDebuff = FindDebuff(debuff);
        if(foundDebuff != null)
            foundDebuff.Refresh();
        else
            AddDebuff(debuff);
    }
    
    private BaseDebuff FindDebuff(BaseDebuffSO debuff)
    {
        for (int i = 0; i < activeDebuffs.Count; i++)
        {
            if (activeDebuffs[i] == null)
                continue;

            if (activeDebuffs[i].CompareTag(debuff.debuffTag))
                return activeDebuffs[i];
        }

        return null;
    }
    
    public void RemoveDebuff(BaseDebuff debuffToRemove)
    {
        if (activeDebuffs.Contains(debuffToRemove))
        {
            activeDebuffs.Remove(debuffToRemove);
        }
    }
    
    private IEnumerator DebuffTickCo()
    {
        while (true)
        {
            for (int i = 0; i < activeDebuffs.Count; i++)
            {
                if (activeDebuffs[i] == null)
                    continue;
                
                activeDebuffs[i].Tick(debuffTick);
            }
            
            yield return waitForDebuffTick;
        }
    }
}
