using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuyManager : MonoBehaviour
{

    public int numOfCoins {get; private set;}
    public UnityEvent<BuyManager> OnCoinCollected;

    private void OnEnable() {
        TriggerManager.OnBuyingSawmill += BuyArea; 
    }

    private void OnDisable() {
        TriggerManager.OnBuyingSawmill -= BuyArea; 
    }

    void BuyArea(){
        if(TriggerManager.areaToBuy != null){
            if(numOfCoins > 0){
                TriggerManager.areaToBuy.Buy(1);
                numOfCoins--;
                OnCoinCollected.Invoke(this);
            }
        }
    }

    public void CoinCollected(){
        numOfCoins++;
        OnCoinCollected.Invoke(this);
    }
}
