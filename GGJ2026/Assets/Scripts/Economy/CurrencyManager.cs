using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private AudioClip pickupCoinSound;
    [SerializeField] private float coinPitchIncrement;
    [SerializeField] private float coinPitchResetDelay;
    
    public int money;

    private WaitForSeconds waitForCoinPitchReset;

    private Coroutine coinPitchReset;

    private bool isCoinPitchResetRunning;
    
    private float coinPitch = 1;

    private void Awake()
    {
        waitForCoinPitchReset = new WaitForSeconds(coinPitchResetDelay);
    }

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
        if(isCoinPitchResetRunning)
            StopCoroutine(coinPitchReset);
        
        AudioManager.instance.PlayCoinSound(pickupCoinSound, coinPitch);

        coinPitch += coinPitchIncrement;

        coinPitchReset = StartCoroutine(ResetCoinPitchCo());
        
        money += amount;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (!moneyText)
            return;
        
        moneyText.text = $"${money}";
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Coin coin;
        
        if (collider.TryGetComponent<Coin>(out coin))
        {
            AddMoney(coin.value);
            
            ObjectPool.instance.PoolObject(ObjectTypes.Coins, coin.gameObject);
        }
    }

    private IEnumerator ResetCoinPitchCo()
    {
        isCoinPitchResetRunning = true;
        yield return waitForCoinPitchReset;

        coinPitch = 1;
        isCoinPitchResetRunning = false;
    }
}
