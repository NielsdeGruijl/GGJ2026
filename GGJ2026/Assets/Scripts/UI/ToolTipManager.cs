using System;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    [SerializeField] private Canvas tooltipCanvas;
    [SerializeField] private Tooltip tooltipPrefab;

    public static ToolTipManager instance;

    private void Awake()
    {
        if(instance && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public Tooltip CreateUITooltip(Vector3 position, string title, string body)
    {
        Tooltip tooltip = Instantiate(tooltipPrefab, position, Quaternion.identity);
        tooltip.transform.SetParent(tooltipCanvas.transform);
        tooltip.Initialize(title, body);
        
        return tooltip;
    }
}
