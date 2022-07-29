using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public delegate void OnCollectTimberArea();
    public static event OnCollectTimberArea OnTimberCollect;

    public delegate void OnGiveWoodArea();
    public static event OnGiveWoodArea OnGiveWood;

    [SerializeField]
    private CollectManager collectManager;
    [SerializeField]
    private CreateTimberManager createTimberManager;
    public GameObject wood;
    public bool isCollecting;
    public bool isGiving;

    private void Start() {
        StartCoroutine(CollectTimberEnum());
        StartCoroutine(GiveWoodEnum());
    }

    IEnumerator CollectTimberEnum(){
        while(true){
            if(isCollecting){
                OnTimberCollect();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator GiveWoodEnum(){
        while(true){
            if(isGiving){
                OnGiveWood();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("TimberArea")){
            isCollecting = true;
            Debug.Log("calisti");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Wood")){
            wood = other.gameObject;
            collectManager.GetWood();
        }
        if(other.gameObject.CompareTag("WoodArea")){
            isGiving = true;
            createTimberManager.isWorking = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("TimberArea")){
            isCollecting = false;
        }
        if(other.gameObject.CompareTag("WoodArea")){
            isGiving = false;
        }
    }

}
