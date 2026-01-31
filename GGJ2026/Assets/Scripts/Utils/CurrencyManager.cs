using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int money;

    public bool Purchase(int cost)
    {
        if (money < cost)
            return false;
        
        money -= cost;
        return true;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        
        Debug.Log(money);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Coin coin;
        
        Debug.Log(collider.TryGetComponent<Coin>(out coin));
        if (collider.TryGetComponent<Coin>(out coin))
        {
            AddMoney(coin.value);
            
            Destroy(collider.gameObject);
        }
    }
}
