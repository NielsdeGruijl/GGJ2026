using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text bodyText;

    public void Initialize(string title, string body)
    {
        titleText.text = title;
        bodyText.text = body;
        
    }
    
}
