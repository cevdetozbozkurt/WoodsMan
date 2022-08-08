using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public delegate void OnCollectTimberArea();
    public event OnCollectTimberArea OnTimberCollect;

    public delegate void OnCollectChairArea();
    public event OnCollectChairArea OnChairCollect;

    public delegate void OnGiveWoodArea();
    public event OnGiveWoodArea OnGiveWood;

    public delegate void OnGiveTimberArea();
    public event OnGiveTimberArea OnGiveTimber;

    public delegate void OnGiveTimberAreaDesk();
    public event OnGiveTimberArea OnGiveTimberDesk;

    public delegate void OnCollectDeskArea();
    public event OnCollectChairArea OnDeskCollect;
    /*
    public delegate void OnBuyArea();
    public static event OnBuyArea OnBuyingSawmill;
    public static BuyArea areaToBuy;
    */


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
    [SerializeField]
    private CreateDeskManager createDesk;
    [SerializeField]
    private DeskManager deskManager;
    public GameObject wood;
    public bool isCollecting;
    public bool isCollectingChair;
    public bool isGiving;
    public bool isGivingTimber;
    public bool isGivingTimberDesk;
    public bool isCollectingDesk;

    private void Start() {
        StartCoroutine(CollectTimberEnum());
        StartCoroutine(GiveWoodEnum());
        StartCoroutine(GiveTimberEnum());
        StartCoroutine(CollectingChairEnum());
        StartCoroutine(CollectingDeskEnum());
        StartCoroutine(GiveTimberDeskEnum());
    }

    IEnumerator CollectTimberEnum(){
        while(true){
            if(isCollecting){
                OnTimberCollect();
            }
            yield return new WaitForSeconds(0.2f);
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
    IEnumerator CollectingDeskEnum()
    {
        while (true)
        {
            if (isCollectingDesk)
            {
                OnDeskCollect();
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

    IEnumerator GiveTimberDeskEnum()
    {
        while (true)
        {
            if (isGivingTimberDesk)
            {
                OnGiveTimberDesk();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerStay(Collider other) {
        /*
        if(other.gameObject.CompareTag("BuyArea")){
            OnBuyingSawmill();
            areaToBuy = other.GetComponent<BuyArea>();
        }
        */
        if(other.gameObject.CompareTag("TimberArea")){
            isCollecting = true;
        }
        if (other.CompareTag("TakingChairArea"))
        {
            isCollectingChair = true;
        }
        if (other.CompareTag("TakingDeskArea"))
        {
            isCollectingDesk = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Wood")){
            wood = other.gameObject;
            collectManager.GetWood();
        }
        if(other.gameObject.CompareTag("WoodArea")){
            collectManager.giveWoodPoint = other.GetComponent<Transform>();
            createTimberManager.exitPoint = other.transform.parent.transform.GetChild(46).GetComponent<Transform>();
            isGiving = true;
        }
        if(other.gameObject.CompareTag("ChairArea")){
            isGivingTimber = true;
        }
        if (other.gameObject.CompareTag("DeskArea"))
        {
            isGivingTimberDesk = true;
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
        if (other.CompareTag("TakingDeskArea"))
        {
            isCollectingDesk = false;
        }
        if (other.gameObject.CompareTag("DeskArea"))
        {
            isGivingTimberDesk = false;
        }
        /*
        if (other.CompareTag("BuyArea")){
            areaToBuy = null;
        }
        */
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
        if (deskManager.timberList.Count > 0)
        {
            createDesk.isWorking = true;
        }
        else
        {
            createDesk.isWorking = false;
        }
    }

}
