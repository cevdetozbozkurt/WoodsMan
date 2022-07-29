using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public delegate void OnCollectTimberArea();
    public static event OnCollectTimberArea OnTimberCollect;

    bool isCollecting;
    bool isGiving;

    private void Start() {
        StartCoroutine(CollectTimberEnum());
    }

    IEnumerator CollectTimberEnum(){
        while(true){
            if(isCollecting){
                OnTimberCollect();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("TimberArea")){
            isCollecting = true;
            Debug.Log("calisti");
        }
        // if(other.gameObject.CompareTag("WoodArea")){
        //     isGiving = true;
        // }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("TimberArea")){
            isCollecting = false;
        }
        // if(other.gameObject.CompareTag("WoodArea")){
        //     isGiving = false;
        // }
    }

}
