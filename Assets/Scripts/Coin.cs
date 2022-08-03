using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        BuyManager buyManager = other.GetComponent<BuyManager>();
        if(buyManager != null)
        {
            buyManager.CoinCollected();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }    
    }
}
