using System.Collections;
using UnityEditor;
using UnityEngine;

public class BaseDebuff
{
    protected EntityDebuffManager target;
    protected string debuffTag;
    protected float duration;

    protected float timeElapsed;
    
    public BaseDebuff(string debuffTag, float debuffDuration)
    {
        this.debuffTag = debuffTag;
        duration = debuffDuration;
    }
    
    public virtual void Apply(EntityDebuffManager debuffTarget)
    {
        target = debuffTarget;
    }

    public virtual void Tick(float interval)
    {
        timeElapsed += interval;
        
        if(timeElapsed > duration)
            Remove();
    }
    
    public virtual void Remove()
    {
        target.RemoveDebuff(this);
    }

    public virtual void Refresh()
    {
        timeElapsed = 0;
    }

    public virtual bool CompareTag(string tagToCompareTo)
    {
        return debuffTag == tagToCompareTo;
    }
}
