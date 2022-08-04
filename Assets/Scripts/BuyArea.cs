using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyArea : MonoBehaviour
{
    public Image progressImage;
    public TextMeshProUGUI coinText;
    public GameObject sawmillGameObject, buyGameObject;
    public float progress;
    public int cost, currentMoney;

    private void Start() {
        coinText.text = currentMoney.ToString() + "/" + cost.ToString();
    }

    public void Buy(int goldAmount){
        if (progress == 1)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            buyGameObject.SetActive(false);
            this.enabled = false;
            sawmillGameObject.SetActive(true);
        }
        else
        {
            currentMoney += goldAmount;
            progress = (float)currentMoney / (float)cost;
            progressImage.fillAmount = progress;
            coinText.text = currentMoney.ToString() + "/" + cost.ToString();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        BuyManager buyManager = other.GetComponent<BuyManager>();
        if (buyManager != null)
        {
            if(buyManager.numOfCoins >= 1)
            {
                buyManager.BuyProcess();
                Buy(1);
            }
        }
    }
}
