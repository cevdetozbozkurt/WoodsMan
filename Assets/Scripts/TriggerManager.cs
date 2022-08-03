using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public delegate void OnCollectTimberArea();
    public static event OnCollectTimberArea OnTimberCollect;

    public delegate void OnCollectChairArea();
    public static event OnCollectChairArea OnChairCollect;

    public delegate void OnGiveWoodArea();
    public static event OnGiveWoodArea OnGiveWood;

    public delegate void OnGiveTimberArea();
    public static event OnGiveTimberArea OnGiveTimber;

    public delegate void OnBuyArea();
    public static event OnBuyArea OnBuyingSawmill;
    public static event OnBuyArea OnBuyingFurniture;
    public static BuyArea areaToBuy;

    

    [SerializeField]
    private CollectManager collectManager;
    [SerializeField]
    private CreateTimberManager createTimberManager;
    [SerializeField]
    private CreateChairManager createChairManager;
    [SerializeField]
    private SawMillManager sawMillManager;
    [SerializeField]
    private FurnitureManager furniture;
    public GameObject wood;
    public bool isCollecting;
    public bool isCollectingChair;
    public bool isGiving;
    public bool isGivingTimber;

    private void Start() {
        StartCoroutine(CollectTimberEnum());
        StartCoroutine(GiveWoodEnum());
        StartCoroutine(GiveTimberEnum());
        StartCoroutine(CollectingChairEnum());
    }

    IEnumerator CollectTimberEnum(){
        while(true){
            if(isCollecting){
                OnTimberCollect();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator CollectingChairEnum()
    {
        while (true)
        {
            if (isCollectingChair)
            {
                OnChairCollect();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator GiveWoodEnum(){
        while(true){
            if(isGiving){
                OnGiveWood();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator GiveTimberEnum()
    {
        while (true)
        {
            if (isGivingTimber)
            {
                OnGiveTimber();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("BuyArea")){
            OnBuyingSawmill();
            areaToBuy = other.GetComponent<BuyArea>();
        }
        if(other.gameObject.CompareTag("TimberArea")){
            isCollecting = true;
        }
        if (other.CompareTag("TakingChairArea"))
        {
            isCollectingChair = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Wood")){
            wood = other.gameObject;
            collectManager.GetWood();
        }
        if(other.gameObject.CompareTag("WoodArea")){
            isGiving = true;
        }
        if(other.gameObject.CompareTag("ChairArea")){
            isGivingTimber = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("TimberArea")){
            isCollecting = false;
        }
        if(other.gameObject.CompareTag("ChairArea")){
            isGivingTimber = false;
        }
        if (other.gameObject.CompareTag("WoodArea"))
        {
            isGiving = false;
        }
        if (other.CompareTag("TakingChairArea"))
        {
            isCollectingChair = false;
        }
        if (other.CompareTag("BuyArea")){
            areaToBuy = null;
        }
    }

    private void Update()
    {
        if (sawMillManager.sawMillWoods.Count > 0)
        {
            createTimberManager.isWorking = true;
        }
        else
        {
            createTimberManager.isWorking = false;
        }
        if (furniture.FurnitureTimber.Count > 0)
        {
            createChairManager.isWorking = true;
        }
        else
        {
            createChairManager.isWorking = false;
        }
    }

}
