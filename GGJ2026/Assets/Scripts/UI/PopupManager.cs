using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private DamagePopup popup;

    private Camera cam;
    
    public static PopupManager instance { get; private set; }

    private void Awake()
    {
        if(instance && instance != this)
            Destroy(this);
        else
            instance = this;
        
        cam = Camera.main;
    }

    public void CreatePopup(Vector2 position, float value, PopupType type = PopupType.Default)
    {
        Vector2 canvasPosition = cam.WorldToScreenPoint(position).ToVector2();

        DamagePopup popupObject = ObjectPool.instance.Get(ObjectTypes.DamagePopups).GetComponent<DamagePopup>();
        popupObject.transform.position = canvasPosition;
        
        popupObject.Initialize(value, type);
        popupObject.transform.SetParent(transform);
    }
}
