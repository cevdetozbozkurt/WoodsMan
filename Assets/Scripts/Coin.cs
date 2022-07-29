using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        PlayerInventoryyy playerInventory = other.GetComponent<PlayerInventoryyy>();
        if(playerInventory != null)
        {
            playerInventory.CoinCollected();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }    
    }
}
