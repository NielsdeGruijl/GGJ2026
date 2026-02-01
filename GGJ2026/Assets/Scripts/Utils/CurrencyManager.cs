using System;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    
    public int money;

    public bool Purchase(int cost)
    {
        if (money < cost)
            return false;
        
        money -= cost;
        
        UpdateUI();
        
        return true;
    }

    public void AddMoney(int amount)
    {
        money += amount;

        PlayerLevelManager.instance.AddXP(amount);

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (!moneyText)
            return;
        
        moneyText.text = $"${money}";
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Coin coin;
        
        if (collider.TryGetComponent<Coin>(out coin))
        {
            AddMoney(coin.value);
            
            Destroy(collider.gameObject);
        }
    }
}
