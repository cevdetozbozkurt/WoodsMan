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
    private PlayerInventoryyy playerInventory;
    private bool isCreating;
    private Coroutine createCoinCoroutine;
    private Coroutine createCoinCorountineChair;
    public bool isActiveTimber;
    public bool isActiveChair;
    private float shopTimberPositionY = 0;
    private float shopChairPositionZ = -0.227f;
    private float shopChairPositionX = 0;
    [SerializeField]
    private List<GameObject> shopTimber = new List<GameObject>();
    [SerializeField]
    private List<GameObject> shopChairs = new List<GameObject>();

    private void Start() {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryyy>();
    }

    public void JumpToShop(Transform WoodToAddForInsideFactory){
        if(shopTimberHolder.childCount%5 == 0){
            shopTimberPositionY += 0.03f;
        }
        WoodToAddForInsideFactory.DOJump(shopTimberHolder.position + new Vector3(0.167f * (shopTimberHolder.childCount%5),0,shopTimberPositionY), 2f, 1, 0.1f).OnComplete(
            () =>{
                WoodToAddForInsideFactory.SetParent(shopTimberHolder, true);
                WoodToAddForInsideFactory.localPosition = new Vector3(0.167f * (shopTimberHolder.childCount%5),0,shopTimberPositionY);
                WoodToAddForInsideFactory.localRotation = Quaternion.identity;
            }
        );
    }

    IEnumerator JumpToShopChair(Transform ChairInShop){
        
        ChairInShop.DOJump(shopChairHolder.position + new Vector3(1f * (shopChairHolder.childCount%3),-0.169f,shopChairPositionZ), 2f, 1, 0.1f).OnComplete(
            () =>{
                ChairInShop.SetParent(shopChairHolder, true);
                ChairInShop.localPosition = new Vector3(1f * (shopChairHolder.childCount%3),-0.169f,shopChairPositionZ);
                ChairInShop.localRotation = Quaternion.identity;
            }
        );
        yield return new WaitForSeconds(0.5f);
        if(shopChairHolder.childCount % 3 == 0 ){
            shopChairPositionX = 0;
            shopChairPositionZ += -0.933f;
        }
    }

    IEnumerator GetTimbers(){
        while(playerInventory.Timber.Count > 0){
            JumpToShop(playerInventory.Timber.Last().transform);
            shopTimber.Add(playerInventory.Timber.Last());
            playerInventory.Timber.Remove(playerInventory.Timber.Last());
            playerInventory.items["Timber"]--;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator GetChairs(){
        while(playerInventory.Chair.Count > 0){
            StartCoroutine(JumpToShopChair(playerInventory.Chair.Last().transform));
            yield return new WaitForSeconds(0.3f);
            shopChairs.Add(playerInventory.Chair.Last());
            playerInventory.Chair.Remove(playerInventory.Chair.Last());
            playerInventory.items["Chair"]--;
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
                yield return new WaitForSeconds(1f);
            }
            Destroy(shopChairs.Last());
            shopChairs.Remove(shopChairs.Last());
            yield return new WaitForSeconds(1f);
            isActiveChair = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            if(playerInventory.items["Timber"] > 0) StartCoroutine(GetTimbers());

            if(playerInventory.items["Chair"] > 0) StartCoroutine(GetChairs());

        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
        }
    }

    private void Update() {
        if(shopTimber.Count > 0 && !isActiveTimber){
            createCoinCoroutine = StartCoroutine(CreateCoinForTimber());
        }else if(shopChairs.Count > 0 && !isActiveChair){
            createCoinCorountineChair = StartCoroutine(CreateCoinForChair());
        }
    }

}
