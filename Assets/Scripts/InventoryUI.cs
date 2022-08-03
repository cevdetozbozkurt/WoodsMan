using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI coinText;
    public void Start() 
    {
        coinText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinText(BuyManager buyManager)
    {
        coinText.text = buyManager.numOfCoins.ToString();
    }
}
