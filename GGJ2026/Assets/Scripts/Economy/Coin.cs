using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float floatSpeed;
    [SerializeField] private float floatMagnitude;
    
    public int value;
    
    

    private void Update()
    {
        transform.Translate(new Vector2(0, Mathf.Sin(Time.fixedTime * floatSpeed) * floatMagnitude));
    }
}
