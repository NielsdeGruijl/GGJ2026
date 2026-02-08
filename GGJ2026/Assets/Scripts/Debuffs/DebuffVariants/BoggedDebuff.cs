
public class BoggedDebuff : BaseDebuff
{
    private float radius;
    private float areaDuration;
    
    public BoggedDebuff(string debuffTag, float duration, float radius, float areaDuration) 
        : base(debuffTag, duration)
    {
        this.radius = radius;
        this.areaDuration = areaDuration;
    }

    public override void Tick(float interval)
    {
        base.Tick(interval);

        LingeringArea area = ObjectPool.instance.Get(ObjectTypes.LingeringAreas).GetComponent<LingeringArea>();
        area.transform.position = target.transform.position;
        area.Initialize(radius, areaDuration);
    }
}
