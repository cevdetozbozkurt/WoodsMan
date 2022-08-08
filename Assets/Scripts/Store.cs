using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
public class Store : MonoBehaviour
{
    
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private Transform coinTransform;
    [SerializeField]
    private Transform shopTimberHolder;
    [SerializeField]
    private Transform shopChairHolder; //Chair Stack
    [SerializeField]
    private Transform shopDeskHolder;
    private CollectManager playerInventory;
    private bool isCreating;
    private Coroutine createCoinCoroutine;
    private Coroutine createCoinCorountineChair;
    private Coroutine createCoinCorountineDesk;
    public bool isActiveTimber;
    public bool isActiveChair;
    public bool isActiveDesk;
    private float shopTimberPositionY = 0;
    private float shopChairPositionZ = -0.227f;
    private float shopChairPositionX = 0;
    [SerializeField]
    private List<GameObject> shopTimber = new List<GameObject>();
    [SerializeField]
    private List<GameObject> shopChairs = new List<GameObject>();
    [SerializeField]
    private List<GameObject> shopDesk = new List<GameObject>();

    private void Start() {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectManager>();
    }

    public void JumpToShop(Transform WoodToAddForInsideFactory){
        if(shopTimberHolder.childCount%5 == 0){
            shopTimberPositionY += 0.03f;
        }
        WoodToAddForInsideFactory.DOJump(shopTimberHolder.position + new Vector3(shopTimberHolder.childCount ,0,shopTimberPositionY), 2f, 1, 0.1f).OnComplete(
            () =>{
                WoodToAddForInsideFactory.SetParent(shopTimberHolder, true);
                WoodToAddForInsideFactory.localPosition = new Vector3(0.167f * (shopTimberHolder.childCount%5),0,shopTimberPositionY);
                WoodToAddForInsideFactory.localRotation = Quaternion.identity;
            }
        );
    }

    IEnumerator JumpToShopChair(Transform ChairInShop){
        
        ChairInShop.DOJump(shopChairHolder.position + new Vector3(shopChairHolder.childCount,-0.169f,shopChairPositionZ), 2f, 1, 0.1f).OnComplete(
            () =>{
                ChairInShop.SetParent(shopChairHolder, true);
                ChairInShop.localPosition = new Vector3(1f * (shopChairHolder.childCount%3),-0.169f,shopChairPositionZ);
                ChairInShop.localRotation = Quaternion.identity;
            }
        );
        yield return new WaitForSeconds(0.5f);
        if(shopChairHolder.childCount % 3 == 0 ){
            shopChairPositionZ += -0.933f;
        }
    }

    IEnumerator JumpToShopDesk(Transform ChairInShop)
    {

        ChairInShop.DOJump(shopDeskHolder.position + new Vector3(shopDeskHolder.childCount, -0.169f, shopChairPositionZ), 2f, 1, 0.1f).OnComplete(
            () => {
                ChairInShop.SetParent(shopDeskHolder, true);
                ChairInShop.localPosition = new Vector3(1f * (shopDeskHolder.childCount % 3), -0.169f, shopChairPositionZ);
                ChairInShop.localRotation = Quaternion.identity;
            }
        );
        yield return new WaitForSeconds(0.5f);
        if (shopDeskHolder.childCount % 3 == 0)
        {
            shopChairPositionZ += -0.933f;
        }
    }

    IEnumerator GetTimbers(){
        while(playerInventory.timberList.Count > 0){
            JumpToShop(playerInventory.timberList.Last().transform);
            shopTimber.Add(playerInventory.timberList.Last());
            playerInventory.timberList.Remove(playerInventory.timberList.Last());
            yield return new WaitForSeconds(0.1f);
            if (shopTimber.Count <= 1) shopTimberPositionY = 0;
        }
    }

    IEnumerator GetChairs(){
        while(playerInventory.chairList.Count > 0){
            StartCoroutine(JumpToShopChair(playerInventory.chairList.Last().transform));
            yield return new WaitForSeconds(0.3f);
            shopChairs.Add(playerInventory.chairList.Last());
            playerInventory.chairList.Remove(playerInventory.chairList.Last());
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator GetDesk()
    {
        while (playerInventory.deskList.Count > 0)
        {
            StartCoroutine(JumpToShopDesk(playerInventory.deskList.Last().transform));
            yield return new WaitForSeconds(0.3f);
            shopDesk.Add(playerInventory.deskList.Last());
            playerInventory.deskList.Remove(playerInventory.deskList.Last());
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator CreateCoinForTimber(){
        while(shopTimber.Count > 0){
            isActiveTimber = true;
            Instantiate(coin,coinTransform.position,Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(coin,coinTransform.position,Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(coin,coinTransform.position,Quaternion.identity);
            Destroy(shopTimber.Last());
            shopTimber.Remove(shopTimber.Last());
            yield return new WaitForSeconds(0.2f);
            isActiveTimber = false;
        }
    }

    public IEnumerator CreateCoinForChair(){
        while(shopChairs.Count > 0){
            isActiveChair = true;
            for(int i = 0;i<9;i++){
                Instantiate(coin,coinTransform.position,Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
            Destroy(shopChairs.Last());
            shopChairs.Remove(shopChairs.Last());
            yield return new WaitForSeconds(0.2f);
            isActiveChair = false;
        }
    }
    public IEnumerator CreateCoinForDesk()
    {
        while (shopDesk.Count > 0)
        {
            isActiveDesk = true;
            for (int i = 0; i < 15; i++)
            {
                Instantiate(coin, coinTransform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
            Destroy(shopDesk.Last());
            shopDesk.Remove(shopDesk.Last());
            yield return new WaitForSeconds(0.2f);
            isActiveDesk = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            if(playerInventory.timberList.Count > 0) StartCoroutine(GetTimbers());

            if(playerInventory.chairList.Count > 0) StartCoroutine(GetChairs());

            if (playerInventory.deskList.Count > 0) StartCoroutine(GetDesk());

        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
        }
    }

    private void Update() {
        if(shopTimber.Count > 0 && !isActiveTimber)
        {
            createCoinCoroutine = StartCoroutine(CreateCoinForTimber());
        }else if(shopChairs.Count > 0 && !isActiveChair)
        {
            createCoinCorountineChair = StartCoroutine(CreateCoinForChair());
        }else if(shopDesk.Count > 0 && !isActiveDesk)
        {
            createCoinCorountineDesk = StartCoroutine(CreateCoinForDesk());
        }
    }

}
